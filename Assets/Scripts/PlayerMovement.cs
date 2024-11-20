using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 5;
    public float groundDrag = 1;

    public float jumpStrength = 7;
    public float jumpCooldown = .25f;
    public float airMultiplier = 5;

    bool readyToJump = true;

    [Header("Ground Check")]
    float playerHeight;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlatform;
    bool isGrounded;
    bool isPlatform;
    bool isJumping = false;


    Transform rotation;
    public float rotationSpeed = 5.0f;

    //public Transform orientation;
    //public Transform theModel;
    GameObject theOrientation;
    Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody theRigidBody;
    Animator animator;
    GameObject playerObject;

    int animationState = 0;

    public bool makeRayVisible = false;
    public float rayOffset = 0.2f;

    [Header("Keybindings")]
    public KeyCode jumpKey = KeyCode.Space;


    void Awake()
    {
        //MeshRenderer renderer;
        
        Collider theCollider;

        //Create the Orientation
        theOrientation = new GameObject("theOrientation");
        theOrientation.transform.position = new Vector3( transform.position.x, transform.position.y,transform.position.z);
        theOrientation.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        theOrientation.transform.parent = gameObject.transform;
        orientation = theOrientation.transform;

        playerObject = gameObject.transform.Find("PlayerObject").gameObject;

       

        theCollider = playerObject.GetComponent<Collider>();
        playerHeight = theCollider.bounds.size.y;
        //Debug.Log("playerHeight: " + playerHeight.ToString());
        
        theRigidBody = GetComponent<Rigidbody>();
        theRigidBody.freezeRotation = true;

        animator = playerObject.GetComponent<Animator>();
    }


    private void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && (isGrounded|isPlatform) && readyToJump)
        {
            Debug.Log("Inside ifJumpkey");
            readyToJump = false;
            jump();

            Invoke(nameof(resetJump), jumpCooldown);
        }
    }

    private void movePlayer()
    {
        //Movement
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (isGrounded) //If on the ground
        {
            theRigidBody.AddForce(moveDirection.normalized * movementSpeed * 5f, ForceMode.Force);
        }
        else if (!isGrounded & !isPlatform)
        {
            theRigidBody.AddForce(moveDirection.normalized * movementSpeed * 5f * airMultiplier, ForceMode.Force);
        }


    }
    private void jump()
    {
        theRigidBody.velocity = new Vector3(theRigidBody.velocity.x, 0f, theRigidBody.velocity.z);
        theRigidBody.AddForce(transform.up * jumpStrength, ForceMode.Impulse);
        isJumping = true;
    }
    private void resetJump()
    {
        readyToJump = true;
        isJumping = false;
    }
    private void setAnimation()
    {
        animationState = 0;
        if ((verticalInput + horizontalInput) != 0) animationState = 1;
        if (isJumping) animationState = 5;
        //Debug.Log(animationState);
        animator.SetInteger("state", animationState);
    }


    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + rayOffset, whatIsGround);
        isPlatform = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + rayOffset, whatIsPlatform);

        //If we need to see the raycast for debugging purposes
        if(makeRayVisible)
        {
            Vector3 tmpVector = Vector3.down * (playerHeight * 0.5f + rayOffset);
            Debug.DrawRay(transform.position, tmpVector, Color.red);
        }

        //Debug.Log(isGrounded);

        if(isPlatform) isGrounded = false;
        
        myInput();
        speedControl();

        if (isGrounded)
            theRigidBody.drag = groundDrag;
        else
            theRigidBody.drag = 0;
    }
    private void FixedUpdate()
    {
        
        movePlayer();
        setAnimation();
    }


    private void speedControl()
    {
        Vector3 flatVelocity = new Vector3(theRigidBody.velocity.x, 0f, theRigidBody.velocity.z);

        if(flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            theRigidBody.velocity = new Vector3(limitedVelocity.x, theRigidBody.velocity.y, limitedVelocity.z);
        }
    }

    public Transform getOrientation()
    {
        return orientation;
    }
    public GameObject getPlayerObject()
    {
        return playerObject;
    }


}
