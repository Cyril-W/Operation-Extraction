using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float WalkSpeed = 20;
    public float RunSpeed = 50;
    public float Acceleration = 20;
    public float Deceleration = 30;
    public Vector3 Jump;
    public float JumpForce = 2.0f;

    [Space(10)]

    [HideInInspector] public bool Attacking;

    bool kUp;
    bool kDown;
    bool kLeft;
    bool kRight;
    bool kRun;
    public bool kAttack;
    bool kJump;

    Vector3 inputDirection;

    public bool isGrounded;

    bool jumping = false;

    Rigidbody rb;

    [HideInInspector] public Vector3 velocity;

    public bool alive = true;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    void Update()
    {
        GetInputs();

        SetMovement();

        if ((kJump) && (isGrounded))
        {
            jumping = true;
            rb.AddForce(Jump * JumpForce, ForceMode.Impulse);
            //isGrounded = false;

        }
    }
    void GetInputs()
    {
        if (alive)
        {
            kUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Z);
            kDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
            kLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q);
            kRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
            kRun = Input.GetKey(KeyCode.LeftShift);
            kAttack = Input.GetKey(KeyCode.Return);
            kJump = Input.GetKeyDown(KeyCode.Space);

            inputDirection = Vector3.zero;

            if (kUp) inputDirection.z += 1;
            if (kDown) inputDirection.z -= 1;
            if (kLeft) inputDirection.x -= 1;
            if (kRight) inputDirection.x += 1;

            inputDirection = inputDirection.normalized;
        }
    }

    void SetMovement()
    {

        if (inputDirection.magnitude > 0)
        {
            velocity = Vector3.Lerp(velocity, inputDirection, Acceleration * Time.deltaTime / 3.6f);
        }
        else
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, Deceleration * Time.deltaTime / 3.6f);
        }

        float speed = WalkSpeed;
        if (kRun) speed = Mathf.Lerp(speed, RunSpeed, 0.5f);


        transform.localPosition += velocity * Time.deltaTime / 3.6f * speed;

        if (velocity.magnitude > .01f)
        {
            Quaternion currentRotation = transform.localRotation;
            Quaternion targetRotation = Quaternion.LookRotation(velocity, Vector3.up);

            transform.localRotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * speed * 2f);
        }

    }
}