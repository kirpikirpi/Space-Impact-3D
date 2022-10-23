using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool active = false;
    private float speed = 5f;
    private Rigidbody rb;
    
    void FixedUpdate()
    {
        if (active)
        {
            Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            rb.MovePosition(transform.position + m_Input * Time.deltaTime * speed);
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
