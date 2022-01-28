using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProfileScript : MonoBehaviour
{
    public Sprite[] characters;

    public Image character;
    public TMPro.TMP_Text profileName;
    public TMPro.TMP_Text coin;
    public TMPro.TMP_Text highscore;
    public TMPro.TMP_Text badge;

    public GameObject statusUI;
    public TMPro.TMP_Text titleTxt;
    public TMPro.TMP_Text detailTxt;

    public Button avatarBtn;
    public Button badgeBtn;
    public Button closeBtn;
    Action<bool> _loadProfileCallback;
    // Start is called before the first frame update
    void Start()
    {
        _loadProfileCallback = (isSuccess) => {
            statusUI.SetActive(false);
            character.sprite = characters[Main.instance.userInfo.avatars_id-1];
            profileName.text = Main.instance.userInfo.name;
            coin.text = Main.instance.userInfo.coin.ToString();
            highscore.text = Main.instance.userInfo.highscore.ToString();
            badge.text = Main.instance.userInfo.badges_count.ToString();
        };

        titleTxt.text = "Status";
        detailTxt.text = "Loading...";
        closeBtn.gameObject.SetActive(false);
        statusUI.SetActive(true);

        avatarBtn.onClick.AddListener(ToAvatarScene);
        badgeBtn.onClick.AddListener(ToBadgeScene);
        StartCoroutine(Main.instance.web.LoadProfile(Main.instance.userInfo.id, _loadProfileCallback));
    }

    void ToAvatarScene() {
        SFXScript.instance.PlaySound("Tap");
        SceneManager.LoadScene("AvatarScene",LoadSceneMode.Single);
    }

    void ToBadgeScene() {
        SFXScript.instance.PlaySound("Tap");
        SceneManager.LoadScene("BadgeScene",LoadSceneMode.Single);
    }
}
