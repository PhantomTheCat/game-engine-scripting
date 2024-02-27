using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//How we are using the new input system
using UnityEngine.InputSystem;


namespace Week6
{
    public class PlayerController : MonoBehaviour
    {
        //Properties (For input action, Can assign buttons to this field in the inspector (Comes from our new input system))
        [SerializeField] private InputAction moveAction;
        [SerializeField] private InputAction jumpAction;
        [SerializeField] private float jumpForce = 5f;
        private const float speed = 5.5f;
        private PlayerControllerMappings mappings;
        private Rigidbody rigidBody;


        //Methods
        private void Awake()
        {
            //Connecting mappings to our mapping instance and rigidBody to our rigidBody
            mappings = new PlayerControllerMappings();
            rigidBody = GetComponent<Rigidbody>();


            //Using our InputAction to trigger the Jump method in the Player instance inside the mappings
            //Important to note that we can access the player's methods from that InputMapping
            //moveAction = mappings.Player.Jump;

            //Connecting our input bindings to the actions
            moveAction = mappings.Player.Move;
            jumpAction = mappings.Player.Jump;
        }

        private void OnEnable()
        {
            //Enabling our character to move and jump when the object is enabled
            moveAction.Enable();
            jumpAction.Enable();

            //Having an event that calls our Jump method when a jump happens
            jumpAction.performed += Jump;
        }

        private void OnDisable()
        {
            //Disabling our player's ability to move when object is disabled
            moveAction.Disable();
            jumpAction.Disable();

            //Having an event that stops our event from performing
            jumpAction.performed -= Jump;
        }

        void Start()
        {
            
        }

        void Update()
        {
            //We read the values that player inputs by using ReadValue
            //Have to have a Vector2 or 3 based on the system of bindings you set up (tells the method to return the Vector2 or 3)

            //So this will return a Vector2 with values in the format of (x,y) where
            //x represents our input from A and D
            //y represents our input from W and S
            //On a range from from -1 to +1
            Vector2 input = moveAction.ReadValue<Vector2>();

            //Getting our input into real terms that we can move things at that rate
            input *= speed * Time.deltaTime;

            //Transforming our position
            float xValue = transform.position.x + input.x;
            float zValue = transform.position.z + input.y;
            float yValue = transform.position.y;
            transform.position = new Vector3(xValue, yValue, zValue);

            //Setting the velocity of our rigid body.
            rigidBody.velocity = new Vector3 (input.x, rigidBody.velocity.y, input.y);
        }

        void Jump(InputAction.CallbackContext context)
        {
            //This method name needs to be same as one in the mappings inspector

            //Making player jump (Adding force to go up)
            rigidBody.AddForce(Vector3.up * jumpForce);
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
    }
}
