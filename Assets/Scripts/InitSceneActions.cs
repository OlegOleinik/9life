using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSceneActions : MonoBehaviour
{
    [SerializeField] private int gameScene = 1;
    
    public void StartButtonClick()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
