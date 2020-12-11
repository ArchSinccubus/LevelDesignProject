using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public Transform Ground, Camera, Hand;

    public Camera cam;

    public float sensitivity = 10f;

    public float walking = 12f;
    public float running = 0f;
    public float speed;

    bool IsHandFull;

    // Start is called before the first frame update
    void Start()
    {
        IsHandFull = false;
    }

    // Update is called once per frame
    void Update()
    {
        ControlWalking();

        ControlRuning();

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
        if (Input.GetKey(KeyCode.Space))
        {
            running = 8f;
        }
        else
        {
            running = 0f;
        }

    }
}
