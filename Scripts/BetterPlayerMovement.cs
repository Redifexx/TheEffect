using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterPlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }

        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            controller.Move(move * 24f * Time.deltaTime);
            UnityEngine.Debug.Log("shift pressed");
        }
        else {
            controller.Move(move * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
