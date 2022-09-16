using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{

    private Rigidbody rbCharacter;
    public Animator catAnimator;

    int groundedLayerMask;
    private bool grounded;


    //Gameplay Variables
    float h;
    float v;
    Vector3 inputDirection;
    Vector3 moveDirection;


    private void Awake()
    {
        groundedLayerMask = LayerMask.GetMask("Floor");
        rbCharacter = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(h, v);
        inputDirection.Normalize();
    }

    private void FixedUpdate()
    {
        rbCharacter.MovePosition(transform.position + Time.deltaTime * 5f * transform.TransformDirection(h, 0, v));

        Quaternion dirQ = Quaternion.LookRotation(inputDirection);
        Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, 2f * Time.deltaTime);
        rbCharacter.MoveRotation(slerp);
    }
}
