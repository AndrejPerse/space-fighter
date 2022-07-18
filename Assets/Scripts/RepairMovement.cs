using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//movement of repair object
public class RepairMovement : MonoBehaviour
{
    private float movementSpeed = 5.0f, rotationSpeed = 0.3f;
    protected float botBound = -15.0f;

    void Update()
    {
        ObjectMovement();
        CheckBound();
    }

    protected virtual void ObjectMovement()
    {
        transform.Translate(Vector3.back * movementSpeed * Time.deltaTime, Space.World);
        transform.Rotate(rotationSpeed, rotationSpeed, rotationSpeed);
    }

    void CheckBound()
    {
        if (transform.position.z < botBound)
        {
            Destroy(gameObject);
        }
    }
}
