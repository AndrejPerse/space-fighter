using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attacking ship inherits from AttackPlayer class. It will also shoot rockets.
// INHERITANCE
public class AttackingShip : AttackPlayer
{
    //rocket properties
    public GameObject enemyRocket;
    private Vector3 leftRocketSpawnOffset = new Vector3(1.5f, 0, -1.3f);
    private Vector3 rightRocketSpawnOffset = new Vector3(-1.5f, 0, -1.3f);
    private bool rightRocketReady;
    private float rocketRechargeRate = 3.0f;

    void Start()
    {
        PrepareAttackingShip();
        InvokeRepeating(nameof(FireRocket), rocketRechargeRate, rocketRechargeRate);
    }

    //change properties for the ship
    private void PrepareAttackingShip()
    {
        rotationSpeed = 7.0f;
        rightRocketReady = true;
        leftBlastBulletSpawnOffset = new Vector3(1.15f, 0, -2f);
        rightBlastBulletSpawnOffset = new Vector3(-1.15f, 0, -2f);
    }

    //spawns rocket, alternating between left and right
    void FireRocket()
    {
        if (rightRocketReady)
        {
            Instantiate(enemyRocket, transform.position + rightRocketSpawnOffset, blastBullet.transform.rotation);
            rightRocketReady = false;
        }
        else
        {
            Instantiate(enemyRocket, transform.position + leftRocketSpawnOffset, blastBullet.transform.rotation);
            rightRocketReady = true;
        }
    }
}
