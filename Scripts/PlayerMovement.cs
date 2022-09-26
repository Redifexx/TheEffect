using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;
    public AudioSource walkingSound;
    bool isPlaying;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    [SerializeField] bool grounded;
    [SerializeField] bool isJumped;
    [SerializeField] bool isPressed;
    [SerializeField] bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        walkingSound = GetComponent<AudioSource>();
        ResetJump();
    }
    private void Update() 
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        MyInput();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else { rb.drag = 0; }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }


    }

    private void FixedUpdate() 
    {
        MovePlayer();
        SpeedControl();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded) 
        {
            isPressed = true;
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void MovePlayer() 
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            if (!walkingSound.isPlaying && (verticalInput != 0f || horizontalInput != 0f)) 
            {
                walkingSound.Play();
            }
        }
        else if (!grounded) 
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            rb.AddForce(transform.up * -9.81f, ForceMode.Force);
            walkingSound.Stop();
        }
    }

    private void SpeedControl() 
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

    }

    private void Jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        isJumped = true;
    }

    private void ResetJump() {
        readyToJump = true;
    }
}
