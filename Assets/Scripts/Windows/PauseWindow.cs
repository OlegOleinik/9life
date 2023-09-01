using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWindow : MonoBehaviour
{
    [SerializeField] private int menuScene = 0;

    private WindowsController windowsController;

    private void Start()
    {
        Controllers.Instance.GetController(EControllerType.Windows, out windowsController);
    }

    public void MenuButtonClick()
    {
        windowsController.HideWindow(EWindowType.Pause);
        SceneManager.LoadScene(menuScene);
    }
    
    public void PlayButtonClick()
    {
        windowsController.HideWindow(EWindowType.Pause);
    }
}
