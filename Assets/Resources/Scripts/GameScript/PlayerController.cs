using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    public static PlayerController instance;
    private Rigidbody2D rb2d;
    public Animator animator;
    public SpriteRenderer playerSprite;

    public Sprite[] characterSprites;
    public float jumpVelo;
    public float runVelo;
    private BoxCollider2D boxCollider2D;
    private Touch theTouch;
    private static bool isDead;
    private bool isDoubleJumped;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;

        animator = AnimatorControllerScript.instance.SelectAnimatorController(animator, Main.instance.userInfo.avatars_id - 1);
        playerSprite.sprite = characterSprites[Main.instance.userInfo.avatars_id-1];

        rb2d = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        isDead = false;
        isDoubleJumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false && (Time.timeScale != 0 || Time.timeScale != 0f) ) {
            if (Input.touchCount > 0) {
                theTouch = Input.GetTouch(0);

                if (IsGrounded() && theTouch.phase == TouchPhase.Began && (theTouch.position.x < Screen.width / 2 && theTouch.position.y < Screen.height / 1.3))
                {
                    rb2d.velocity = new Vector2(runVelo, jumpVelo);
                    animator.SetBool("isJumping", true);
                    SFXScript.instance.PlaySound("Jump");
                }
                else if (!IsGrounded() && !isDoubleJumped && theTouch.phase == TouchPhase.Began && (theTouch.position.x < Screen.width / 2 && theTouch.position.y < Screen.height / 1.3))
                {
                    rb2d.velocity = new Vector2(runVelo, jumpVelo);
                    animator.SetBool("isJumping", true);
                    SFXScript.instance.PlaySound("Jump");
                    isDoubleJumped = true;
                }
            }
            else if (IsGrounded())
            {
                rb2d.velocity = new Vector2(runVelo, rb2d.velocity.y);
                animator.SetFloat("speed", rb2d.velocity.x);
                animator.SetBool("isJumping", false);
                isDoubleJumped = false;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            SFXScript.instance.PlaySound("Collect");
        }
        else if (collision.gameObject.CompareTag("DeadZone"))
        {
            SFXScript.instance.PlaySound("Lose");
            isDead = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Obstacle") && isDead == false) {
            Kill();
        }
    }

    public bool IsGrounded() {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }

    public void Kill()
    {
        rb2d.velocity = new Vector2(0,0);
        animator.SetBool("isDead", true);
        Debug.Log("player is dead");
        SFXScript.instance.PlaySound("Lose");
        isDead = true;
    }

    public static bool GetIsDead() {
        return isDead;
    }

    public static Vector3 GetPosition() 
    {
        return GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
