using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WindowElement
{
    public EWindowType type;
    public Window window;
}

public class WindowsController : AController
{
    [SerializeField] private List<WindowElement> windowsList;
    
    private Dictionary<EWindowType, Window> windowsDictonary = new Dictionary<EWindowType, Window>();
    private List<Window> showedWindows = new List<Window>();
    private PauseController pauseController;

    private void Start()
    {
        Controllers.Instance.GetController(EControllerType.Pause, out pauseController);

        foreach (var window in windowsList)
        {
            if (windowsDictonary.Keys.Contains(window.type))
                Debug.LogError($"[WindowsController][Start] Window with this type is already registered");
            else
            {
                windowsDictonary.Add(window.type, window.window);
                window.window.Hide();
            }
        }
    }

    public void ShowWindow(EWindowType type)
    {
        pauseController.PauseGame();
        var window = windowsDictonary[type];
        if (!showedWindows.Contains(window))
        {
            window.Show();
            showedWindows.Add(window);
        }
    }

    public void HideWindow(EWindowType type)
    {
        pauseController.PlayGame();
        var window = windowsDictonary[type];
        window.Hide();
        showedWindows.Remove(window);
    }

    public void HideAllWindows(EWindowType type)
    {
        pauseController.PlayGame();
        foreach (var window in windowsDictonary)
        {
            window.Value.Hide();
            if (showedWindows.Contains(window.Value)) showedWindows.Remove(window.Value);
        }
    }
}
