using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for objects to move forward, destroys when above or bellow boundary or when it collides
public class MoveForward : MonoBehaviour
{
    protected float movementSpeed;
    protected float topBound = 13.0f, botBound = -15.0f;

    //set movement speed based on projectile (enemy blast bullet need negative value, so it moves backwards)
    private void Start()
    {
        if (gameObject.tag == "PBlast")
        {
            movementSpeed = 40.0f;
        }
        else if (gameObject.tag == "EBlast")
        {
            movementSpeed = -20.0f;
        }
        else if (gameObject.tag == "Rocket")
        {
            movementSpeed = 30.0f;
        }
    }
    private void Update()
    {
        ObjectMovement();
        CheckBound();
    }

    protected virtual void ObjectMovement()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime, Space.World);
    }

    private void CheckBound()
    {
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < botBound)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
