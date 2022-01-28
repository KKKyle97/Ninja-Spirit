using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControllerScript : MonoBehaviour
{
    // Start is called before the first frame update
    private static SFXControllerScript instance = null;
    public static SFXControllerScript Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
      
        instance = this;
       

        DontDestroyOnLoad(this.gameObject);
    }
}
