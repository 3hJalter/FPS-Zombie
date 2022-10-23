using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;
    private float horizontalInput, verticalInput;
    private bool isJump = false;
    private bool isGrounded = true;
    [SerializeField] private float speed = 1, jumpForce = 2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        Vector3 playMovement = new Vector3(horizontalInput, 0, verticalInput);
        playMovement *= speed;
        rb.velocity = playMovement;

        // Create new ray, it's center is the player position, it's direction is Vector3.Down
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, transform.localScale.x / 2f + 0.01f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }


        if (isJump == true && isGrounded == true)
        {
            rb.velocity = playMovement;
            isJump = false;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    isGrounded = true;
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    isGrounded = false;
    //}
}
