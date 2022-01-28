using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheckScript : MonoBehaviour
{
    public Toggle toggle;
    public Button closeBtn;
    public GameObject tutorialUI;
    public GameObject countdownUI;
    private Action<bool> _CloseUICallback;

    // Start is called before the first frame update

    public void OpenTutorial()
    {
        SFXScript.instance.PlaySound("Tap");
        tutorialUI.SetActive(true);
        GameStartScript.instance.PauseGame();
    }

    private void Start()
    {
        toggle.isOn = Main.instance.userInfo.is_skipped;

        _CloseUICallback = (isCorrect) => {
            if (isCorrect)
            {
                tutorialUI.SetActive(false);
                if (GameStartScript.instance.GetGameStarted() == false)
                {
                    countdownUI.SetActive(true);
                    CountdownScript.instance.StartCountdown();
                }
                else {
                    GameStartScript.instance.StartGame();
                }
            }
        };

        closeBtn.onClick.AddListener(()=> {
            SFXScript.instance.PlaySound("Tap");
            if (toggle.isOn != Main.instance.userInfo.is_skipped)
            {
                Debug.Log("changing");
                Main.instance.userInfo.is_skipped = toggle.isOn;
                StartCoroutine(Main.instance.web.ShowTutorial(Main.instance.userInfo.id, Main.instance.userInfo.is_skipped, _CloseUICallback));
            }
            else
            {
                _CloseUICallback(true);
            }
        });

        if (Main.instance.userInfo.is_skipped)
        {
            tutorialUI.SetActive(false);
            GameStartScript.instance.PauseGame();
            countdownUI.SetActive(true);
            CountdownScript.instance.StartCountdown();
        }
        else
        {
            tutorialUI.SetActive(true);
            GameStartScript.instance.PauseGame();
        }
    }

    
}
