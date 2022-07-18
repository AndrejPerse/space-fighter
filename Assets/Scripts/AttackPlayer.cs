using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AttackPlayer is a script for attacking units. they will stop above player using EnemyUnitMovement script
//with this script units will follow player on X axis and fire bullets if player is in close enough proximity. units will also avoid meteors
public class AttackPlayer : MonoBehaviour
{
    //enemy unit properties
    protected float xMovementSpeed = 7.0f, rotationSpeed = 20.0f;

    //blast bullet properties
    public GameObject blastBullet;
    protected Vector3 leftBlastBulletSpawnOffset = new Vector3(0.5f, 0, -0.5f);
    protected Vector3 rightBlastBulletSpawnOffset = new Vector3(-0.5f, 0, -0.5f);

    //player properties
    private GameObject playerShip;
    private float playerXCoordinate;
    private float playerSearchInterval = 0.5f;

    //meteor properties
    private GameObject meteorObject;
    protected float meteorSearchInterval = 1.0f;
    private float meteorXPosition;
    protected float meteorZPosition;

    //avoid coordinates
    private float unitXMinPosition = 1.0f;
    private float unitXMaxPosition = 5.0f;
    private float unitXPosition;

    void Awake()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating(nameof(GetPlayerPosition), playerSearchInterval, playerSearchInterval); //using InvokeRepeating so units don't mimic players movements
        InvokeRepeating(nameof(SearchForMeteor), 0, meteorSearchInterval);
    }

    void Update()
    {
        //if meteor is behind the unit -> avoid
        if (meteorObject != null && transform.position.z < meteorObject.transform.position.z)
        {
            AvoidMeteor();
        }
        else
        {
            FollowPlayer();
        }
    }

    //saves players X coordinate and shoots at him if he is close enough
    protected virtual void GetPlayerPosition()
    {
        if(playerShip != null)
        {
            playerXCoordinate = playerShip.transform.position.x;
            ShootPlayer();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //follows player based on his location every search interval
    protected void FollowPlayer()
    {
        if (playerXCoordinate > transform.position.x)
        {
            transform.Translate(xMovementSpeed * Time.deltaTime * Vector3.right, Space.World);
            transform.rotation = Quaternion.Euler(0, 0, (playerXCoordinate - transform.position.x) * -rotationSpeed);
        }
        else if (playerXCoordinate < transform.position.x)
        {
            transform.Translate(xMovementSpeed * Time.deltaTime * Vector3.left, Space.World);
            transform.rotation = Quaternion.Euler(0, 0, (transform.position.x - playerXCoordinate) * rotationSpeed);
        }
    }

    //search for meteor object, get X coordinate of meteor trajectory, calculate X coordinate for the unit to move
    protected void SearchForMeteor()
    {
        meteorObject = GameObject.FindGameObjectWithTag("Meteor");
        if (meteorObject != null)
        {
            meteorXPosition = meteorObject.transform.position.x;
            unitXPosition = Random.Range(unitXMinPosition, unitXMaxPosition);
        }
    }

    //avoid meteor and move towards calculated X coordinate
    protected void AvoidMeteor()
    {
        if (meteorXPosition > 0 && transform.position.x > -unitXPosition)
        {
            transform.Translate(xMovementSpeed * Time.deltaTime * Vector3.left, Space.World);
            transform.rotation = Quaternion.Euler(0, 0, rotationSpeed * (transform.position.x + unitXPosition));
        }
        else if (meteorXPosition < 0 && transform.position.x < unitXPosition)
        {
            transform.Translate(xMovementSpeed * Time.deltaTime * Vector3.right, Space.World);
            transform.rotation = Quaternion.Euler(0, 0, rotationSpeed * (transform.position.x - unitXPosition));
        }
    }

    //shoot at player if he is close enough
    protected void ShootPlayer()
    {
        float distance = playerXCoordinate - transform.position.x;
        if (Mathf.Abs(distance) < 4.0f)
        {
            FireBlastBullet();
        }
    }

    //spawns two blast bullets
    private void FireBlastBullet()
    {
        Instantiate(blastBullet, transform.position + rightBlastBulletSpawnOffset, blastBullet.transform.rotation);
        Instantiate(blastBullet, transform.position + leftBlastBulletSpawnOffset, blastBullet.transform.rotation);
    }
}
