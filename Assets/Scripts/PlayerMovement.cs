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
    private float rubberBandSpeed = 1;
    private float horizontalConstraint = 7;
    private float horizontalMovement;
    private Vector3 spawnPosition;

    /*
    void FixedUpdate()
    {
        if (active)
        {
            Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
            
            Vector3 lookPosition = new Vector3(0,0, aimpointOnZAxis);
            //transform.LookAt(lookPosition);
        }
    }
    */
    void Start()
    {
        spawnPosition = transform.position;
    }

    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        if (active)
        {
            Vector3 targetPos = Vector3.zero;
            Vector3 currentPos = transform.position;

            if (horizontalMovement > 0 || horizontalMovement < 0)
            {
                float xPos = currentPos.x + horizontalMovement * horizontalSpeed * Time.deltaTime;
                xPos = Mathf.Clamp(xPos, -horizontalConstraint, horizontalConstraint);
                targetPos = new Vector3(xPos, currentPos.y, currentPos.z);
            }
            else
            {
                Vector3 directionToTarget = spawnPosition - currentPos;
                float distanceToTarget = directionToTarget.magnitude;

                if (distanceToTarget <= 0.2f) targetPos = spawnPosition;
                else
                {
                    float x = currentPos.x + directionToTarget.normalized.x * rubberBandSpeed * Time.deltaTime;
                    float y = currentPos.y + directionToTarget.normalized.y * rubberBandSpeed * Time.deltaTime;
                    float z = currentPos.z + directionToTarget.normalized.z * rubberBandSpeed * Time.deltaTime;
                    targetPos = new Vector3(x, y, z);
                }
            }
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

    public void Setup(Rigidbody movementRigidbody, float movementSpeed)
    {
        rb = movementRigidbody;
        speed = movementSpeed;
        active = true;
    }
}