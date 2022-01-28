using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject kunaiPrefab;
    public Animator animator;
    private Touch theTouch;

    private void Start()
    {
        animator = AnimatorControllerScript.instance.SelectAnimatorController(animator, Main.instance.userInfo.avatars_id-1);
        Debug.Log(animator.runtimeAnimatorController);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameOverScript.isInputEnabled && (Time.timeScale != 0f || Time.timeScale != 0)) {
            if (Input.touchCount > 0)
            {
                theTouch = Input.GetTouch(0);
                if ((theTouch.position.x > Screen.width / 2 && theTouch.position.y < Screen.height / 1.3) && animator.GetCurrentAnimatorStateInfo(0).IsTag("run") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("throw"))
                {
                    //shoot method
                    if (ScoreManager.instance.GetKunaiScore() != 0)
                    {
                        animator.SetBool("isThrowing", true);
                        SFXScript.instance.PlaySound("Throw");
                        Debug.Log("normal shooting");
                        Shoot();
                    }

                }
                else if ((theTouch.position.x > Screen.width / 2 && theTouch.position.y < Screen.height / 1.3) && animator.GetCurrentAnimatorStateInfo(0).IsTag("jump") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("jumpThrow"))
                {
                    if (ScoreManager.instance.GetKunaiScore() != 0)
                    {
                        animator.SetBool("isJumpThrowing", true);
                        SFXScript.instance.PlaySound("Throw");
                        Debug.Log("jumpshooting");
                        Shoot();
                    }
                }
            }
            else
            {
                animator.SetBool("isThrowing", false);
                animator.SetBool("isJumpThrowing", false);
            }
        }


    }

    void Shoot()
    {
        Instantiate(kunaiPrefab, firePoint.position, kunaiPrefab.transform.rotation);
        ScoreManager.instance.MinusScore();
    }
}
