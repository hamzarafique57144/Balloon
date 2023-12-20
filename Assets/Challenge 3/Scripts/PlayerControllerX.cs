using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver=false;

    public float floatForce;
    private float gravityModifier = .8f;
    public Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb =GameObject.Find("Player"). GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
     //   playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 14.16f)
        {
            transform.position = new Vector3(transform.position.x, 14.16f, transform.position.z);
        }
        
        if (Input.GetKey(KeyCode.Space))
        {
            floating();
            
        }
        
    }
    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
        else if(other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * 55);
        }
        else if(other.gameObject.CompareTag("Bound"))
        {
            playerRb.AddForce(Vector3.down * 5);
        }

    }
    private void floating()
    {
       
        // While space is pressed and player is low enough, float up
       
            playerRb.AddForce(Vector3.up * floatForce);
            //  transform.Translate(0, 3, 0);
            Debug.Log("Force Applied");
        
    }
}
