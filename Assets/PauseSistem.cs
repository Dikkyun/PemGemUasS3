using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseSistem : MonoBehaviour
{
    public GameObject pause;

    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !isPaused)
        {
            Pause();
        }
    }

    public void Continue()
    {
        pause.gameObject.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pause.gameObject.SetActive(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void MainMenu(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
