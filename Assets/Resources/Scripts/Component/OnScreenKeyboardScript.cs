using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenKeyboardScript : MonoBehaviour
{

    public void openKeyboard() {
        Debug.Log("hello");
        TouchScreenKeyboard.Open("",TouchScreenKeyboardType.NamePhonePad);
    }
}
