using UnityEngine;
using UnityEngine.SceneManagement;

public class FailWindow : MonoBehaviour
{
    [SerializeField] private int menuScene = 0;
    
    private WindowsController windowsController;
    
    private void Start()
    {
        Controllers.Instance.GetController(EControllerType.Windows, out windowsController);
    }
    
    public void MenuButtonClick()
    {
        windowsController.HideWindow(EWindowType.Fail);
        SceneManager.LoadScene(menuScene);
    }
}
