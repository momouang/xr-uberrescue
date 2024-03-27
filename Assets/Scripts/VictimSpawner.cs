using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictimSpawner : MonoBehaviour
{
    public GameObject[] victims;
    public Transform[] spawnPoints;
    public bool isSpawning;
    public float spawnTiming = 5f;

    public Image screenImage;
    public Animator ScreenAnim;
    public Sprite[] sprites;

    private void OnEnable()
    {
        VictimScript.OnVictimDead += ShowOneStarRating;
    }

    private void OnDisable()
    {
        VictimScript.OnVictimDead -= ShowOneStarRating;
    }

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

    void ShowOneStarRating()
    {     
        ScreenAnim.SetTrigger("Show");
        screenImage.sprite = sprites[1];
    }

    IEnumerator AutoSpawn()
    {
        yield return new WaitForSeconds(1f);

        while(isSpawning)
        {
            yield return new WaitForSeconds(spawnTiming);
            SpawnVictim();
          
            ScreenAnim.SetTrigger("Show");
            screenImage.sprite = sprites[0];
        }
    }
}
