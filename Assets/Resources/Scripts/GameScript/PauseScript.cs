using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    bool GameIsPaused = false;
    // Update is called once per frame
    public GameObject gamePauseUI;
    public static bool isInputEnabled;
    // Start is called before the first frame update
    private void Start()
    {
        isInputEnabled = true;
    }
    // Update is called once per frame
    public void PauseGame()
    {
        SFXScript.instance.PlaySound("Tap");
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void Resume()
    {
        gamePauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        gamePauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
