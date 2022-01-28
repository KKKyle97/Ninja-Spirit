using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadgeScript : MonoBehaviour
{
    public GameObject[] lockBadges;
    public GameObject statusUI;
    public TMPro.TMP_Text titleTxt;
    public TMPro.TMP_Text statusTxt;
    public Button closeBtn;
    public Action<bool> callback;

    // Start is called before the first frame update
    void Start()
    {
        callback = (isCorrect) => {
            statusUI.SetActive(false);
            DisplayUnlockedBadge();
        };

        statusUI.SetActive(true);
        titleTxt.text = "status";
        statusTxt.text = "Loading...";
        closeBtn.gameObject.SetActive(false);
        StartCoroutine(Main.instance.web.GetBadges(Main.instance.userInfo.patient_accounts_id, callback));
    }

    private void DisplayUnlockedBadge()
    {
        foreach (Badge badge in Main.instance.badges)
        {
            lockBadges[badge.id - 1].transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
