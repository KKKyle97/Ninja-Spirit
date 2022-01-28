using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AnimatorControllerScript : MonoBehaviour
{
    public Animator selectedAnimator;
    public static AnimatorControllerScript instance;
    private string[] controllerNames = { "AnimationController", "AnimationController2","Character3Controller", "Character4Controller" , "Character5Controller" , "Character6Controller" };
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public Animator SelectAnimatorController(Animator animator, int controllerId) {
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animations/Character/"+controllerNames[controllerId]);
        Debug.Log(animator);
        Time.timeScale = 0f;
        return animator;
    }
}
