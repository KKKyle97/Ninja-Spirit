using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : MonoBehaviour
{
    public static GameStartScript instance;
    private bool isGameStarted;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        isGameStarted = false;
        if(MusicScript.Instance != null) Destroy(MusicScript.Instance.gameObject);
        SFXScript.instance.bgMusic = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void SetGameStarted(bool val) {
        isGameStarted = val;
    }

    public bool GetGameStarted() {
        return isGameStarted;
    }
}
