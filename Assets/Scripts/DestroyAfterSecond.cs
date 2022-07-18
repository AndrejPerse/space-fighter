using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script for explosion particle to destroy itself after determined time
public class DestroyAfterSecond : MonoBehaviour
{
    private float aliveTime = 1.1f;

    void Start()
    {
        StartCoroutine(DestroyItself());
    }

    IEnumerator DestroyItself()
    {
        yield return new WaitForSeconds(aliveTime);
        Destroy(gameObject);
    }
}
