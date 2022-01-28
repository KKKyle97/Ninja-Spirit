using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    [SerializeField] private Transform levelPart;
    private Vector3 endPosition;
    private bool isDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        endPosition = levelPart.Find("EndPosition").position;
        isDestroyed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.GetPosition().x > endPosition.x && isDestroyed == false)
        {
            StartCoroutine(ExecuteAfterTime(1f));
        } 
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        // Code to execute after the delay
        Destroy(gameObject);
        isDestroyed = true;
        
    }
}
