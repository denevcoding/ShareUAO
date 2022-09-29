using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTytpe
{
    Atun,
    Fish,
    Yaya
}

public class CharacterConrtoller : MonoBehaviour
{
    //My Gameobject Components
    private Rigidbody rbCharacter;
    public Animator catAnimator;
    public CatSoundManager soundManager;

    public ItemTytpe itemType;

    public Transform Cam;

    public int numerodeitems;

    [Header("Platformer properties")]
    public bool sideScroller; // the game is 2.5D?

    //Calculating input direction from the user
    float h;
    float v;
    private Vector3 inputDirection;

    private Vector3 direction, moveDirection;
    private Vector3 screenMovementForward, screenMovementRight;//World space or camera space relative vectors
    private Quaternion screenMovementSpace = Quaternion.identity;


    [Header("Ground detection")]
    public bool grounded;
    int groundedLayerMask; //We can add any layers to this mask and combine to evaluate if we are touching players or something else to restore jump

    [Header("Movement properties")]
    //Ground
    [Tooltip("Esta variable controla la aceleracion en el suelo")]
    public float maxSpeed; //Mas speed of the character
    public float accel;
    public float decel;
    public float runningAccel;

    private float defaultAccel;
    private float defaultAirAcel;


    [Space(20)]
    //Air
    public float airAccel;
    public float airDecel;

    public float rotateSpeed;//Ground rotation speed;
    public float airRotateSpeed;

    private float slope;
    public float slopeLimit;
    public float slideAmount; // Velocity to feel down a slide
    public float movingPlatformFriction; //Friction of the object like moving platforms 
    private Vector3 movingObjSpeed; // Id we are on a platform


    //Temporal values to calculate the value of the movement and in the air
    private float curAccel, curDecel;
    private float curRotateSpeed;

    //Physic Properties 
    [HideInInspector] public Vector3 m_currentSpeed; //Velocity of the rigidBodie
    [HideInInspector] public Vector3 currentAngVel;
    [HideInInspector] public float m_DistanceToTarget; //Value that indicates if I arrived to my next frame position


    //Mechanics 
    [Header("Cat  and running")]
    public bool running;

    [Header("Cat Jump")]
    public float jumpForce;
    public float walkingJumpForce;
    public float runningJumpForce;
    public float coyoteTime;
    public float coyoteTimeCounter;


    [Header("Sounds")]
    public AudioClip ronroneo;





    private void Awake()
    {
        soundManager = GetComponentInChildren<CatSoundManager>();
        rbCharacter = GetComponent<Rigidbody>();
        //soundManager = GetComponent<CatSoundManager>();


        defaultAccel = accel;
        defaultAirAcel = airAccel;
    }

    // Start is called before the first frame update
    void Start()
    {
        groundedLayerMask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        grounded = IsGrounded(); //Set the animator from here or inside the function
        HandleCoyoteTime();
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //if (input.getbuttondown("fire2"))
        //{
        //    gameobject balaauxiliar = instantiate(sphere, transform.position/* + transform.forward*/ , quaternion.identity);
        //    balaauxiliar.getcomponent<rigidbody>().addforce(transform.forward * 1000);
        //    destroy(balaauxiliar, 2);

        //}

        inputDirection = new Vector2(h, v);
        inputDirection.Normalize();

        //Debug.Log(grounded);

        CalculateInputs(); //Update and listen inputs and calculate vector based on sidescroller bool       

        //Setting values to animator
        if (catAnimator)
        {
            catAnimator.SetBool("isGrounded", grounded);

            catAnimator.SetFloat("DistanceToTarget", m_DistanceToTarget);

            if (grounded == false)
            {

                if (rbCharacter.velocity.y < -1)
                {
                    catAnimator.SetBool("Falling", true);
                    catAnimator.SetBool("Jumping", false);
                }
            }
            else
            {
                catAnimator.SetBool("Falling", false);
            }




            Vector3 velocity = rbCharacter.velocity;
            velocity.y = 0;
            catAnimator.SetFloat("XVelocity", velocity.magnitude);
            catAnimator.SetFloat("YVelocity", rbCharacter.velocity.y);
        }
    }



