using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using TMPro;

namespace Tests
{
    public class TestSuite
    {
        private GameObject canvas;
        [SetUp]
        public void Setup()
        {
            // We delete everything from PlayerPrefs before every test
            SceneManager.LoadScene("Assets/Scenes/LoginScene.unity");
            canvas = GameObject.Find("Canvas");

        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestPlayerIsRunningRealTime()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.


            yield return new WaitForSeconds(1);

            Main.instance.userInfo.id = 1;
            Main.instance.userInfo.avatars_id = 1;
            Main.instance.userInfo.coin = 0;
            Main.instance.userInfo.highscore = 0;
            Main.instance.userInfo.is_skipped = true;

            SceneManager.LoadScene("Assets/Scenes/GameScene.unity");

            yield return new WaitForSeconds(1);

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            

            float initialPosX = player.transform.position.x;

            yield return new WaitForSeconds(1);

            Assert.Greater(player.transform.position.x, initialPosX);
        }

        [UnityTest]
        public IEnumerator TestCoinIsUpdatedRealTime()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.


            yield return new WaitForSeconds(1);

            Main.instance.userInfo.id = 1;
            Main.instance.userInfo.avatars_id = 1;
            Main.instance.userInfo.coin = 0;
            Main.instance.userInfo.highscore = 0;
            Main.instance.userInfo.is_skipped = true;

            SceneManager.LoadScene("Assets/Scenes/GameScene.unity");

            

            yield return new WaitForSeconds(0.1f);

            string coinTxt = GameObject.Find("CoinCounterText").GetComponent<TMP_Text>().text;
           

            yield return new WaitForSeconds(2);

            TMP_Text coinCounterUpdatedText = GameObject.Find("CoinCounterText").GetComponent<TMP_Text>();

            Assert.AreNotEqual(coinTxt, coinCounterUpdatedText.text);
        }

        [UnityTest]
        public IEnumerator TestScoreIsUpdatedRealTime()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.


            yield return new WaitForSeconds(1);

            Main.instance.userInfo.id = 1;
            Main.instance.userInfo.avatars_id = 1;
            Main.instance.userInfo.coin = 0;
            Main.instance.userInfo.highscore = 0;
            Main.instance.userInfo.is_skipped = true;

            SceneManager.LoadScene("Assets/Scenes/GameScene.unity");



            yield return new WaitForSeconds(0.1f);

            string scoreTxt = GameObject.Find("Score").GetComponent<TMP_Text>().text;


            yield return new WaitForSeconds(2);

            TMP_Text scoreUpdatedText = GameObject.Find("Score").GetComponent<TMP_Text>();

            Assert.AreNotEqual(scoreTxt, scoreUpdatedText.text);
        }

        [UnityTest]
        public IEnumerator TestKunaiIsUpdatedRealTime()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.


            yield return new WaitForSeconds(1);

            Main.instance.userInfo.id = 1;
            Main.instance.userInfo.avatars_id = 1;
            Main.instance.userInfo.coin = 0;
            Main.instance.userInfo.highscore = 0;
            Main.instance.userInfo.is_skipped = true;

            SceneManager.LoadScene("Assets/Scenes/GameScene.unity");



            yield return new WaitForSeconds(0.1f);

            string kunaiTxt = GameObject.Find("KunaiCounterText").GetComponent<TMP_Text>().text;


            yield return new WaitForSeconds(2);

            TMP_Text kunaiUpdatedxt = GameObject.Find("KunaiCounterText").GetComponent<TMP_Text>();

            Assert.AreNotEqual(kunaiTxt, kunaiUpdatedxt.text);
        }

        [UnityTest]
        public IEnumerator TestPlayerIsDeadRealTime()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.


            yield return new WaitForSeconds(1);

            Main.instance.userInfo.id = 1;
            Main.instance.userInfo.avatars_id = 1;
            Main.instance.userInfo.coin = 0;
            Main.instance.userInfo.highscore = 0;
            Main.instance.userInfo.is_skipped = true;

            SceneManager.LoadScene("Assets/Scenes/GameScene.unity");



            yield return new WaitForSeconds(0.1f);

            yield return new WaitForSeconds(3.5f);

            Assert.IsTrue(PlayerController.GetIsDead());
        }
    }
}
