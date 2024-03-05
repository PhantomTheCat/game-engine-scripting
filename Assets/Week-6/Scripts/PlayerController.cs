using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

//How we are using the new input system
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


//Help and Code gotten from our In-Class Demo and from code in canvas


namespace Week6
{
    public class PlayerController : MonoBehaviour
    {
        //Properties (For input action, Can assign buttons to this field in the inspector (Comes from our new input system))
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private const float speed = 5.5f;
        [SerializeField] float rotation = 5.0f;
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] Transform bulletSpawnTransform;
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction lookAction;
        private InputAction fireAction;
        private PlayerControllerMappings mappings;
        private Rigidbody rigidBody;
        private float mouseDeltaX = 0f;
        private float mouseDeltaY = 0f;
        private float cameraRotationX = 0f;
        private int rotationDirection = 0;
        private bool grounded;


        //Methods
        private void Awake()
        {
            //Connecting mappings to our mapping instance and rigidBody to our rigidBody
            mappings = new PlayerControllerMappings();
            rigidBody = GetComponent<Rigidbody>();

            //Using our InputAction to trigger the Jump method in the Player instance inside the mappings
            //Important to note that we can access the player's methods from that InputMapping
            //moveAction = mappings.Player.Jump;


            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //Connecting our input bindings to the actions
            moveAction = mappings.Player.Move;
            jumpAction = mappings.Player.Jump;
            lookAction = mappings.Player.Look;
            fireAction = mappings.Player.Fire;
        }

        private void OnEnable()
        {
            //Enabling our player actions

            //Enabling our character to move and jump when the object is enabled
            moveAction.Enable();
            jumpAction.Enable();
            lookAction.Enable();
            fireAction.Enable();

            //Having an event that calls our Jump method when a jump happens
            jumpAction.performed += Jump;
            fireAction.performed += Fire;
        }

        private void OnDisable()
        {
            //Disabling our player actions

            //Disabling our player's ability to move when object is disabled
            moveAction.Disable();
            jumpAction.Disable();
            lookAction.Disable();
            fireAction.Disable();

            //Having an event that stops our event from performing
            //jumpAction.performed -= Jump;
        }

        void Update()
        {
            HandleHorizontalRotation();
            HandleVerticalRotation();

            //We read the values that player inputs by using ReadValue
            //Have to have a Vector2 or 3 based on the system of bindings you set up (tells the method to return the Vector2 or 3)

            //So this will return a Vector2 with values in the format of (x,y) where
            //x represents our input from A and D
            //y represents our input from W and S
            //On a range from from -1 to +1
            //Vector2 input = moveAction.ReadValue<Vector2>();

            ////Getting our input into real terms that we can move things at that rate
            //input *= speed * Time.deltaTime;

            ////Transforming our position
            //float xValue = transform.position.x + input.x;
            //float zValue = transform.position.z + input.y;
            //float yValue = transform.position.y;
            //transform.position = new Vector3(xValue, yValue, zValue);

            ////Setting the velocity of our rigid body.
            //rigidBody.velocity = new Vector3 (input.x, rigidBody.velocity.y, input.y);
        }

        bool IsGrounded()
        {
            //Basically casting a ray and if it goes through the ground layer
            //that we have declared in engine, we are grounded/above the ground.

            //Bit shift the index of the layer (8) to get a bit mask (Getting the ground layer)
            int layerMask = 1 << 3;

            RaycastHit hit;

            //Checking if the ray goes through the ground layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up * -1), out hit, 1.1f, layerMask))
            {
                //Draws the actual line in the system
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up * -1) * hit.distance, Color.yellow);

                //Returns true
                return true;
            }
            else
            {
                //Draws the actual line in the system
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

                //Else returns false
                return false;
            }


        }

        private void FixedUpdate()
        {
            grounded = IsGrounded();

            HandleMovement();
        }

        void HandleMovement()
        {
            if (grounded == false) return;

            Vector2 axis = moveAction.ReadValue<Vector2>();

            Vector3 input = (axis.x * transform.right) + (transform.forward * axis.y);

            input *= speed;

            rigidBody.velocity = new Vector3(input.x, rigidBody.velocity.y, input.z);
        }

        void HandleHorizontalRotation()
        {

            mouseDeltaX = lookAction.ReadValue<Vector2>().x;

            if (mouseDeltaX != 0)
            {
                rotationDirection = mouseDeltaX > 0 ? 1 : -1;

                transform.eulerAngles += new Vector3(0, rotation * Time.deltaTime * rotationDirection, 0);
            }
        }

        void HandleVerticalRotation()
        {
            mouseDeltaY = lookAction.ReadValue<Vector2>().y;

            if (mouseDeltaY != 0)
            {
                rotationDirection = mouseDeltaY > 0 ? -1 : 1;

                cameraRotationX += rotation * Time.deltaTime * rotationDirection;
                cameraRotationX = Mathf.Clamp(cameraRotationX, -45f, 45f);

                var targetRotation = Quaternion.Euler(Vector3.right * cameraRotationX);


                //Vector3 angle = new Vector3(rotation * Time.deltaTime * rotDir, 0, 0);

                //Debug.Log(Camera.main.transform.localRotation.x);

                Camera.main.transform.localRotation = targetRotation;
                //Camera.main.transform.Rotate(angle, Space.Self);

            }
        }

        void Jump(InputAction.CallbackContext context)
        {
            //This method name needs to be same as one in the mappings inspector
            //Making player jump if they are on the ground(Adding force to go up)
            if (IsGrounded())
            {
                rigidBody.AddForce(Vector3.up * jumpForce);
            }
        }

        void Fire(InputAction.CallbackContext context)
        {
            //Instantiating bullets
            //Using the camera as using the player's rotation doesn't count the vertical rotation
            Instantiate(bulletPrefab, bulletSpawnTransform.position, Camera.main.transform.rotation);
        }
    }
}