    void CalculateInputs()
    {

        //adjust movement values if we're in the air or on the ground
        curAccel = (grounded) ? accel : airAccel;
        curDecel = (grounded) ? decel : airDecel;
        curRotateSpeed = (grounded) ? rotateSpeed : airRotateSpeed;

        //get movement axis relative to camera
        screenMovementForward = screenMovementSpace * Cam.forward;
        screenMovementRight = screenMovementSpace * Cam.right;

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");


        //only apply vertical input to movemement, if player is not sidescroller
        if (!sideScroller)
            inputDirection = (screenMovementForward * v) + (screenMovementRight * h);
        else
            inputDirection = Vector3.right * h;

        //inputDirection = new Vector3(h, 0f, v);
        
        if (inputDirection.magnitude > 0.1f) {
            inputDirection.Normalize();
            soundManager.vfxSource.volume = 0f;
            //soundManager.CleanClip();
            //float targetAngle = Mathf.Atan2(inputDirection.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref t);
            //moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            soundManager.vfxSource.volume = 0.4f;
        }


        


        moveDirection = transform.position + inputDirection;


        Debug.DrawRay(transform.position, moveDirection, Color.green);       
        Debug.DrawRay(transform.position, inputDirection, Color.red);
        Debug.DrawRay(transform.position, rbCharacter.velocity);


        if (grounded)
        {
            if (Input.GetKey(KeyCode.LeftShift) && inputDirection.magnitude > 0)
            { //Is Running
                running = true;
                catAnimator.SetBool("Running", true);
                accel = runningAccel;
                airAccel = 15f;
                jumpForce = runningJumpForce;
            }
            else
            {
                //Not Running - So walking
                running = false;
                catAnimator.SetBool("Running", false);
                accel = defaultAccel;
                airAccel = 5f;
                jumpForce = walkingJumpForce;
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
        else
        {
            running = false;
            catAnimator.SetBool("Running", false);
        }
    }



    private void FixedUpdate()
    {
        rbCharacter.WakeUp();

        MoveTo(moveDirection, curAccel, 0.05f, true);

        //Validate if user is pressing inputs so we need to rotate to llok to that direction
        if (rotateSpeed != 0 && inputDirection.magnitude != 0)        
            RotateToDirection(inputDirection, curRotateSpeed, true);


        ManageSpeed(curDecel, maxSpeed + movingObjSpeed.magnitude, true);

      

    }

    public bool IsGrounded()
    {

        //get distance to ground, from centre of collider (where floorcheckers should be)
        float dist = GetComponent<CapsuleCollider>().bounds.extents.y + 0.05f;

        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * (dist), Color.cyan, 0.05f);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, dist + 0.05f, groundedLayerMask))
        {
            //FloatingCapsule(hit);
            return true;

        }

        return false;


        ////get distance to ground, from centre of collider (where floorcheckers should be)
        //float dist = GetComponent<Collider>().bounds.extents.y;

     
        //    RaycastHit hit;

        //    if (Physics.Raycast(this.transform.position, Vector3.down, out hit, dist + 0.05f, groundedLayerMask))
        //    {
        //        Debug.DrawRay(this.transform.position, Vector3.down * (dist + 0.05f), Color.cyan, 0.05f);
        //        if (!hit.transform.GetComponent<Collider>().isTrigger)
        //        { //The object has a real collider not Trigger

        //            slope = Vector3.Angle(hit.normal, Vector3.up); //slope control

        //            //slide down slopes
        //            if (slope > slopeLimit && hit.transform.tag != "Pusheable")
        //            {
        //                Vector3 slide = new Vector3(0f, -slideAmount, 0f);
        //                rbCharacter.AddForce(slide, ForceMode.Force);
        //            }

        //            ////Moving with platforms
        //            ////moving platforms
        //            //if (hit.transform.tag == "MovingPlatform" || hit.transform.tag == "Pushable")
        //            //{
        //            //    movingObjSpeed = hit.transform.GetComponent<Rigidbody>().velocity;
        //            //    movingObjSpeed.y = 0f;
        //            //    //9.5f is a magic number, if youre not moving properly on platforms, experiment with this number
        //            //    rbCharacter.AddForce(movingObjSpeed * movingPlatformFriction * Time.deltaTime, ForceMode.VelocityChange);
        //            //}
        //            //else
        //            //{
        //            //    movingObjSpeed = Vector3.zero;
        //            //}

        //            Debug.Log("Am touching the floor");
        //            //yes our feet are on something    
                
        //            return true;

        //        }
        //    }
        

        //movingObjSpeed = Vector3.zero;
        //Debug.Log("Am Not touching the floor");
        ////no none of the floorchecks hit anything, we must be in the air (or water)
        //return false;
    }

    public void HandleCoyoteTime()
    {
        //Coyote time to have a responsive jumop
        if (grounded)
        {
            //On the ground
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            //Falling or in the air
            coyoteTimeCounter -= Time.deltaTime;
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
            rbCharacter.AddForce((m_currentSpeed * -1) * deceleration * Time.deltaTime, ForceMode.VelocityChange);
            if (rbCharacter.velocity.magnitude > maxSpeed)
                rbCharacter.AddForce((m_currentSpeed * -1) * deceleration * Time.deltaTime, ForceMode.VelocityChange);

            //if (running)
            //{
            //    if (rbCharacter.velocity.magnitude > 10)
            //        rbCharacter.AddForce((m_currentSpeed * -1) * deceleration * Time.deltaTime, ForceMode.VelocityChange);
            //}
            //else
            //{
            //    if (rbCharacter.velocity.magnitude > maxSpeed)
            //        rbCharacter.AddForce((m_currentSpeed * -1) * deceleration * Time.deltaTime, ForceMode.VelocityChange);
            //}
           
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
        currentAngVel = rbCharacter.angularVelocity;

        Vector3 characterPos = transform.position;
        if (ignoreY)
        {
            characterPos.y = 0;
            lookDir.y = 0;
        }
        //Un n?mero mayor o menor a este har? que mire hacia el otro lado al voltear
        //lookDir.z = 0;


        turnSpeed *= 1.6f;
        Quaternion dirQ = Quaternion.LookRotation(lookDir);
        Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, turnSpeed * Time.deltaTime);
        rbCharacter.MoveRotation(slerp);
    }






  
}
