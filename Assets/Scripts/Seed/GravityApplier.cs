using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityApplier : MonoBehaviour
{
    public float gravityStrength = 9.8f; // Strength of gravity
    public bool applyGravity = true;
    public bool isOnGround;
    float groundDistance = 0.4f;
    LayerMask groundMask;

    private void Start()
    {
        groundMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        CheckIfGrounded();
        if (!isOnGround && applyGravity)
        {
            ApplyGravity();
        }
    }

    bool CheckIfGrounded()
    {
        isOnGround = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        ActivateGravity();
        return isOnGround;
    }

    void ActivateGravity()
    {
        if(isOnGround)
        {
            applyGravity = false;
        }
    }
     

    void ApplyGravity()
    {
        // Apply gravity manually without using Rigidbody
        Vector3 currentPosition = transform.position;
        currentPosition.y -= gravityStrength * Time.deltaTime;
        transform.position = currentPosition;
    }

}
