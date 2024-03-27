using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StylizedWater2;
using System;

public class VictimScript : MonoBehaviour
{
    public static Action OnVictimDead;


    public float lifeTime = 10f;
    float currentTime = 0f;
    AlignToWaves waterLevel;

    float gravity = 1f;

    private void Start()
    {
        waterLevel = GetComponent<AlignToWaves>();
        waterLevel.enabled = false;
        gravity = Time.deltaTime * -9.8f;
    }

    private void Update()
    {
        if(gameObject.transform.position.y <= 1)
        {
            waterLevel.enabled = true;

            currentTime += Time.deltaTime;
            waterLevel.heightOffset -= Time.deltaTime * (lifeTime * 0.01f);

            if (currentTime >= lifeTime)
            {
                currentTime = lifeTime;
                waterLevel.heightOffset -= Time.deltaTime * (lifeTime * 0.5f);
                StartCoroutine(StartDeath());
            }
        }
        else
        {
            waterLevel.enabled = false;
            DropVictim();
        }

    }

    IEnumerator StartDeath()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Destroyed");
        SendRating();
        Destroy(gameObject);

    }

    void DropVictim()
    {
        gameObject.transform.Translate(new Vector3(0, gravity, 0));
    }

    public void SendRating()
    {
        OnVictimDead?.Invoke();
    }

}
