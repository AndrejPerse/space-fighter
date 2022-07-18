using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//player controls a ship that can move left and right with arrow keys, shoot bullets with space and rocket with B buttons
public class PlayerController : MonoBehaviour
{
    //ship properties
    public float shipHealth = 100;
    private float movementSpeed = 10.0f, rotationSpeed = 20.0f;
    private float xConstraint = 5.0f;
    private float horizontalInput;
    
    //blast bullet properties
    public GameObject blastBullet;
    private Vector3 rightBlastBulletSpawnOffset = new Vector3(0.9f, 0, 2.0f);
    private Vector3 leftBlastBulletSpawnOffset = new Vector3(-0.9f, 0, 2.0f);

    //rocket properties
    public GameObject rocket;
    private Vector3 rocketSpawnOffset = new Vector3(-1.142f, 0, 1.5f);
    private bool rocketReady;
    private float rocketRechargeRate = 3.0f;
    private GameObject visibleRocket; //shown on top of the wing

    //sound and visual effects
    private AudioSource playerAudio;
    public AudioClip repairSound;
    private float soundVolume = 0.5f;
    private AudioManager audioManagerScript;
    public GameObject explosionParticlePrefab;

    //game manager
    private GameManager gameManagerScript;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        audioManagerScript = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();

        visibleRocket = GameObject.Find("Rocket_Missile");
        rocketReady = true;
    }

    void Update()
    {
        ConstrainMovement();
        MovePlayer();
        CheckFireInput();
    }

    void MovePlayer()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(horizontalInput * movementSpeed * Time.deltaTime * Vector3.right, Space.World);
        transform.rotation = Quaternion.Euler(0, 180, horizontalInput * rotationSpeed);
    }

    void ConstrainMovement()
    {
        if (transform.position.x < -xConstraint)
        {
            transform.position = new Vector3(-xConstraint, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xConstraint)
        {
            transform.position = new Vector3(xConstraint, transform.position.y, transform.position.z);
        }
    }

    void CheckFireInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBlastBullet();
        }
        if (Input.GetKeyDown(KeyCode.B) && rocketReady)
        {
            FireRocket();
        }
    }

    void FireBlastBullet()
    {
        Instantiate(blastBullet, transform.position + rightBlastBulletSpawnOffset, blastBullet.transform.rotation);
        Instantiate(blastBullet, transform.position + leftBlastBulletSpawnOffset, blastBullet.transform.rotation);
    }

    void FireRocket()
    {
        rocketReady = false;
        visibleRocket.SetActive(false);
        Instantiate(rocket, transform.position + rocketSpawnOffset, rocket.transform.rotation);
        audioManagerScript.RocketLaunchSound();
        StartCoroutine(PrepareRocket());
    }

    //rocket available based on rocket recharge rate
    IEnumerator PrepareRocket()
    {
        yield return new WaitForSeconds(rocketRechargeRate);
        visibleRocket.SetActive(true); 
        rocketReady = true;
    }

    //when player is defeated
    private void DestructionEffects()
    {
        audioManagerScript.ExplosionSound();
        Instantiate(explosionParticlePrefab, transform.position, explosionParticlePrefab.transform.rotation);
        gameManagerScript.GameOver();
    }

    //collisions manager
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EBlast"))
        {
            shipHealth -= 10;
            if (shipHealth <= 0)
            {
                DestructionEffects();
                Destroy(gameObject);

            }
        }
        else if (other.CompareTag("ERocket"))
        {
            shipHealth -= 60;
            if (shipHealth <= 0)
            {
                DestructionEffects();
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Meteor") || other.CompareTag("EShip") || other.CompareTag("EFighter"))
        {
            DestructionEffects();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Repair"))
        {
            Destroy(other.gameObject);
            shipHealth = 100;
            playerAudio.PlayOneShot(repairSound, soundVolume);
        }
    }
}
