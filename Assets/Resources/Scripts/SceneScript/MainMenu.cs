using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button PlayBtn;
    public Button ReportBtn;
    public Button MessageBtn;
    public Button ProfileBtn;
    public GameObject badgePanel;

    private void Start()
    {
        PlayBtn.onClick.AddListener(ToGameScene);
        MessageBtn.onClick.AddListener(ToMessagePanel);
        ProfileBtn.onClick.AddListener(ToProfileScene);
        ReportBtn.onClick.AddListener(ToReportScene);

        if (Main.instance.hasNewUnlock) badgePanel.SetActive(true);
    }
    void ToGameScene() {
        SFXScript.instance.PlaySound("Tap");
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
    void ToMessagePanel() {
        SFXScript.instance.PlaySound("Tap");
        SceneManager.LoadScene("MessageScene", LoadSceneMode.Single);
    }
    void ToProfileScene() {
        SFXScript.instance.PlaySound("Tap");
        SceneManager.LoadScene("ProfileScene", LoadSceneMode.Single);
    }

    void ToReportScene() {
        SFXScript.instance.PlaySound("Tap");
        SceneManager.LoadScene("ReportScene", LoadSceneMode.Single);
    }
}
