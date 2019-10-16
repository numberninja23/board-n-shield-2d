using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    private Vector3 offset;
    public Transform playerLeft;
    public Transform playerRight;
    public Camera cam;
    public float maxZoomIN;
    public float maxZoomOUT;

    // Use this for initialization
    void Start () {
        offset = transform.position - target.position; // subtract target's position from camera's position
        maxZoomIN = cam.orthographicSize;
	}

    void Update()
    {
        target.position = (playerLeft.position + playerRight.position) / 2;
        float camEdge;
        if(Mathf.Abs(playerLeft.position.x) >= Mathf.Abs(playerRight.position.x))
        {
            camEdge = Mathf.Abs(playerLeft.position.x - target.position.x);
        }
        else
        {
            camEdge = Mathf.Abs(playerRight.position.x - target.position.x);
        }
        if (camEdge > maxZoomIN && camEdge < maxZoomOUT)
        {
            cam.orthographicSize = camEdge;
        }
        else if (camEdge < maxZoomOUT)
        {
            cam.orthographicSize = maxZoomIN;
        }
        else
        {
            cam.orthographicSize = maxZoomOUT;
        }
    }

    // LateUpdate is called after all the other Update functions
    void LateUpdate () {
        transform.position = target.position + offset; // preserves the offset btw camera and player
	}
}
