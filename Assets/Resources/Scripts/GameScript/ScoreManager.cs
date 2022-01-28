using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI kunaiText;
    public TextMeshProUGUI scoreText;
    float score;
    int coinScore = 0;
    int kunaiScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
        kunaiText.text = "X " + kunaiScore.ToString();

    }

    private void Update()
    {
        if (!GameStartScript.instance.GetGameStarted()) {
            score = 0;
            scoreText.text = "Score: " + score.ToString("0.00");
        }
        if (!PlayerController.GetIsDead())
        {
            score += 5 * Time.deltaTime;
            scoreText.text = "Score: " + score.ToString("0.00");
        }
        
    }

    public void AddScore(int value,string collectableObject) {
        if (collectableObject.Equals("coin"))
        {
            coinScore += value;
            coinText.text = "X " + coinScore.ToString();
        }
        else {
            kunaiScore += value;
            kunaiText.text = "X " + kunaiScore.ToString();
        }
    }

    public void MinusScore() {
        kunaiScore--;
        kunaiText.text = "X " + kunaiScore.ToString();
    }

    public int GetKunaiScore() {
        return kunaiScore;
    }

    public int GetCoinScore()
    {
        return coinScore;
    }

    public float GetScore() {
        return score;
    }
}
