using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DescriptionScript : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject nextPanel;
    public GameObject errorPanel;
    public TMPro.TMP_Text errorTxt;
    public Button[] buttons;
    public Button nextBtn;

    private int selectedBtnId;
    private string selectedDescription;
    private IDictionary<int, string> descriptionDic = new Dictionary<int, string>();
    // Start is called before the first frame update
    void Start()
    {
        errorTxt.text = "Please Select a Description!";
        selectedBtnId = 0;
        InitializeDictionary();

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
            if (string.IsNullOrEmpty(selectedDescription))
            {
                errorPanel.SetActive(true);
            }
            else
            {
                Main.instance.report.description = selectedDescription;
                currentPanel.SetActive(false);
                nextPanel.SetActive(true);
            }
        });
    }

    void SelectPainDescription(int btnId)
    {
        selectedDescription = descriptionDic[btnId];
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

    void InitializeDictionary() {
        descriptionDic.Add(0, "Aching");
        descriptionDic.Add(1, "Stabbing");
        descriptionDic.Add(2, "Prickling");
        descriptionDic.Add(3, "Throbbing");
        descriptionDic.Add(4, "Burning");
    }
}
