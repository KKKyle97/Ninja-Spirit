using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Message : MonoBehaviour
{
    // Start is called before the first frame update
    public Button[] buttons;
    public Button submitBtn;
    public GameObject successPanel;
    public TMPro.TMP_Text titleTxt;
    public TMPro.TMP_Text contentTxt;
    public Slider slider;

    private bool[] buttonToggle = { false, false, false, false};
    private IDictionary<int, string> messageDic = new Dictionary<int,string>();
    private string finalMessage;
    private int selectedBodyPartCount;

    private Action<bool> _messageAPICallback;
    private void Start()
    {
        _messageAPICallback = (isTrue) => {
            if (isTrue)
            {
                titleTxt.text = "Success";
                contentTxt.text = "Message Has Been Sent!";
                successPanel.SetActive(true);
                ExecuteAfterTime(1);
            }
            else
            {
                titleTxt.text = "Error";
                contentTxt.text = "Error Sending Message!";
                successPanel.SetActive(true);
                ExecuteAfterTime(1);
            }
        };

        InitializeDictionary();

        selectedBodyPartCount = 0;

        for (int i = 0; i < buttons.Length; i++) {
            int bId = i;
            buttons[i].onClick.AddListener(() => {
                SFXScript.instance.PlaySound("Tap");
                SelectPainLocation(bId);
            });
        }

        submitBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            finalMessage = "Pain at ";
            if (selectedBodyPartCount > 0)
            {
                for (int i = 0; i < buttonToggle.Length; i++)
                {
                    if (buttonToggle[i] == true)
                    {
                        finalMessage += messageDic[i] + ", ";
                    }
                }

                StartCoroutine(Main.instance.web.SendMessage(Main.instance.userInfo.patient_accounts_id, Int32.Parse(slider.valueTxt.text), finalMessage, _messageAPICallback));
            }
            else {
                successPanel.SetActive(true);
                titleTxt.text = "Error";
                contentTxt.text = "Please select a body part";
                Debug.Log("No body part selected");
            }
            
        });
    }

    void SelectPainLocation(int buttonId) {
        if (buttonToggle[buttonId] == true) 
        {
            ChangeFalseColor(buttonId);
            selectedBodyPartCount--;
            buttonToggle[buttonId] = false;
        }
        else 
        {
            ChangeTrueColor(buttonId);
            selectedBodyPartCount++;
            buttonToggle[buttonId] = true;
        }
            

        Debug.Log(buttonToggle[buttonId]);

        
        
    }

    void ChangeTrueColor(int buttonId)
    {
        ColorBlock cb = buttons[buttonId].colors;
        cb.normalColor = cb.highlightedColor = cb.pressedColor = cb.selectedColor = new Color(1, 0.5f, 0);
        buttons[buttonId].colors = cb;
    }

    void ChangeFalseColor(int buttonId)
    {
        ColorBlock cb = buttons[buttonId].colors;
        cb.normalColor = cb.highlightedColor = cb.pressedColor = cb.selectedColor = new Color(1, 1, 1);
        buttons[buttonId].colors = cb;
    }

    void InitializeDictionary() {
        messageDic.Add(0, "Head");
        messageDic.Add(1, "Body");
        messageDic.Add(2, "Hand");
        messageDic.Add(3, "Leg");
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        // Code to execute after the delay
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);

    }
}
