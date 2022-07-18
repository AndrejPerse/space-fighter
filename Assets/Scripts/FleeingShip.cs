using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fleeing ship iherits from EnemyUnitMovement class. it also moves on X axis to avoid incoming meteors and only shoots rockets at player.
// INHERITANCE
public class FleeingShip : EnemyUnitMovement
{
    //ship properties
    private float xMovementSpeed = 10.0f, rotationSpeed = 7.0f;

    //enemy ship weapon system
    public GameObject enemyRocket;
    private Vector3 leftRocketSpawnOffset = new Vector3(1.5f, 0, -1.3f);
    private Vector3 rightRocketSpawnOffset = new Vector3(-1.5f, 0, -1.3f);
    private bool rightRocketReady;
    private float rocketRechargeRate = 3.0f;

    //meteor properties
    private GameObject[] meteorObjects;
    private float meteorSearchInterval = 1.0f;
    private float meteorXPosition;

    //avoid coordinates
    private float unitXMinPosition = 1.0f;
    private float unitXMaxPosition = 5.0f;
    private float unitXPosition;

    void Start()
    {
        shipHealth = 100;
        InvokeRepeating(nameof(FireRocket), rocketRechargeRate, rocketRechargeRate);
        InvokeRepeating(nameof(SearchForMeteor), 0, meteorSearchInterval);
    }

    // POLYMORPHISM
    protected override void Update()
    {
        base.Update();
        if (inEndPosition)
        {
            Destroy(gameObject);
        }
        AvoidMeteor();
    }

    // POLYMORPHISM
    protected override void CalculateEndPosition()
    {
        shipEndPosition = -15.0f;
    }

    //search for meteor object, get X coordinate of meteor trajectory, calculate X coordinate for the unit to move
    protected void SearchForMeteor()
    {
        meteorObjects = GameObject.FindGameObjectsWithTag("Meteor");
        foreach(GameObject meteor in meteorObjects)
        {
            if (meteor.transform.position.z > transform.position.z)
            {
                meteorXPosition = meteor.transform.position.x;
                unitXPosition = Random.Range(unitXMinPosition, unitXMaxPosition);
            }
        }
    }

    //avoid meteor and move towards calculated X coordinate
    protected void AvoidMeteor()
    {
        if (meteorXPosition > 0 && transform.position.x > -unitXPosition)
        {
            transform.Translate(xMovementSpeed * Time.deltaTime * Vector3.left, Space.World);
            transform.rotation = Quaternion.Euler(0, 180, (unitXPosition + transform.position.x) * -rotationSpeed);
        }
        else if (meteorXPosition < 0 && transform.position.x < unitXPosition)
        {
            transform.Translate(xMovementSpeed * Time.deltaTime * Vector3.right, Space.World);
            transform.rotation = Quaternion.Euler(0, 180, (unitXPosition - transform.position.x) * rotationSpeed);
        }
    }

    //spawns rocket, alternating between left and right
    void FireRocket()
    {
        if (rightRocketReady)
        {
            Instantiate(enemyRocket, transform.position + rightRocketSpawnOffset, enemyRocket.transform.rotation);
            rightRocketReady = false;
        }
        else
        {
            Instantiate(enemyRocket, transform.position + leftRocketSpawnOffset, enemyRocket.transform.rotation);
            rightRocketReady = true;
        }
    }
}
