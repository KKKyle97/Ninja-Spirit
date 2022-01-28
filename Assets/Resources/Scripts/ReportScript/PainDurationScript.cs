using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainDurationScript : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject nextPanel;
    public Button nextBtn;
    public TMPro.TMP_Text valueTxt;
    // Start is called before the first frame update
    void Start()
    {
        nextBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            Main.instance.report.duration = Int32.Parse(valueTxt.text);
            currentPanel.SetActive(false);
            nextPanel.SetActive(true);
        });
    }
}
