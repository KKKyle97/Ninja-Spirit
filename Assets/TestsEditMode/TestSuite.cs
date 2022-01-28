using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEditor.SceneManagement;
using TMPro;
using UnityEditor;

namespace Tests
{
    public class TestSuite
    {

        [SetUp]
        public void Setup()
        {
            // We delete everything from PlayerPrefs before every test
            EditorSceneManager.OpenScene("Assets/Scenes/GameScene.unity");
        }

        [Test]
        public void TestDefaultCoinAmount()
        {
            TMP_Text coinCounterText = GameObject.Find("CoinCounterText").GetComponent<TMP_Text>();

            Assert.AreEqual(coinCounterText.text, "X 0");
        }

        [Test]
        public void TestDefaultKunaiAmount()
        {
            TMP_Text kunaiCounterText = GameObject.Find("KunaiCounterText").GetComponent<TMP_Text>();

            Assert.AreEqual(kunaiCounterText.text, "X 0");
        }

        [Test]
        public void TestDefaultScore()
        {
            TMP_Text scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();

            Assert.AreEqual(scoreText.text, "score: 0");
        }

        [Test]
        public void TestEnemyDefaultHealth()
        {
            GameObject prefab = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Prefab/Enemy.prefab", typeof(GameObject));

            Enemy enemy = prefab.GetComponent<Enemy>();
        }
    }
}
