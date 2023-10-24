using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    Rigidbody rb;

    #region floats
    [SerializeField] private float currentSpeed;
    private float rotationSpeed = 5.0f;
    private float jumpForce = 6.0f;
    float groundDistance = 0.4f;
    #endregion

    #region bools
    private bool isGrounded;
    private bool isRunning = false;
    #endregion

    #region weirdos
    private LayerMask groundMask;
    private Transform playerPickPoint;
    public Seed seed;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundMask = LayerMask.GetMask("Ground");
        playerPickPoint = GameObject.Find("PickPoint").GetComponent<Transform>();
        
    }

    private void Update()
    {
        CheckIfGrounded();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? 10.0f : 5.0f;
        isRunning = currentSpeed == 10.0f;
        
        HandleMovement(currentSpeed);


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            HandleJump();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            seed.RemoveSeedParent();
        }
    }

    void HandleMovement(float moveSpeed)
    {

        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveDirection = new(inputVector.x, 0.0f, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;

         transform.position += moveDirection * moveDistance;
       

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    void HandleJump()
    {

        if(isGrounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void DestroySeed() //no usada aun
    {
        foreach (Transform child in playerPickPoint)
        {
            Seed seed = child.GetComponent<Seed>();
            if (seed != null)
            {
                seed.RemoveSeedParent();
                Destroy(seed.gameObject);
                break;
            }
        }
    }

    bool CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        return isGrounded;
    }

    public bool HasSeed()
    {
        return seed != null;
    }


    public Transform GetSeedNewTransform()
    {
        return playerPickPoint;
    }
}
