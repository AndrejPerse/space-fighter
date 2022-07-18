using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//meteor inherits from MoveForward class.
// INHERITANCE
public class MeteorMovement : MoveForward
{
    //meteor size properties
    private float size;
    private float minSize = 0.3f;
    private Vector3 sizeChange = new Vector3(0.1f, 0.1f, 0.1f);

    //meteor movement properties
    private Vector3 rotation;
    private float maxRotationSpeed = 0.1f;

    //visual and sound effects
    public GameObject explosionParticlePrefab;
    private AudioManager audioManagerScript;

    private void Start()
    {
        audioManagerScript = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        PrepareRandomMeteor();
    }

    private void PrepareRandomMeteor()
    {
        movementSpeed = 10.0f;
        topBound = 24.0f;
        //calculate random size
        size = Random.Range(0.7f, 1.0f);
        transform.localScale *= size;
        //calculate random rotation
        float rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        rotation = new Vector3(rotationSpeed, rotationSpeed, rotationSpeed);
    }

    // POLYMORPHISM
    protected override void ObjectMovement()
    {
        transform.Translate(Vector3.back * movementSpeed * Time.deltaTime, Space.World);
        transform.Rotate(rotation);
    }

    //when meteor is destroyed
    private void DestructionEffects()
    {
        audioManagerScript.ExplosionSound();
        Instantiate(explosionParticlePrefab, transform.position, explosionParticlePrefab.transform.rotation);
    }

    //meteor loses size if hit by bullets
    private void TakeDamage()
    {
        size -= 0.1f;
        transform.localScale -= sizeChange;
        if (size < minSize)
        {
            DestructionEffects();
            Destroy(gameObject);
        }
    }

    // POLYMORPHISM
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PBlast") || other.CompareTag("EBlast"))
        {
            TakeDamage();
        }
        else if (other.CompareTag("Rocket") || other.CompareTag("ERocket"))
        {
            DestructionEffects();
            Destroy(gameObject);
        }
        else if (other.CompareTag("Repair"))
        {
            //Ignore collision
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
