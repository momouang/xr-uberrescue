using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoatCtrl : MonoBehaviour
{
    [SerializeField]
    Transform boat, directionPointer;

    [SerializeField]
    CharacterController controller;

    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    TMP_Text speedText;

    public float TopSpeed = 5f;
    public float TopRotate = 100f;
    public float SpeedRate = 0.01f;
    public float RotateRate = 0.01f;

    [Range(0f, 1f)]
    public float AssignedSpeed = 0f;

    [Range(0f, 1f)]
    public float AssignedDirection = .5f;

    [SerializeField]
    float currentSpeed = 0f;

    [SerializeField]
    bool goForward = true;

    private void FixedUpdate()
    {
        float newDirection = AssignedDirection - 0.5f;
        newDirection *= TopRotate;
        directionPointer.localEulerAngles = new Vector3(0f, newDirection, 0f);
        boat.rotation = Quaternion.Lerp(boat.rotation, directionPointer.rotation, RotateRate);

        float newSpeed = (goForward) ? AssignedSpeed : -AssignedSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, newSpeed, SpeedRate);

        Vector3 newPosition = boat.forward * currentSpeed * TopSpeed * Time.deltaTime;
        //transform.position += newPosition;
        controller.Move(newPosition);
        //rb.velocity = newPosition;

        UpdateDisplays();
    }

    void UpdateDisplays()
    {
        speedText.text = ((int)(AssignedSpeed * 100)).ToString("D3");
    }

    public void SetAssignedSpeed(float _value) => AssignedSpeed = _value;

    public void SetAssignedDirection(float _value) => AssignedDirection = _value;

    public void SetGoForward(bool _value) => goForward = _value;
}
