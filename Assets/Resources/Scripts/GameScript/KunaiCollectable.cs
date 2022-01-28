using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiCollectable : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) ScoreManager.instance.AddScore(1,"kunai");
    }
}
