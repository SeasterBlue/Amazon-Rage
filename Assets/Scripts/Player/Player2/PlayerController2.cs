using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    #region int
    //int lives = 12;
    #endregion

    #region floats
    [SerializeField] float currentSpeed;
    float rotationSpeed = 5.0f;
    float jumpForce = 6.0f;
    float groundDistance = 0.4f;
    #endregion

    #region bools
    private bool isGrounded;
    public bool isRunning = false;
    public bool oneArmChopped;
    public bool twoArmsChopped;
    public bool headChopped;
    #endregion

    #region weirdos
    LayerMask groundMask;
    Transform playerPickPoint;
    Transform leftArm;
    Transform rightArm;
    Transform rightHand;
    Transform leftHand;
    Transform head;
    Transform plantHead;
    public Seed seed;
    Rigidbody rb;
    Animator animator;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        groundMask = LayerMask.GetMask("Ground");
        playerPickPoint = GameObject.Find("PickPoint").GetComponent<Transform>();
        leftArm = GameObject.Find("Bone003").GetComponent<Transform>();
        leftHand = GameObject.Find("Bone010").GetComponent<Transform>();
        rightArm = GameObject.Find("Bone032").GetComponent<Transform>();
        rightHand = GameObject.Find("Bone033").GetComponent<Transform>();
        head = GameObject.Find("Bone018").GetComponent<Transform>();
        plantHead = GameObject.Find("Bone019").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        CheckIfGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            HandleJump();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            seed.RemoveSeedParent();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            if (twoArmsChopped) CutHead();
            if (oneArmChopped) CutRightArm();
            CutLeftArm();
             
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? 10.0f : 5.0f;
        isRunning = currentSpeed == 10.0f;

        HandleMovement(currentSpeed);

        
       
    }

    void HandleMovement(float moveSpeed)
    {

        Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMoving = inputVector != Vector2.zero;

        animator.SetBool("isWalking", isMoving && !isRunning);
        animator.SetBool("isRunning", isRunning);

        //
        Vector3 moveDirection = new(inputVector.x, 0.0f, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;

        //Movement
        transform.position += moveDirection * moveDistance;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    void HandleJump()
    {

        if(isGrounded) rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void CutLeftArm()
    {
        leftArm.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
        oneArmChopped = true;
    }

    void CutRightArm()
    {
        rightArm.localScale = new Vector3(0.0001f, 0.0001f, 0.0001f);
        twoArmsChopped = true;
    }

    void CutHead()
    {
        head.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        plantHead.localScale = new Vector3(7, 7, 7);
        headChopped = true;
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

    public Transform GetMacheteNewTransform()
    {
        return leftHand;
    }

    public Transform GetGunsawNewTransform()
    {
        return rightHand;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Machete"))
        {
            other.transform.parent = GetMacheteNewTransform();
            //other.transform.localPosition = Vector3.zero;
            other.transform.localPosition = leftHand.transform.localPosition + new Vector3(0,0.12f,0);
            other.transform.localRotation = leftHand.transform.localRotation * Quaternion.Euler(0, 0, 1);


        }
    }
}
