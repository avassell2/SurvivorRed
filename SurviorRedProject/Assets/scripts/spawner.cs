using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public Transform[] spawnPoints; //manipulate transform components of spawn point
    public GameObject[] hazards; //stores array of hazard enemies sprites

    private float timeBtwSpawns; //next time to spawn hazard
    public float startTimeBtwSpawns;

    public float minTimeBetweenSpawns;
    public float decrease;
    public GameObject player;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) //check if player is alive or not. stop spawning if he's dead make sure to drag player object for aid componet
        {
            if (timeBtwSpawns <= 0)
            {
                //Spawn hazard
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject randomHazard = hazards[Random.Range(0, hazards.Length)]; //will choose random hazard sprite


                Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.identity);

                if (startTimeBtwSpawns > minTimeBetweenSpawns)
                {//once enemy is spawned check if starttime is greater than mintime in  other words if game is not at max difficulty so we can gradually increase it to max
                    startTimeBtwSpawns -= decrease;
                }
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}
