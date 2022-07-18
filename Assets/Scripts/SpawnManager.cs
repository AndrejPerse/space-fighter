using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//takes care of spawning enemy units, repair powerup and meteor obstacles
public class SpawnManager : MonoBehaviour
{
    public GameObject[] unitPrefabs;
    private float zUnitSpawnLocation = 15.0f;
    private float unitSpawnStartDelay = 1.0f, unitSpawnInterval = 2.0f;

    public GameObject meteorPrefab;
    private float zMeteorSpawnLocation = 23.0f;
    private float meteorSpawnStartDelay = 2.0f, meteorSpawnInterval = 5.0f;

    public GameObject repairPrefab;
    private float zRepairSpawnLocation = 16.0f;
    private float repairSpawnStartDelay = 5.0f, repairSpawnInterval = 5.0f;

    private float xSpawnLocation = 5.0f, ySpawnLocation = 5.0f;

    private GameManager gameManagerScript;


    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        InvokeRepeating(nameof(SpawnUnitPrefab), unitSpawnStartDelay, unitSpawnInterval);
        InvokeRepeating(nameof(SpawnMeteorPrefab), meteorSpawnStartDelay, meteorSpawnInterval);
        InvokeRepeating(nameof(SpawnRepairPrefab), repairSpawnStartDelay, repairSpawnInterval);
    }

    void SpawnUnitPrefab()
    {
        if (gameManagerScript.isGameActive)
        {
            int prefabIndex = Random.Range(0, unitPrefabs.Length);
            Vector3 spawnLocation = new Vector3(Random.Range(-xSpawnLocation, xSpawnLocation), ySpawnLocation, zUnitSpawnLocation);
            Instantiate(unitPrefabs[prefabIndex], spawnLocation, unitPrefabs[prefabIndex].transform.rotation);
        }
    }
    
    void SpawnMeteorPrefab()
    {
        if (gameManagerScript.isGameActive)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-xSpawnLocation, xSpawnLocation), ySpawnLocation, zMeteorSpawnLocation);
            Instantiate(meteorPrefab, spawnLocation, meteorPrefab.transform.rotation);
        }
    }

    void SpawnRepairPrefab()
    {
        if (gameManagerScript.isGameActive)
        {
            Vector3 spawnLocation = new Vector3(Random.Range(-xSpawnLocation, xSpawnLocation), ySpawnLocation, zRepairSpawnLocation);
            Instantiate(repairPrefab, spawnLocation, repairPrefab.transform.rotation);
        }
    }
}
