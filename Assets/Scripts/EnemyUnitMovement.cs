using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//script moves enemy units on Z axis (toward player) and manages their collisions and destructions
public class EnemyUnitMovement : MonoBehaviour
{
    //enemy unit properties
    protected float shipHealth = 30;
    protected float zMovementSpeed = 3.0f;
    private float shipMinPosition = 1.0f;
    protected float shipMaxPosition = 10.0f;
    protected float shipEndPosition;
    protected bool inEndPosition = false;

    //sound and visual effects
    public GameObject explosionParticlePrefab;
    private AudioManager audioManagerScript;

    //game manager
    private GameManager gameManagerScript;

    private void Awake()
    {
        audioManagerScript = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();

        CalculateEndPosition();
    }

    protected virtual void Update()
    {
        //if unit hasn't yet reached end position
        if (!inEndPosition)
        {
            MoveToEndPosition();
        }
    }

    //calculates random position on Z axis where unit will stop
    protected virtual void CalculateEndPosition()
    {
        shipEndPosition = Random.Range(shipMinPosition, shipMaxPosition);
    }

    //moves unit towards end position in Z direction
    protected virtual void MoveToEndPosition()
    {
        if (transform.position.z > shipEndPosition)
        {
            transform.Translate(zMovementSpeed * Time.deltaTime * Vector3.back, Space.World);
        }
        else if (transform.position.z <= shipEndPosition)
        {
            inEndPosition = true;
        }
    }

    //when unit is destroyed
    private void DestructionEffects()
    {
        audioManagerScript.ExplosionSound();
        Instantiate(explosionParticlePrefab, transform.position, explosionParticlePrefab.transform.rotation);
    }

    //deducts health if hit and destroys itself if health drop to 0
    private void TakeDamage()
    {
        shipHealth -= 10;
        if (shipHealth <= 0)
        {
            DestructionEffects();
            gameManagerScript.UpdateScore();
            Destroy(gameObject);
        }
    }

    //checks for collision with projectiles and meteor
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PBlast"))
        {
            TakeDamage();
        }
        else if (other.CompareTag("Rocket"))
        {
            DestructionEffects();
            gameManagerScript.UpdateScore();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Meteor"))
        {
            DestructionEffects();
            Destroy(gameObject);
        }
    }
}
