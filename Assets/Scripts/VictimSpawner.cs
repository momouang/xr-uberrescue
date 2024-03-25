using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictimSpawner : MonoBehaviour
{
    public GameObject[] victims;
    public Transform[] spawnPoints;
    public bool isSpawning;
    public float spawnTiming = 5f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(AutoSpawn());
            isSpawning = true;
        }
    }

    void SpawnVictim()
    {
        Instantiate(victims[Random.Range(0, victims.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)]);
    }

    IEnumerator AutoSpawn()
    {
        yield return new WaitForSeconds(1f);

        while(isSpawning)
        {
            yield return new WaitForSeconds(spawnTiming);
            SpawnVictim();
        }
    }
}
