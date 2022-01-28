using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20f;
    public Rigidbody2D rb2d;
    public float destroyDistance;

    private Vector3 kunaiInitialPosition;
    void Start()
    {
        rb2d.velocity = transform.up * speed;
        kunaiInitialPosition = gameObject.transform.position;
        
    }

    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, kunaiInitialPosition) > destroyDistance) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null) {
            SFXScript.instance.PlaySound("Hit");
            enemy.TakeDamage(60);
            Destroy(gameObject);
        }
    }
}
