using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class wavespawn : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public neoenemy[] enemies;
        public int count;
        public float timebtwnspawns;
    }

    public Wave[] waves;
    public Transform[] spawnpoints;
    public float timebtwnwaves;

    private Wave currentwave;
    private int currentwaveindex;
    private Transform player;
    private bool spawndone;
    private scenetransition screentrans;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentwaveindex));
        screentrans = FindObjectOfType<scenetransition>();
    }




    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timebtwnwaves);
        StartCoroutine(SpawnWave(index));
    }


    IEnumerator SpawnWave(int index)
    {
        currentwave = waves[index];


        for (int i = 0; i < currentwave.count; i++)
        {
            if (player == null)
            {
                yield break;//player is dead stop waves
            }

            neoenemy randomenemy = currentwave.enemies[Random.Range(0, currentwave.enemies.Length)];
            Transform randomspot = spawnpoints[Random.Range(0, spawnpoints.Length)];
            Instantiate(randomenemy, randomspot.position, randomspot.rotation);

            if (i == currentwave.count - 1)
            {
                spawndone = true;
            }
            else
            {
                spawndone = false;
            }

            yield return new WaitForSeconds(timebtwnwaves);


        }


    }

    private void Update()
    {
        if (spawndone == true && GameObject.FindGameObjectsWithTag("enemy").Length == 0)// a wave has been cleared
        {
            spawndone = false; //start new wave
            if (currentwaveindex + 1 < waves.Length)
            {
                currentwaveindex++;
                StartCoroutine(StartNextWave(currentwaveindex));
            }
            else
            {
                screentrans.loadscreen("win");
            }
        }
    }

}