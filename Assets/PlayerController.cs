using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform Ground, Camera, Hand;

    public Camera cam;

    public float sensitivity = 10f;

    bool IsHandFull;

    // Start is called before the first frame update
    void Start()
    {
        IsHandFull = false;
    }

    // Update is called once per frame
    void Update()
    {
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
}
