using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoodSelectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject errorPanel;
    public GameObject notiPanel;
    public TMPro.TMP_Text errorTxt;
    public Button[] buttons;
    public Button nextBtn;

    private int selectedBtnId;
    private int selectedMood;
    private IDictionary<int, int> descriptionDic = new Dictionary<int, int>();

    private Action<bool> _ReportSuccessCallback;
    private Action<bool> _UnlockBadgeCallback;
    // Start is called before the first frame update
    void Start()
    {
        errorTxt.text = "Please Select A Mood!";
        selectedMood = -1;
        selectedBtnId = 0;
        InitializeDictionary();

        _ReportSuccessCallback = (isCorrect) => {
            if (isCorrect)
            {
                notiPanel.SetActive(true);
                StartCoroutine(Main.instance.web.UnlockReportBadge(Main.instance.userInfo.patient_accounts_id, _UnlockBadgeCallback));
            }
        };

        _UnlockBadgeCallback = (isCorrect) => {
            StartCoroutine(ExecuteAfterTime(1));
        };

        for (int i = 0; i < buttons.Length; i++)
        {
            int btnId = i;
            buttons[i].onClick.AddListener(() => {
                SFXScript.instance.PlaySound("Tap");
                SelectPainDescription(btnId);
            });
        }

        nextBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            if (selectedMood == -1)
            {
                errorPanel.SetActive(true);
            }
            else
            {
                Main.instance.report.mood = selectedMood;
                StartCoroutine(Main.instance.web.SendReport(Main.instance.userInfo.patient_accounts_id, Main.instance.report, _ReportSuccessCallback));
            }
        });
    }

    void SelectPainDescription(int btnId)
    {
        selectedMood = descriptionDic[btnId];
        selectedBtnId = btnId;
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != selectedBtnId)
                ChangeFalseColor(i);
            else
                ChangeTrueColor(i);

        }
    }

    void ChangeTrueColor(int btnId)
    {
        ColorBlock cb = buttons[btnId].colors;
        cb.normalColor = cb.highlightedColor = cb.pressedColor = cb.selectedColor = new Color(1, 0.5f, 0);
        buttons[btnId].colors = cb;
    }

    void ChangeFalseColor(int btnId)
    {
        ColorBlock cb = buttons[btnId].colors;
        cb.normalColor = cb.highlightedColor = cb.pressedColor = cb.selectedColor = new Color(1, 1, 1);
        buttons[btnId].colors = cb;
    }

    void InitializeDictionary()
    {
        descriptionDic.Add(0, 0);
        descriptionDic.Add(1, 1);
        descriptionDic.Add(2, 2);
        descriptionDic.Add(3, 3);
        descriptionDic.Add(4, 4);
        descriptionDic.Add(5, 5);
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        // Code to execute after the delay
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);

    }
}
