using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class SelectBodyPartScript : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject nextPanel;
    public GameObject errorPanel;
    public GameObject moodPanel;
    public GameObject particle;
    private GameObject currentParticle;
    public GameObject initiator;
    public TMPro.TMP_Text text;
    public TMPro.TMP_Text errorText;
    public Button saveBtn;
    public Button noneBtn;
    private string selectedBodyPart;
    private string colliderName;
    void Start()
    {
        saveBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            if (string.IsNullOrEmpty(selectedBodyPart))
            {
                errorText.text = "Please Select A Body Part";
                errorPanel.SetActive(true);
            }
            else
            {
                Main.instance.report.body = selectedBodyPart;
                currentPanel.SetActive(false);
                nextPanel.SetActive(true);
            }
        });

        noneBtn.onClick.AddListener(() => {
            SFXScript.instance.PlaySound("Tap");
            Main.instance.report.body = "None";
            Main.instance.report.level = 0;
            Main.instance.report.duration = 0;
            Main.instance.report.description = "None";
            currentPanel.SetActive(false);
            moodPanel.SetActive(true);
        });
    }

    void Update()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                // Construct a ray from the current touch coordinates
                Vector2 ray = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);

                RaycastHit2D raycast = Physics2D.Raycast(ray,Input.GetTouch(i).position);
                // Create a particle if hit
                if (raycast) {
                    SFXScript.instance.PlaySound("Tap");
                    Debug.Log(raycast.collider.name);
                    if (currentParticle != null)
                    {
                        Destroy(currentParticle);
                        if (raycast.collider.name == colliderName)
                        {
                            colliderName = "";
                            text.text = colliderName;
                        }
                        else
                        {
                            currentParticle = Instantiate(particle, raycast.collider.transform.position, initiator.transform.rotation, currentPanel.transform);

                            ChangeText(raycast.collider.name);
                            colliderName = raycast.collider.name;
                        }
                    }
                    else {
                        currentParticle = Instantiate(particle, raycast.collider.transform.position, initiator.transform.rotation, currentPanel.transform);
                        colliderName = raycast.collider.name;
                        ChangeText(raycast.collider.name);
                    }
                }
            }
        }
    }

    void ChangeText(string name)
    {
        switch (name)
        {
            case "Head":
                text.text = selectedBodyPart = "Head";
                break;

            case "LeftHand":
                text.text = selectedBodyPart = "Left Hand";
                break;

            case "RightHand":
                text.text = selectedBodyPart = "Right Hand";
                break;

            case "LeftChest":
                text.text = selectedBodyPart = "Left Chest";
                break;

            case "RightChest":
                text.text = selectedBodyPart = "Right Chest";
                break;

            case "Stomach":
                text.text = selectedBodyPart = "Stomach";
                break;

            case "LeftLeg":
                text.text = selectedBodyPart = "Left Leg";
                break;

            case "RightLeg":
                text.text = selectedBodyPart = "Right Leg";
                break;

            case "LeftFoot":
                text.text = selectedBodyPart = "Left Foot";
                break;

            case "RightFoot":
                text.text = selectedBodyPart = "Right Foot";
                break;
        }
    }
}