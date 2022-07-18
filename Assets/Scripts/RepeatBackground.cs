using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//moves background and repeats it so background looks endless
public class RepeatBackground : MonoBehaviour
{
    private Vector3 startLocation = new Vector3(0, 0, 20);
    private float repeatHeight = 92.16f;
    private float speed = 10.0f;

    void Update()
    {
        if (transform.position.z < (startLocation.z - repeatHeight))
        {
            transform.position = startLocation;
        }
        transform.Translate(speed * Time.deltaTime * Vector3.back);
    }
}
