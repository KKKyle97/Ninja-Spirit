using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstLogin : MonoBehaviour
{
    public GameObject firstLoginUI;
    public GameObject characterSelectionUI;
    public GameObject panelUI;
    public TMPro.TMP_Text titleTxt;
    public TMPro.TMP_Text detailTxt;
    public Sprite[] characters;
    int selectedCharacter = 0;
    public Image characterBackground;
    public Button toCharacterSelectionUIBtn;
    public Button previousBtn;
    public Button nextBtn;
    public Button registerBtn;
    public Button closeBtn;

    public TMPro.TMP_InputField username;
    // Start is called before the first frame update
    void Start()
    {
        ChangeCharacter();

        toCharacterSelectionUIBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            ToCharacterSelection();
        });

        previousBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            PreviousCharacter();
        });

        nextBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            NextCharacter();
        });

        registerBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            Register();
        });
    }

    void ChangeCharacter() {
        characterBackground.sprite = characters[selectedCharacter];
    }

    void PreviousCharacter() {
        if (selectedCharacter == 0)
        {
            selectedCharacter = characters.Length - 1;
        }
        else {
            selectedCharacter--;
        }

        ChangeCharacter();
    }

    void NextCharacter()
    {
        if (selectedCharacter == characters.Length - 1)
        {
            selectedCharacter = 0;
        }
        else
        {
            selectedCharacter++;
        }

        ChangeCharacter();
    }

    void ToCharacterSelection() {
        //if the username is null
        if (string.IsNullOrEmpty(username.text))
        {
            titleTxt.text = "Error!";
            detailTxt.text = "Name Cannot Be Empty!";
            panelUI.SetActive(true);
            closeBtn.gameObject.SetActive(true);
        }
        else
        {
            firstLoginUI.SetActive(false);
            characterSelectionUI.SetActive(true);
        }
        
    }

    void Register() {
        System.Action<bool> ToMainMenuCallback = (isRegistered) =>
        {
            Debug.Log("in register callback");
            if (isRegistered) {
                SceneManager.LoadScene("MainMenuScene",LoadSceneMode.Single);
            }   
        };

        titleTxt.text = "Login";
        detailTxt.text = "Registering...";
        panelUI.SetActive(true);
        closeBtn.gameObject.SetActive(false);


        StartCoroutine(Main.instance.web.Register(Main.instance.username, username.text, selectedCharacter+1, ToMainMenuCallback));
    }
}
