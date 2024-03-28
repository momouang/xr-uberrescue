using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StylizedWater2;

public enum PlayerStates
{
    Walk,
    Float,
    Swim,
}

class PlayerEvents
{
    public static event System.Action ResetRotationEvent;
    public static void ResetRotation() => ResetRotationEvent?.Invoke();
}

public class Player : MonoBehaviour
{
    public Transform cameraPosition;
    public PlayerStates currentState = PlayerStates.Walk;


    private void OnEnable() {
        PlayerEvents.ResetRotationEvent += ResetRotation;
    }

    private void OnDisable() {
        PlayerEvents.ResetRotationEvent -= ResetRotation;
    }
    
    public Transform playerHead;

    public Rigidbody rb;

    public float speed = 1f, jumpForce = 5f, swimForce = 100f;

    public InputActionProperty walkInputAction;
    public InputActionProperty rightPaddleAction, leftPaddleAction;

    float horizontal, vertical;

    private void Update() 
    {
        horizontal = walkInputAction.action.ReadValue<Vector2>().x;
        vertical = walkInputAction.action.ReadValue<Vector2>().y;

        Vector3 movement = (playerHead.right * horizontal + playerHead.forward * vertical) * speed;
        
        Move(movement);

        if (rightPaddleAction.action.WasPressedThisFrame())
            PaddleRemapping();

        if (leftPaddleAction.action.WasPerformedThisFrame())
            PaddleRemapping();
    }

    void CheckCurrentState()
    {
        if(transform.position.y>0)
        {
            if(currentState!= PlayerStates.Walk)
            {
                currentState = PlayerStates.Walk;
                SwitchState(currentState);
            }
        }
        else if(transform.position.y<=0 && playerHead.position.y>.2f)
        {
            currentState = PlayerStates.Float;
        }
        else
        {
            currentState = PlayerStates.Swim;
        }
    }

    void SwitchState(PlayerStates state)
    {
        switch (state)
        {
            case PlayerStates.Walk:
                rb.drag = 0;
                rb.useGravity = true;
                break;
        }
    }

    void PaddleRemapping()
    {
        if(currentState==PlayerStates.Walk)
            Jump();
        else
            Swim();
    }

    void Move(Vector3 movement)
    {
        Vector3 newVelocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        rb.velocity = newVelocity;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    void Swim()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void ResetRotation()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
