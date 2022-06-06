using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationAndMovementController : MonoBehaviour
{
    [Range(0,100)]
    [SerializeField] private float rotationFactorPerFrame = 1.0f;
    [Range(0, 30)]
    [SerializeField] private float runMultiplier = 3.0f;

    PlayerInput playerInput;
    CharacterController characterController;
    Animator anim;

    int isWalkingHash;
    int isRunningHash;

    [SerializeField]
    float gravity = -9.8f;
    [SerializeField]
    float groundedGravity = -0.05f;
    [SerializeField]
    float maxJumpHeight = 1.0f, maxJumpTime = 0.5f;
    [SerializeField]
    float initialJumpVelocity;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    bool isRunPressed;
    bool isMovementPressed;
    bool isJumpPressed = false;
    bool isJumping;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        playerInput.CharacterControls.Move.started += OnMovementInput;

        playerInput.CharacterControls.Move.canceled += OnMovementInput;

        playerInput.CharacterControls.Move.performed += OnMovementInput;

        playerInput.CharacterControls.Run.started += OnRun;

        playerInput.CharacterControls.Run.canceled += OnRun;

        playerInput.CharacterControls.Jump.started += OnJump;

        playerInput.CharacterControls.Jump.canceled += OnJump;

        SetupJumpVariables();

        //playerInput.CharacterControls.Run.performed += OnRun;
    }

    void Update()
    {
        HandleAnimation();
        HandleRotation();
        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
        characterController.Move(currentMovement * Time.deltaTime);
        HandleGravity();
        HandleJump();

    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable();
    }
    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }

    void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void HandleJump()
    {
        //if (!isJumping && characterController.isGrounded && isJumpPressed)
        //{
        //    isJumping = true;
        //    currentMovement.y = initialJumpVelocity;
        //    currentRunMovement.y = initialJumpVelocity;
        //}
        //else if (!isJumpPressed && isJumping && characterController.isGrounded)
        //{
        //    isJumping = false;
        //}
        if (!isJumping && characterController.isGrounded && isJumpPressed)
        {
            isJumping = true;
            currentMovement.y = initialJumpVelocity * 0.5f;
            currentRunMovement.y = initialJumpVelocity * 0.5f;
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded)
        {
            isJumping = false;
        }




    }

    void HandleGravity()
    {
        bool isFalling = currentMovement.y <= 0.0f;
        float fallMultiplier = 2.0f;
        if (characterController.isGrounded)
        {
            currentMovement.y = groundedGravity;
            currentRunMovement.y = groundedGravity;
        }
        //else if (isFalling)
        //{
        //    float previousYVelocity = currentMovement.y;
        //    float newYVelocity = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
        //    float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
        //    currentMovement.y = nextYVelocity;
        //    currentRunMovement.y = nextYVelocity; 
        //}
        else
        {
            //float previousYVelocity = currentMovement.y;
            //float newYVelocity = currentMovement.y + (gravity * Time.deltaTime);
            //float nextYVelocity = (previousYVelocity + newYVelocity) * 0.5f;
            //currentMovement.y += nextYVelocity;
            //currentRunMovement.y += nextYVelocity;

            currentMovement.y += gravity * Time.deltaTime;
            currentRunMovement.y += gravity * Time.deltaTime;
        }
    }
    void OnJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
         
    }

    void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void HandleAnimation()
    {
        bool isWalking = anim.GetBool(isWalkingHash);
        bool isRunning = anim.GetBool(isRunningHash);
        if (isMovementPressed && !isWalking)
        {
            anim.SetBool(isWalkingHash, true);
        }
        else if (!isMovementPressed && isWalking)
        {
            anim.SetBool(isWalkingHash, false);
        }

        if((isMovementPressed && isRunPressed) && !isRunning)
        {
            anim.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            anim.SetBool(isRunningHash, false);
        }
    }

    void HandleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;
        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }


    }
}
