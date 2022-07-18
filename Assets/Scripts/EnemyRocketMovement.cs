using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enemy rockets inherit from MoveForward class. when rocket is instantiated it will fly toward player
// INHERITANCE
public class EnemyRocketMovement : MoveForward
{
    //player properties
    private GameObject playerShip;
    private Vector3 shootingDirection;

    void Start()
    {
        movementSpeed = 20.0f;
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            CalculateShootingDirection();
        }
        else
        {
            Destroy(gameObject); //destroys rockets on spawn after game over
        }
    }

    private void CalculateShootingDirection()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        shootingDirection = playerShip.transform.position - transform.position;
        //turns rocket towards player
        float shootingRotation = Mathf.Atan(shootingDirection.x / shootingDirection.z) * 180 / Mathf.PI;
        transform.Rotate(0, shootingRotation, 0);
    }

    protected override void ObjectMovement()
    {
        transform.position += shootingDirection * Time.deltaTime;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meteor") || other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
