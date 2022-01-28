using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject UI;
    public GameObject previousUI;
    public GameObject currentUI;
    public GameObject bodyUI;

    public void CloseUI()
    {
        SFXScript.instance.PlaySound("Tap");
        UI.SetActive(false);
    }

    public void ResumeGame()
    {
        SFXScript.instance.PlaySound("Tap");
        UI.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoBack()
    {
        SFXScript.instance.PlaySound("Tap");
        previousUI.SetActive(true);
        currentUI.SetActive(false);
    }

    public void ReportGoBack()
    {
        SFXScript.instance.PlaySound("Tap");
        if (Main.instance.report.body.Contains("None"))
            bodyUI.SetActive(true);
        else
            previousUI.SetActive(true);

        currentUI.SetActive(false);
        
    }

    public void ToHomeScene()
    {
        SFXScript.instance.PlaySound("Tap");
        SceneManager.LoadScene("MainMenuScene",LoadSceneMode.Single);
    }

    public void ToProfileScene()
    {
        SFXScript.instance.PlaySound("Tap");
        SceneManager.LoadScene("ProfileScene",LoadSceneMode.Single);
    }

    public void ToBadgeScreen()
    {
        SFXScript.instance.PlaySound("Tap");
        SceneManager.LoadScene("BadgeScene",LoadSceneMode.Single);
    }
}
