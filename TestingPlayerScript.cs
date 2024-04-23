using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class TestingPlayerScript : MonoBehaviour
{
    public Transform trans;
    public Transform modelHolder;


    private Rigidbody rb;

    public float moveSpeed;
    public float jumpForce;

    private bool dead = false;

    private Vector3 spawnPoint;

    private float respawnWaitTime = 2;

    Animator animator;
    string animationState = "AnimationState";

    bool grounded = true;

    enum CharStates
    {
        idle = 0,
        run = 1,
        left = 2,
        right = 3,
        jump = 4,
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        spawnPoint = trans.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        grounded = true;
        animator.SetInteger(animationState, (int)CharStates.run);
    }

    void movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //print(horizontalInput);
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDir = Vector3.zero;

        if (horizontalInput > 0 && grounded == true)
        {
            animator.SetInteger(animationState, (int)CharStates.right);
            //modelHolder.eulerAngles.y = 30;

            //modelHolder.Rotate(0, 30, 0);
            // Smoothly tilts a transform towards a target rotation.
            /*
            float smooth = 5.0f;
            float tiltAngle = 30.0f;
            float tiltAroundY = Input.GetAxis("Horizontal") * tiltAngle;
            float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(0, tiltAroundY, 0);

            // Dampen towards the target rotation
            modelHolder.rotation = Quaternion.Slerp(modelHolder.rotation, target, Time.deltaTime * smooth);
            */
        }
        else if (horizontalInput < 0 && grounded == true)
        {
            animator.SetInteger(animationState, (int)CharStates.left);
        }
        else if (verticalInput > 0 || verticalInput < 0 && grounded == true)
        {
            /*
            float smooth = 5.0f;
            float tiltAngle = 30.0f;
            float tiltAroundY = Input.GetAxis("Horizontal") * tiltAngle;
            float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;
            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(0, 0, 0);

            // Dampen towards the target rotation
            modelHolder.rotation = Quaternion.Slerp(modelHolder.rotation, target, Time.deltaTime * smooth);
            */
            //animator.SetInteger(animationState, (int)CharStates.run);
        }
        else if (grounded == true)
        {
            animator.SetInteger(animationState, (int)CharStates.run);
        }
        movementDir = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 movement = transform.TransformDirection(movementDir) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            grounded = false;
            animator.SetInteger(animationState, (int)CharStates.jump);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            
        }
    }

    public void FreezeMovement()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;
    }

    public void Die()
    {
        if (!dead)
        {
            dead = true;
            Invoke("Respawn", respawnWaitTime);
            enabled = false;
            //rb.enabled = false;
            //modelHolder.gameObject.SetActive(false);
        }
    }

    public void Respawn()
    {
        dead = false;
        trans.position = spawnPoint;

        enabled = true;
        //rb.enabled = true;
        //modelHolder.gameObject.SetActive(true);
    }
}
