using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraControler : MonoBehaviour
{
    public float speedH = 5.0f;
    public float speedV = 5.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    
    public Transform targetObject;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;
    
    void Start()
    {
        targetObject = GameObject.Find("ball").transform;
        initalOffset = transform.position - targetObject.position;
    }

    void Update () {
        cameraPosition = targetObject.position + initalOffset;
        transform.position = cameraPosition;
        
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }
}