using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class MarioController : MonoBehaviour
{
    //Properties
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private const float speed = 5.5f;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction crouchAction;
    private MarioControls mappings;
    private Animator animator;
    private Rigidbody rigidBody;


    //Methods
    private void Awake()
    {
        mappings = new MarioControls();
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();


        moveAction = mappings.Movement.Move;
        jumpAction = mappings.Movement.Jump;
        crouchAction = mappings.Movement.Crouch;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        crouchAction.Enable();

        jumpAction.performed += Jump;
        crouchAction.performed += Crouch; 
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        crouchAction.Disable();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float axis = moveAction.ReadValue<float>();

        Vector3 input = (axis * transform.right);

        input *= speed;

        rigidBody.velocity = new Vector3(input.x, 0, 0);

        animator.SetFloat("Speed", input.x);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //Triggering the jump and animation
        animator.SetTrigger("Jump");
        rigidBody.AddForce(Vector3.up * jumpForce);
    }

    private void Crouch(InputAction.CallbackContext context)
    {
        //Triggering the animation
        animator.SetTrigger("Crouch");
    }
}
