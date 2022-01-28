using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    // Update is called once per frame
    public GameObject gameOverUI;
    public GameObject badgeUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TMP_Text highscoreTxt;
    public static bool isInputEnabled;
    private Action<bool> _UpdateScoreCallback;
    private Action<bool> _DisplayNotiCallback;
    private Action<bool> _ReloadProfileCallback;
    private bool updated = false;

    private void Start()
    {
        isInputEnabled = true;

        _DisplayNotiCallback = (isCorrect) => {
            if (isCorrect)
            {
                badgeUI.SetActive(true);
                Main.instance.hasNewUnlock = false;
            }
        };

        _ReloadProfileCallback = (isCorrect) =>
        {
            StartCoroutine(Main.instance.web.UnlockCoinBadge(Main.instance.userInfo.patient_accounts_id, ScoreManager.instance.GetCoinScore(), _DisplayNotiCallback));
        };

        _UpdateScoreCallback = (isCorrect) => {
            StartCoroutine(Main.instance.web.LoadProfile(Main.instance.userInfo.id,_ReloadProfileCallback));
        };
    }
    void Update()
    {

        if (PlayerController.GetIsDead()) {
            isInputEnabled = false;
            if (updated == false) {
                updated = true;
                StartCoroutine(ExecuteAfterTime(0.7f));
            }
            
        }
    }

    void GameOver(int score, int coin)
    {
        Debug.Log("Running");
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        coinText.text = "Coins Collected: " + coin;
        scoreText.text = "Total Score: " + (score + 1);

        if (score > Main.instance.userInfo.highscore)
            highscoreTxt.gameObject.SetActive(true);
        else
            highscoreTxt.gameObject.SetActive(false);

        UpdateScore(score,coin);
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        // Code to execute after the delay
        GameOver((int) ScoreManager.instance.GetScore(), ScoreManager.instance.GetCoinScore());
    }

    void UpdateScore(int score, int coin)
    {
        //todo: get user id after login to update in database
        StartCoroutine(Main.instance.web.UpdateScore(Main.instance.userInfo.id,score+1,coin,_UpdateScoreCallback));
    }

}
