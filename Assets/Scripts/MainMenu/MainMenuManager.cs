using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void PlayGame(string NameScene)
    {
        SceneManager.LoadSceneAsync(NameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
