using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConrtoller : MonoBehaviour
{
    private Rigidbody rbCharacter;
    public Animator catAnimator;

    [Header("Platformer properties")]
    public bool sideScroller; // the game is 2.5D?
    //Gameplay Variables
    float h;
    float v;

    //Movement
    public float maxSpeed; //Mas speed of the character
    private float curAccel, curDecel;

    [Header("Movement properties")]
    //Ground
    [Tooltip("Esta variable controla la aceleracion en el suelo")]
    public float accel;
    public float runningAccel;
    public float decel;


    public bool running;

    public float jumpForce;


    [Space(20)]
    //Air
    public float airAccel;  
    public float airDecel;

    //Movement Vectors
    [HideInInspector] public Vector3 m_currentSpeed;
    [HideInInspector] public float m_DistanceToTarget;


    //Rotations
    private Quaternion screenMovementSpace = Quaternion.identity;
    private float curRotateSpeed;

    private Vector3 direction, moveDirection, screenMovementForward, screenMovementRight;
    private Vector3 inputDirection;

    public float rotateSpeed;//Ground rotation speed;
    public float airRotateSpeed;

    private float slope;
    public float slopeLimit;
    public float slideAmount; // Velocity to feel down a slide
    public float movingPlatformFriction; //Friction of the object like moving platforms 
    private Vector3 movingObjSpeed; // Id we are on a platform



    //FloorCheks Floor Detection
    public Transform floorChecks;
    public Transform[] floorCheckers;
    private bool grounded;
    int groundedLayerMask; //We can add any layers to this mask and combine to evaluate if we are touching players or something else to restore jump


    private void Awake()
    {
        groundedLayerMask = LayerMask.GetMask("Floor");
        rbCharacter = GetComponent<Rigidbody>();
        InitFloorChecks();
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        grounded = IsGrounded(); //Set the animator from here or inside the function
        catAnimator.SetBool("isGrounded", grounded);
        Debug.Log(grounded);

        CalculateInputs(); //Update and listen inputs and calculate vector based on sidescroller bool

       
    }


    public bool IsGrounded()
    {
        //get distance to ground, from centre of collider (where floorcheckers should be)
        float dist = GetComponent<Collider>().bounds.extents.y;

     
            RaycastHit hit;

            if (Physics.Raycast(this.transform.position, Vector3.down, out hit, dist + 0.05f, groundedLayerMask))
            {
                Debug.DrawRay(this.transform.position, Vector3.down * (dist + 0.05f), Color.cyan, 0.05f);
                if (!hit.transform.GetComponent<Collider>().isTrigger)
                { //The object has a real collider not Trigger

                    slope = Vector3.Angle(hit.normal, Vector3.up); //slope control

                    //slide down slopes
                    if (slope > slopeLimit && hit.transform.tag != "Pusheable")
                    {
                        Vector3 slide = new Vector3(0f, -slideAmount, 0f);
                        rbCharacter.AddForce(slide, ForceMode.Force);
                    }

                    ////Moving with platforms
                    ////moving platforms
                    //if (hit.transform.tag == "MovingPlatform" || hit.transform.tag == "Pushable")
                    //{
                    //    movingObjSpeed = hit.transform.GetComponent<Rigidbody>().velocity;
                    //    movingObjSpeed.y = 0f;
                    //    //9.5f is a magic number, if youre not moving properly on platforms, experiment with this number
                    //    rbCharacter.AddForce(movingObjSpeed * movingPlatformFriction * Time.deltaTime, ForceMode.VelocityChange);
                    //}
                    //else
                    //{
                    //    movingObjSpeed = Vector3.zero;
                    //}

                    Debug.Log("Am touching the floor");
                    //yes our feet are on something    
                
                    return true;

                }
            }
        

        movingObjSpeed = Vector3.zero;
        Debug.Log("Am Not touching the floor");
        //no none of the floorchecks hit anything, we must be in the air (or water)
        return false;
    }

    private void FixedUpdate()
    {
        MoveTo(moveDirection, curAccel, 0.05f, true);


        if (rotateSpeed != 0 && direction.magnitude != 0) {
            RotateToDirection(inputDirection, curRotateSpeed, true);
            //RotateVelocity(rotateSpeed, true);
        }



        ManageSpeed(curDecel, maxSpeed + movingObjSpeed.magnitude, true);


        if (catAnimator)
        {
            catAnimator.SetFloat("DistanceToTarget", m_DistanceToTarget);

            if (rbCharacter.velocity.y < 0)
            {
                catAnimator.SetBool("Jumping", false);
            }

            Vector3 velocity = rbCharacter.velocity;
            velocity.y = 0;
            catAnimator.SetFloat("XVelocity", velocity.magnitude);


            catAnimator.SetFloat("YVelocity", rbCharacter.velocity.y);
        }
      
    }




    private void InitFloorChecks()
    {
        //Create a single floorcheck in centre of object, if none are assigned
        if (!floorChecks)
        {
            floorChecks = new GameObject().transform;
            floorChecks.name = "FloorChecks";
            floorChecks.parent = transform;
            floorChecks.position = transform.position;
            GameObject check = new GameObject();
            check.name = "Check1";
            check.transform.parent = floorChecks;
            check.transform.position = transform.position;
            Debug.LogWarning("No 'floorChecks' assigned to PlayerMove script, so a single floorcheck has been created", floorChecks);
        }

        //gets child objects of floorcheckers, and puts them in an array
        //later these are used to raycast downward and see if we are on the ground
        floorCheckers = new Transform[floorChecks.childCount];
        for (int i = 0; i < floorCheckers.Length; i++)
        {
            floorCheckers[i] = floorChecks.GetChild(i);
        }
    }



    void CalculateInputs()
    {
        rbCharacter.WakeUp();

        //adjust movement values if we're in the air or on the ground
        curAccel = (grounded) ? accel : airAccel;
        curDecel = (grounded) ? decel : airDecel;
        curRotateSpeed = (grounded) ? rotateSpeed : airRotateSpeed;

        //get movement axis relative to camera
        screenMovementForward = screenMovementSpace * Vector3.forward;
        screenMovementRight = screenMovementSpace * Vector3.right;

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");


        //only apply vertical input to movemement, if player is not sidescroller
        if (!sideScroller)
            inputDirection = (screenMovementForward * v) + (screenMovementRight * h);
        else
            inputDirection = Vector3.right * h;

       
        if (inputDirection.magnitude > 1.0f)
        {
            inputDirection.Normalize();
        }



        moveDirection = transform.position + inputDirection;

        Debug.DrawRay(transform.position, moveDirection, Color.green);


        
        Debug.DrawRay(transform.position, inputDirection, Color.red);


        if (grounded)
        {
            if (Input.GetMouseButton(0) && inputDirection.magnitude > 0)
            {
                running = true;
                catAnimator.SetBool("Running", true);
            }
            else
            {
                running = false;
                catAnimator.SetBool("Running", false);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (grounded)
                {
                    catAnimator.SetBool("Jumping", true);
                    rbCharacter.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }

            }
        }

       




    }


    public bool MoveTo(Vector3 destination, float acceleration, float stopDistance, bool ignoreY) 
    {
        Vector3 relativePos = (destination - transform.position);
        if (ignoreY)
            relativePos.y = 0;

        m_DistanceToTarget = relativePos.magnitude;
        if (m_DistanceToTarget <= stopDistance) {
            catAnimator.SetFloat("Locomotion", 0);
            return true; //Deje d moverme y llegue al destino del input
        }
            

        else {
            if (running)
            {
                rbCharacter.AddForce(relativePos * runningAccel * Time.deltaTime, ForceMode.VelocityChange);
                catAnimator.SetFloat("Locomotion", 2);
            }
            else
            {

                rbCharacter.AddForce(relativePos * acceleration * Time.deltaTime, ForceMode.VelocityChange);
                catAnimator.SetFloat("Locomotion", 1);
            }
        }
          



        return false; // Keep moving, input or IA are applying movement

       

    }

    public void ManageSpeed(float deceleration, float maxSpeed, bool ignoreY) 
    {//Controls the speed of our character on static floor, moving floors dynamicly friction 
        m_currentSpeed = rbCharacter.velocity;

        if (ignoreY)//if We want to handle the Y position manually
            m_currentSpeed.y = 0;

        if (m_currentSpeed.magnitude > 0)
        {
            if (running)
            {
                if (rbCharacter.velocity.magnitude > 10)
                    rbCharacter.AddForce((m_currentSpeed * -1) * deceleration * Time.deltaTime, ForceMode.VelocityChange);
            }
            else
            {
                if (rbCharacter.velocity.magnitude > maxSpeed)
                    rbCharacter.AddForce((m_currentSpeed * -1) * deceleration * Time.deltaTime, ForceMode.VelocityChange);
            }
           
        }

    }




    public void RotateVelocity(float turnSpeed, bool ignoreY)
    {
        Vector3 direction;
        if (ignoreY)
        {
            direction = new Vector3(rbCharacter.velocity.x, 0f, rbCharacter.velocity.z);
        }
        else
        {
            direction = rbCharacter.velocity;
        }

        if (direction.magnitude > 0.1f)
        {
            Quaternion dirQ = Quaternion.LookRotation(direction);
            Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, direction.magnitude * turnSpeed * Time.deltaTime);
            rbCharacter.MoveRotation(slerp);
        }
    }


    public void RotateToDirection(Vector3 lookDir, float turnSpeed, bool ignoreY)
    {

        Vector3 characterPos = transform.position;
        if (ignoreY)
        {
            characterPos.y = 0;
            lookDir.y = 0;
        }
        //Un n?mero mayor o menor a este har? que mire hacia el otro lado al voltear
        //lookDir.z = 0;


        turnSpeed *= 1.6f;
        Quaternion dirQ = Quaternion.LookRotation(inputDirection);
        Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, turnSpeed * Time.deltaTime);
        rbCharacter.MoveRotation(slerp);
    }






  
}
