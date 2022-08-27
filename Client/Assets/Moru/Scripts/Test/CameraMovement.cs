using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    public float speed = 1;
    
    public bool isRecording;
    private void Update()
    {
        var mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        this.gameObject.transform.LookAt(mousePos);
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.localPosition += Vector3.forward * speed*Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.localPosition += Vector3.back* speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.localPosition += Vector3.right* speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.localPosition += Vector3.left * speed * Time.deltaTime;
        }
    }
}
