using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float walking = 12f;
    public float running = 0f;
    public float speed ;  
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    
    
    [SerializeField]KeyCode runningKey;

    private bool isGrounded;
    private Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        Gravity();
        Walking();
        

    }

    public void Walking()
    {
        speed = walking + running;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        Runing();

        controller.Move(move * speed * Time.deltaTime);
    }

    public void Runing()
    {
        running = Input.GetKey(runningKey) ? 8f : 0f;
    }

    public void Gravity()
    {
       
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime * 2);

        GroundedCheak();

    }

    public void GroundedCheak()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}