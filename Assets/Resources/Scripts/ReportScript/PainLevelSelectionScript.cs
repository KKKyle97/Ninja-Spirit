using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainLevelSelectionScript : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject nextPanel;
    public Button saveBtn;
    public TMPro.TMP_Text valueTxt;

    // Start is called before the first frame update
    void Start()
    {
        saveBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            Main.instance.report.level = Int32.Parse(valueTxt.text);
            currentPanel.SetActive(false);
            nextPanel.SetActive(true);
        });
    }
}
