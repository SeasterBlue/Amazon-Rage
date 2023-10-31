using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityApplier : MonoBehaviour
{
    public float gravityStrength = 5.0f;
    public bool applyGravity = true;
    public bool isOnGround;
    float groundDistance = 0.2f; // antes 0.4f;
    LayerMask groundMask;
    public GameObject lightFromPlant;

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
        else if (isOnGround)
        {
            DeActivateGravity();
        }
    }

    bool CheckIfGrounded()
    {
        isOnGround = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        return isOnGround;
    }

    void DeActivateGravity()
    {
        if(isOnGround)
        {
            applyGravity = false;
            ReActiveLight();
        }
    }

    void ReActiveLight()
    {
        lightFromPlant.SetActive(true);
    }
     

    void ApplyGravity()
    {
        // Apply gravity manually without using Rigidbody
        Vector3 currentPosition = transform.position;
        currentPosition.y -= gravityStrength * Time.deltaTime;
        transform.position = currentPosition;
    }

}
