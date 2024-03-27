using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RatingScript : MonoBehaviour
{
    [SerializeField]
    public float maxAmount = 5f;
    public Slider slider;
    public TMP_Text text;

    float formalAttendance = 100f;
    float diedAttendance = 0f;


    private void OnEnable()
    {
        VictimScript.OnVictimDead += SetRating;
    }

    private void OnDisable()
    {
        VictimScript.OnVictimDead -= SetRating;
    }

    private void Start()
    {
        slider.maxValue = maxAmount;
        slider.value = maxAmount;
    }

    public void SetRating()
    {
        diedAttendance += 1;
        slider.value = Mathf.Round((formalAttendance * 5 + diedAttendance * 1) / (formalAttendance + diedAttendance)*100) / 100;
        text.text = string.Format("(" + slider.value + ")");
    }
}
