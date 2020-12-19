using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public Transform Ground, Camera, Hand;

    public Camera cam;

    public float sensitivity = 10f;

    public float walking;
    public float running = 0f;
    public float jumpStrength;
    public float speed;

    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    bool IsHandFull, IsOnGround;
    Vector3 PlayerVelocity;

    public Vector3 RespawnPoint;

    public CapsuleCollider PlayerCollider;

    // Start is called before the first frame update
    void Start()
    {
        IsHandFull = false;
        RespawnPoint = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {        
        Jump();

        ControlCamera();

        ControlPickupObject();
    }

    public void ControlCamera()
    {
        Camera.Rotate(-Input.GetAxis("Mouse Y") * sensitivity, 0, 0);

        Ground.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
    }

    public void ControlPickupObject()
    {
        if (Input.GetMouseButtonDown(0) && !IsHandFull)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Pickup")
                {
                    Transform t = hit.transform;

                    t.SetParent(Hand);

                    t.localPosition = Vector3.zero;
                    t.localRotation = Quaternion.identity;
                    IsHandFull = true;
                }
                
            }
        }
    }

    public void ControlWalking()
    {
        speed = walking + running;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = Ground.transform.right * x + Ground.transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    public void ControlRuning()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed += running;
        }


    }

    public void Crawling()
    {
        if (IsOnGround)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                PlayerCollider.height = 0.5f;
            }
            else
            {
                PlayerCollider.height = 1f;
            }
        }
    }

    public void Jump()
    {
        speed = walking;

        ControlRuning();

        IsOnGround = controller.isGrounded;
        if (IsOnGround && PlayerVelocity.y < 0)
        {
            PlayerVelocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = Ground.transform.right * x + Ground.transform.forward * z;

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && IsOnGround)
        {
            PlayerVelocity.y += Mathf.Sqrt(jumpHeight * -jumpStrength * gravityValue);
        }

        PlayerVelocity.x = move.x * speed;
        PlayerVelocity.z = move.z * speed;

        PlayerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(PlayerVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rope")
        {
            other.GetComponentInParent<RopeController>().TouchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rope")
        {
            other.GetComponentInParent<RopeController>().TouchingPlayer = false;
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Spikes")
        {
            transform.position = RespawnPoint;
            IsOnGround = true;
        }
    }
}
