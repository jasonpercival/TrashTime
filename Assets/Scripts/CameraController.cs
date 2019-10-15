using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public Transform centerPoint;
    public float cameraSpeed = 5.0f;
    public float mouseSensitivity = 10.0f;

    private float mouseX, mouseY;

    public float zoom = 1.6f;
    public float zoomSpeed = 2.0f;
    public float zoomMin = 1.0f;
    public float zoomMax = 3.0f;

    private Vector3 offset;

    // mouse look
    private float yrotation = 0f;
    private float xrotation = 0f;
    private const float xSpeed = 100f;
    private const float ySpeed = 100f;
    private const float setDistance = 7f;
    private const float maxAngle = 80f;
    private const float minAngle = -70f;
    private readonly Vector3 pivotPos = new Vector3(0f, 2f, 0f);
    private bool isMouseLooking = false;

    void Start()
    {
        offset = player.transform.position - transform.position;
        Debug.Log(offset);
        //HideMouse();
    }

    private void Update()
    {
        // mouse zoom
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (zoom < zoomMin)
            zoom = zoomMin;

        if (zoom > zoomMax)
            zoom = zoomMax;


        isMouseLooking = Input.GetMouseButton(1);
    }

    void LateUpdate()
    {
        if (isMouseLooking)
        {
            yrotation += Input.GetAxis("Mouse X") * ySpeed * Time.deltaTime;
            xrotation += Input.GetAxis("Mouse Y") * xSpeed * Time.deltaTime;
            if (xrotation > maxAngle) xrotation = maxAngle;
            if (xrotation < minAngle) xrotation = minAngle;

            Debug.Log("X = " + xrotation + ", Y = " + yrotation);

            // Vector3 camPos = Quaternion.Euler(xrotation, yrotation, 0) * -player.transform.forward * setDistance;

            //transform.position = camPos + player.transform.position + pivotPos;
            //transform.LookAt(player.transform.position + pivotPos);

        }
        else
        {

            var newRot = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRot, cameraSpeed * Time.deltaTime);

            //Vector3 newPos = player.transform.position - player.transform.forward * offset.z * zoom - player.transform.up * offset.y * zoom * 1.2f;
            Vector3 newPos = player.transform.position - offset;
            transform.position = Vector3.Slerp(transform.position, newPos, cameraSpeed * Time.deltaTime);
        }

    }

    //public static void HideMouse() => Cursor.lockState = CursorLockMode.Locked;
    //public static void ShowMouse() => Cursor.lockState = CursorLockMode.Locked;


}
