using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIM : MonoBehaviour
{
    public GameObject PauseUI;

    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        PauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            paused = !paused;
            SoundManager.instance.PlayStartSound();
        }

        if(paused)
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }

        if(!paused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    { 
        SceneManager.LoadScene("MyGame");
        Score.scores = 0;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        paused = true;
    }
}
