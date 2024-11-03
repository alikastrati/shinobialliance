using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 9, -7);

    // Start is called before the first frame update
    void Start()
    {
        // Automatically find a GameObject tagged as "Player"
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Debug.Log("Player found and camera assigned to follow it.");
        }
        else
        {
            Debug.LogError("Player object not found! Ensure the player prefab has the 'Player' tag assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
        else {
            Debug.Log("No Player for the camera to follow");
        }
        
    }
}
