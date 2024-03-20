using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

class PlayerEvents
{
    public static event System.Action ResetRotationEvent;
    public static void ResetRotation() => ResetRotationEvent?.Invoke();
}

public class Player : MonoBehaviour
{
    private void OnEnable() {
        PlayerEvents.ResetRotationEvent += ResetRotation;
    }

    private void OnDisable() {
        PlayerEvents.ResetRotationEvent -= ResetRotation;
    }
    
    public Transform playerHead;

    public Rigidbody rb;

    public float speed = 1f, jumpForce = 5f;

    public InputActionProperty walkInputAction;
    public InputActionProperty jumpInputAction;

    float horizontal, vertical;

    private void Update() 
    {
        horizontal = walkInputAction.action.ReadValue<Vector2>().x;
        vertical = walkInputAction.action.ReadValue<Vector2>().y;

        Vector3 movement = (playerHead.right * horizontal + playerHead.forward * vertical) * speed;
        
        Move(movement);
        
        if(jumpInputAction.action.WasPressedThisFrame())
            Jump();
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

    private void ResetRotation()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
