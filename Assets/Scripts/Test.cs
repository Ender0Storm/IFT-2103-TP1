using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Test : MonoBehaviour
{
    private GameObject cameraObject;
    
    public int speed = 2;
    public float startTime;

    void Start()
    {
        cameraObject = GameObject.Find("main_camera");
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            startTime = Time.time;
        }
        if (Input.GetKeyUp("space") || Time.time > 1000)
        {
            Debug.Log((Time.time - startTime).ToString("00:00.00"));
            print((Time.time - startTime).ToString("00:00.00"));
            transform.Translate(cameraObject.transform.forward * (Time.time - startTime) * 1000 * Time.deltaTime);
        }

        if (Input.GetKeyDown("z"))
        {
            cameraObject.transform.position = new Vector3(1, 2, 0) * Time.deltaTime;
        }
        if (Input.GetKeyDown("s"))
        {
            cameraObject.transform.position = new Vector3(1, 2, 0) * Time.deltaTime;
        }
        if (Input.GetKeyDown("q"))
        {
            cameraObject.transform.position = new Vector3(0, 2, 1) * Time.deltaTime;
        }
        if (Input.GetKeyDown("d"))
        {
            cameraObject.transform.position = new Vector3(0, 2, 1) * Time.deltaTime;
        }
        /*if (Input.GetKeyDown ("Horizontal"))
            timer = Time.time;
 
        if (Input.GetKeyDown() ("Horizontal") && Time.time - timer > windUpTime)
        {*
            //float frameCount = timer;
            //int tiime = Mathf.FloorToInt(timer);
            //int current = (int)frameCount;
            //transform.Translate(cameraObject.transform.forward * 100 * Time.deltaTime);
        //}
        
        /*if (Input.GetKeyDown("space"))
        {
            transform.Translate(cameraObject.transform.forward * speed * Time.deltaTime);
        }*/
    }
}