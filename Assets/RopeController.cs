using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public bool TouchingPlayer;

    public Transform SpawnPoint;

    public PlayerController Player;

    public Material Mat;

    // Start is called before the first frame update
    void Start()
    {
        Mat.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (TouchingPlayer)
        {
            Mat.color = Color.green;
        }
    }


    private void OnMouseEnter()
    {
        if (true)
        {
            Mat.color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        if (TouchingPlayer)
        {
            Player.controller.enabled = false;
            Player.transform.position = SpawnPoint.position;
            Player.RespawnPoint = SpawnPoint.position;
            Player.controller.enabled = true;
            Mat.color = Color.white;
            TouchingPlayer = false;
        }
    }
}
