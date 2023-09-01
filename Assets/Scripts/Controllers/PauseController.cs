using UnityEngine;

public class PauseController : AController
{
    private float timeScale = 1;

    private void Start()
    {
        timeScale = Time.timeScale;
    }

    public void PauseGame() => Time.timeScale = 0;

    public void PlayGame() => Time.timeScale = timeScale;
}
