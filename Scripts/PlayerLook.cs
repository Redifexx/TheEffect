using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public float mouseSensitivity = 500f;
    public Transform flashLight;

    public Transform playerBody;
    //public Transform flashlight;
    //public Transform gun;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        flashLight.localRotation = Quaternion.Euler(xRotation, 0f, yRotation);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //gun.RotateAround = Quaternion.Euler(xRotation, 0f, 0f);
        //playerBody.Rotate(Vector3.up * mouseX);
        playerBody.rotation = Quaternion.Euler(0, yRotation, 0);

    }
}
