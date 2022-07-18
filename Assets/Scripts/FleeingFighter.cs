using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//fleeing fighter inherits from EnemyUnitMovement claas. Script makes fighter moves left and right like a sinus function and does not fire at player
// INHERITANCE
public class FleeingFighter : EnemyUnitMovement
{
    private float sinusCenterX;

    void Start()
    {
        sinusCenterX = transform.position.x;
    }

    //change end position to position behind the player
    protected override void CalculateEndPosition()
    {
        shipEndPosition = -15.0f;
    }

    protected override void MoveToEndPosition()
    {
        base.MoveToEndPosition();
        //moves fighter in X direction with sinus function
        Vector3 currentPosition = transform.position;
        float sinus = Mathf.Sin(currentPosition.z) * zMovementSpeed;
        currentPosition.x = sinusCenterX + sinus;
        transform.position = currentPosition;
        //destroy unit if it has passed player
        if (inEndPosition)
        {
            Destroy(gameObject);
        }
    }
}
