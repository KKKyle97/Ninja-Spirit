using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvatarScript : MonoBehaviour
{
    public Sprite[] characters;
    public Image character;

    public Button[] characterBtn;
    public Button selectBtn;
    public Button unlockBtn;
    
    public Image selectedTxt;

    public GameObject lockedPanel;
    public GameObject statusUI;
    public TMPro.TMP_Text titleTxt;
    public TMPro.TMP_Text statusTxt;
    public Button closeBtn;

    public TMPro.TMP_Text nameTxt;
    public TMPro.TMP_Text priceTxt;

    //purchase panel
    public GameObject purchasePanel;
    public TMPro.TMP_Text userCoinTxt;
    public TMPro.TMP_Text avatarPriceTxt;
    public Button purchaseBtn;

    //success noti
    public GameObject noti;
    public TMPro.TMP_Text notiTxt;

    //badge panel
    public GameObject badgePanel;

    public int avatarCount;

    private int initialSelection;
    private int currentSelection;
    private int finalSelection;

    private IDictionary<int, string> avatarName = new Dictionary<int, string>();
    private IDictionary<int, int> avatarPrice = new Dictionary<int, int>();

    private ArrayList allAvatarsId = new ArrayList();

    Action<bool> _loadAvatarCallback;
    Action<bool> _updateAvatarCallback;
    Action<bool> _unlockAvatarBadgeCallback;




    // Start is called before the first frame update
    void Start()
    {
        //callback to load the avatar scene when the new user profile info has been loaded successfully
        _loadAvatarCallback = (isCorrect) =>
        {
            if (isCorrect)
            {
                statusUI.SetActive(false);
                DisplayNotification();
                InitializeAvatar();

                //add unlocked avatar id to the arraylist
                foreach (Avatar avatar in Main.instance.avatars)
                {
                    allAvatarsId.Add(avatar.avatar_id);
                }

                //initialize current avatar
                //set avatar id to initial selection
                initialSelection = Main.instance.userInfo.avatars_id;
                //set avatar image
                character.sprite = characters[initialSelection - 1];
                //selected avatar name
                nameTxt.text = avatarName[initialSelection-1];
                //price tag if unlocked
                priceTxt.text = "unlocked";
                //set initial selection to current selection
                currentSelection = initialSelection;

                //setting buttons
                for (int i = 0; i < avatarCount; i++)
                {
                    int avatarCurrentNumber = i;

                    //initialize avatar button function
                    characterBtn[avatarCurrentNumber].onClick.AddListener(() => {

                        SFXScript.instance.PlaySound("Tap");

                        //set sprite to respective button
                        character.sprite = characters[avatarCurrentNumber];
                        nameTxt.text = avatarName[avatarCurrentNumber];
                        // + 1 because id starts from 1 not 0
                        currentSelection = avatarCurrentNumber + 1;

                        //check avatar id if match the avatar
                        if (allAvatarsId.Contains(avatarCurrentNumber + 1))
                        {
                            Debug.Log("avatar exists");
                            lockedPanel.SetActive(false);
                            priceTxt.text = "unlocked";

                            currentSelection = avatarCurrentNumber+1;

                            if (initialSelection == currentSelection)
                            {
                                selectedTxt.gameObject.SetActive(true);
                                selectBtn.gameObject.SetActive(false);
                                unlockBtn.gameObject.SetActive(false);
                            }
                            else 
                            {
                                selectedTxt.gameObject.SetActive(false);
                                selectBtn.gameObject.SetActive(true);
                                unlockBtn.gameObject.SetActive(false);
                            }
                        }
                        else
                        {
                            Debug.Log("avatar not exists");
                            priceTxt.text = avatarPrice[avatarCurrentNumber].ToString();
                            selectedTxt.gameObject.SetActive(false);
                            selectBtn.gameObject.SetActive(false);
                            unlockBtn.gameObject.SetActive(true);
                            lockedPanel.SetActive(true);
                        }
                    });
                }

                selectBtn.onClick.AddListener(() => {
                    SFXScript.instance.PlaySound("Tap");
                    SelectAvatar();
                });

                unlockBtn.onClick.AddListener(() => {
                    SFXScript.instance.PlaySound("Tap");
                    UnlockAvatar();
                });
            }
        };

        //callback when the avatar has been updated
        _updateAvatarCallback = (isCorrect) =>
                {
                    purchasePanel.SetActive(false);
                    noti.SetActive(true);
                    //set selected avatar to user info object
                    Main.instance.userInfo.avatars_id = finalSelection;

                    Debug.Log("refreshing scene");
                    //refresh scene after 1 sec
                    StartCoroutine(ExecuteAfterTime(1));
                };

        //callback to check if there is new avatar badge to be unlocked when a new avatar is unlocked
        _unlockAvatarBadgeCallback = (isCorrect) => {

            if (isCorrect) {
                notiTxt.text = "Purchased Successful!";
                Debug.Log("Purchased");
                StartCoroutine(Main.instance.web.UnlockAvatarBadge(Main.instance.userInfo.patient_accounts_id, _updateAvatarCallback));
            } 
        };

        //callback to get all the available avatar

        statusUI.SetActive(true);
        titleTxt.text = "status";
        statusTxt.text = "Loading...";
        closeBtn.gameObject.SetActive(false);

        StartCoroutine(Main.instance.web.GetAvatars(Main.instance.userInfo.patient_accounts_id, _loadAvatarCallback));

        //update profile detail to get the latest avatar id
        //StartCoroutine(Main.instance.web.LoadProfile(Main.instance.userInfo.id,_loadProfileCallback));
    }

    void SelectAvatar()
    {
        finalSelection = currentSelection;
        notiTxt.text = "Avatar Changed!"; 
        StartCoroutine(Main.instance.web.ChangeAvatar(Main.instance.userInfo.id, finalSelection, _updateAvatarCallback));
    }

    void UnlockAvatar()
    {
        finalSelection = currentSelection;
        userCoinTxt.text = Main.instance.userInfo.coin.ToString();
        avatarPriceTxt.text = avatarPrice[finalSelection - 1].ToString();
        purchasePanel.SetActive(true);

        purchaseBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            //if the user coin is more than the price
            if (Main.instance.userInfo.coin >= avatarPrice[finalSelection - 1]) {
                StartCoroutine(Main.instance.web.UnlockAvatar(Main.instance.userInfo.id, finalSelection, Main.instance.userInfo.patient_accounts_id, Main.instance.userInfo.coin - avatarPrice[finalSelection-1], _unlockAvatarBadgeCallback));
            }
            else
            {
                Debug.Log("no money");
                StartCoroutine(InsufficientCoinNoti());
            }
            
        });
    }

    void DisplayNotification()
    {
        if (Main.instance.hasNewUnlock)
        {
            badgePanel.SetActive(true);
            Main.instance.hasNewUnlock = false;
        }
    }

    void InitializeAvatar()
    {
        avatarName.Add(0, "Ash");
        avatarName.Add(1, "Jean");
        avatarName.Add(2, "Ken");
        avatarName.Add(3, "Shiba");
        avatarName.Add(4, "Macy");
        avatarName.Add(5, "Ein");

        avatarPrice.Add(0, 0);
        avatarPrice.Add(1, 0);
        avatarPrice.Add(2, 100);
        avatarPrice.Add(3, 200);
        avatarPrice.Add(4, 300);
        avatarPrice.Add(5, 400);
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        // Code to execute after the delay
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        
    }

    IEnumerator InsufficientCoinNoti()
    {
        notiTxt.text = "Insufficient Coin!";
        noti.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        noti.SetActive(false);

    }
}
