using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 5.0f;
    private float playerRadius;
    private float playerHeight;


    [SerializeField] private Transform playerPickPoint;

    public Seed pickedSeed;
    private GameInput gameInput;
    private Animator mandraAnimator;

    private bool isRunning;

    private void Awake()
    {
        gameInput = GetComponent<GameInput>();
        mandraAnimator = GetComponent<Animator>();
        playerRadius = GetComponent<CharacterController>().radius;
        playerHeight = GetComponent<CharacterController>().height;

    }
    private void Start()
    {
        gameInput.OnInteractAction += OnInteractAction;
        gameInput.OnRunAction += OnRunAction;
        gameInput.OnRunCanceled += OnRunCanceled;
        GameManager.Instance.GameOver += OnGameOver;
        GameManager.Instance.Victory += OnVictory;

    }
    void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDirection = new(inputVector.x, 0.0f, inputVector.y);
        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = CollisionDetection(moveDirection, moveDistance);


        if (!mandraAnimator.GetBool("isRunning")) mandraAnimator.SetBool("isWalking", inputVector == new Vector2(0, 0) ? false : true);

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

    }

    private void OnInteractAction(object sender, EventArgs e)
    {
        // Check if the player is near a Nest object and has an egg
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2.0f);
        bool nearNest = false;
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Nest"))
            {
                nearNest = true;
                break;
            }
        }

        if (nearNest && HasEgg())
        {
            DestroyEgg();
            GameManager.Instance.eggsDropped++;
        }
    }

    private void DestroyEgg()
    {
        foreach (Transform child in playerPickPoint)
        {
            Seed egg = child.GetComponent<Seed>();
            if (egg != null)
            {
                egg.RemoveEggParent();
                Destroy(egg.gameObject);
                break;
            }
        }
    }

    private bool CollisionDetection(Vector3 moveDirection, float moveDistance)
    {
        // checks if the object is colliding, if false is not colliding so we get the opposite
        bool canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDirection,
            moveDistance
            );
        return canMove;
    }
    private void OnRunAction(object sender, EventArgs e)
    {
        Debug.Log("RUNNINGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
        mandraAnimator.SetBool("isWalking", false);
        Vector2 inputVector = gameInput.GetMovementVector();
        bool ShouldRun = inputVector != Vector2.zero && moveSpeed > 0.0f;
        mandraAnimator.SetBool("isRunning", ShouldRun);
        if (ShouldRun)
        {
            moveSpeed *= 2.0f;
            isRunning = true;
        }
    }

    private void OnRunCanceled(object sender, EventArgs e)
    {
        Debug.Log("STOPPED RUNNING");
        mandraAnimator.SetBool("isRunning", false);
        moveSpeed = isRunning ? moveSpeed / 2.0f : moveSpeed;
        isRunning = false;
        // Your code to handle the Run key being released
    }

    private void OnGameOver(object sender, EventArgs e)
    {
        Debug.Log("GAME OVEEEEEEEEEEEEEEEEEEEEEEEEEEEER");
        mandraAnimator.SetBool("isDead", true);
    }

    private void OnVictory(object sender, EventArgs e)
    {
        Debug.Log("WOOOOOOOOOOOOOOOOOOOOOOOOOOOON");
        mandraAnimator.SetBool("isAttacking", true);
    }

    public Transform GetEggNewTransform()
    {
        return playerPickPoint;
    }



    public bool HasEgg()
    {
        return pickedSeed != null;
    }

}
