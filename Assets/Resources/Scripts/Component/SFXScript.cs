using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    public AudioClip throwSound, jumpSound, collectSound, hitSound, loseSound, tapSound;
    public GameObject bgMusic;
    public static SFXScript instance;
    private AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        throwSound = Resources.Load<AudioClip>("Audio/Throw");
        jumpSound = Resources.Load<AudioClip>("Audio/Jump");
        collectSound = Resources.Load<AudioClip>("Audio/Coin");
        hitSound = Resources.Load<AudioClip>("Audio/Hit");
        loseSound = Resources.Load<AudioClip>("Audio/Lose");
        tapSound = Resources.Load<AudioClip>("Audio/Tap");

        audioSrc = GetComponent<AudioSource>();

        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(string clip) {
        switch (clip) {
            case "Throw":
                audioSrc.PlayOneShot(throwSound);
                break;
            case "Jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "Collect":
                audioSrc.PlayOneShot(collectSound);
                break;
            case "Hit":
                audioSrc.PlayOneShot(hitSound);
                break;
            case "Lose":
                Destroy(bgMusic);
                audioSrc.PlayOneShot(loseSound);
                break;
            case "Tap":
                audioSrc.PlayOneShot(tapSound);
                break;
        }
    }
}
