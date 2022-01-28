using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoginScript : MonoBehaviour
{
    public GameObject titleUI;
    public GameObject loginUI;
    public GameObject firstLoginUI;
    public GameObject panelUI;
    public TMPro.TMP_Text titleTxt;
    public TMPro.TMP_Text detailTxt;
    public TMPro.TMP_InputField usernameInput;
    public TMPro.TMP_InputField passwordInput;
    public Button toLoginBtn, loginBtn, closeBtn;

    Action<int> _loginCallback;

    // Start is called before the first frame update
    void Start()
    {
        _loginCallback = (actionNum) =>
        {
            //if login success
            if (actionNum == 1)
            {
                SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
            }
            //if is a new user
            else if (actionNum == 2)
            {
                Main.instance.username = usernameInput.text;
                panelUI.SetActive(false);
                ToFirstLogin();
            }
            //if invalid username or password
            else
            {
                titleTxt.text = "Error!";
                detailTxt.text = "Invalid Username Or Password";
                closeBtn.gameObject.SetActive(true);
            }
                
                
        };

        toLoginBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            ToLogin();
        });

        loginBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            Debug.Log("logging in...");
            //if the username or password field is empty
            if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
            {
                titleTxt.text = "Error!";
                detailTxt.text = "Username Or Password Cannot Be Empty!";
                panelUI.SetActive(true);
            }
            else
            {
                titleTxt.text = "Login";
                detailTxt.text = "Logging in...";
                panelUI.SetActive(true);
                closeBtn.gameObject.SetActive(false);
                StartCoroutine(Main.instance.web.Login(usernameInput.text, passwordInput.text, _loginCallback));
            }
            
        });

        Debug.Log("Application started");
        
    }

    public void BackToTitle() {
        loginUI.SetActive(false);
        titleUI.SetActive(true);
    }

    public void ToLogin() {
        titleUI.SetActive(false);
        loginUI.SetActive(true);
    }

    public void ToFirstLogin() {
        loginUI.SetActive(false);
        firstLoginUI.SetActive(true);
    }
}
