using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraControler : MonoBehaviour
{
    //public GameObject player;
    public float sensitivity;

    private Vector3 offset;
    private Rigidbody rb;

    private GameObject mainCamera;

    void Start () 
    {
        offset = transform.position;
        rb = GetComponent<Rigidbody> ();
    }

    void LateUpdate () 
    {
        //transform.position = player.transform.position + offset;
    }
    
    public float speedH = 5.0f;
    public float speedV = 5.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update () {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    /*void FixedUpdate ()
    {
        Vector3 mousePosition = Input.mousePosition;
        float rotateHorizontal = Input.GetAxis ("Mouse X");
        float rotateVertical = Input.GetAxis ("Mouse Y");
        transform.Rotate((float)Input.mousePosition.x/380, (float)0.3f, (float)Input.mousePosition.y/380);
        //transform.RotateAround(GetComponent<Camera>().transform.position, -Vector3.up, rotateHorizontal * sensitivity);
        //transform.RotateAround (Vector3.zero, transform.right, rotateVertical * sensitivity);
        //transform.RotateAround(GetComponent<Camera>().transform.position, Input.mousePosition.y, (float)Input.mousePosition.x);
        
        print(Input.mousePosition);
        /*float rotateHorizontal = Input.GetAxis ("Mouse X");
        float rotateVertical = Input.GetAxis ("Mouse Y");
        transform.RotateAround (player.transform.position, -Vector3.up, rotateHorizontal * sensitivity); //use transform.Rotate(-transform.up * rotateHorizontal * sensitivity) instead if you dont want the camera to rotate around the player
        transform.RotateAround (Vector3.zero, transform.right, rotateVertical * sensitivity); // again, use transform.Rotate(transform.right * rotateVertical * sensitivity) if you don't want the camera to rotate around the player*/

        /*float rotateHorizontal = Input.GetAxis ("Mouse X");
        float rotateVertical = Input.GetAxis ("Mouse Y");

        Vector3 rotation = new Vector3 (rotateHorizontal, 0.0f, rotateVertical);

        rb.AddForce (rotation * sensitivity);

    }*/
}