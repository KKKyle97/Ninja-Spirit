using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    public TMPro.TMP_Text countdownTxt;
    public int countdownTime;
    public GameObject countdownPanel;
    public static CountdownScript instance;
    // Start is called before the first frame update
    void Start()
    {
       instance = this;
    }

    public void StartCountdown() {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {

        
        while (countdownTime > 0) {
            countdownTxt.text = countdownTime.ToString();
            Debug.Log(countdownTime);
            yield return new WaitForSecondsRealtime(1f);
            Debug.Log("countdown " + countdownTime);
            countdownTime--;
        }

        GameStartScript.instance.SetGameStarted(true);

        GameStartScript.instance.StartGame();
        
        countdownTxt.text = "Go!";

        yield return new WaitForSecondsRealtime(1f);

        countdownPanel.SetActive(false);
    }
}
