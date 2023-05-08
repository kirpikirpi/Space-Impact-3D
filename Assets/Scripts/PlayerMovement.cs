using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool active = false;
    private float speed = 5f;
    private float aimpointOnZAxis = 35f;
    private Rigidbody rb;

    private float horizontalSpeed = 8;
    private float horizontalMovement;
    private Vector3 spawnPosition;

    public float accelerationSpeed = 4f;
    public float decelerationSpeed = 8f;
    public float maxSpeed = 40f;
    public float minSpeed = 0f;
    private float currentSpeed = 0f;
    
    public float mouseSensitivity = 100.0f;
    public float maxPitch = 80.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;


    void Start()
    {
        spawnPosition = transform.position;
    }

    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        
        // Check if "W" is pressed
        if (Input.GetKey(KeyCode.W))
        {
            // Accelerate
            currentSpeed += accelerationSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Decelerate
            currentSpeed -= decelerationSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
        }
        else
        {
            // Decelerate gradually if no keys are pressed
            currentSpeed -= decelerationSpeed * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
        }
        
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    void FixedUpdate()
    {
        if (active)
        {
            Vector3 targetPos = Vector3.zero;
            Vector3 currentPos = transform.position;

            float xPos = currentPos.x + horizontalMovement * horizontalSpeed * Time.deltaTime;
            float zForward = currentPos.z + currentSpeed * Time.deltaTime;
            targetPos = new Vector3(xPos, currentPos.y, zForward);
            
            float forward = currentSpeed * Time.deltaTime;
            
            Vector3 right = Vector3.Cross(transform.forward, transform.up);
            float sidways = - horizontalMovement * horizontalSpeed * Time.deltaTime; //minus because controls are inverted
            
            targetPos = currentPos + transform.forward * forward;
            targetPos = currentPos + right * sidways + transform.forward * forward;

            rb.MovePosition(targetPos);
        }
    }

    public void SetActive(bool isActive)
    {
        active = isActive;
    }

    public void Setup(Rigidbody movementRigidbody)
    {
        rb = movementRigidbody;
        active = true;
    }
}