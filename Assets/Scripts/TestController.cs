using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    [Tooltip("Speed of the character movement.")]
    [SerializeField] private float moveSpeed = 50.0f;
    private Vector3 moveInput;
    private Rigidbody rb;
        
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Gets inputs for horizontal and vertical movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(horizontalInput,0, verticalInput).normalized;
    }

    private void FixedUpdate()
    {
        // Moves the character
        rb.velocity = moveInput * moveSpeed * Time.fixedDeltaTime;
    }
}
