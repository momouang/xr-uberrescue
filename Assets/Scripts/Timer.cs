using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public Transform lookatTarget;

    public float time = 30f;
    public bool isCounting = false;

    public TextMeshPro text;

    void Start()
    {
        isCounting = true;
    }


    void Update()
    {
        gameObject.transform.LookAt(lookatTarget);
        if(isCounting)
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
                UpdateTimer(time);
            }
            else
            {
                Debug.Log("Time Up");
                time = 0;
                isCounting = false;
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
