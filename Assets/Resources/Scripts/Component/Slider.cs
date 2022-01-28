using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slider : MonoBehaviour
{
    public TMPro.TMP_Text valueTxt;
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateValue(float value) 
    {
        valueTxt.text = value.ToString();
    }
}
