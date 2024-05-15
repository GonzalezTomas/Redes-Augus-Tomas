using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Jugador : NetworkBehaviour
{
    public float speed;
    public float jumpForce;
    public Animator anim;

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Aquí puedes agregar cualquier animación de salto que necesites
        }
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        transform.Translate(Vector3.forward * verticalInput * Runner.DeltaTime * speed);
        transform.Translate(Vector3.right * horizontalInput * Runner.DeltaTime * speed);
      
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        float distance = GetComponent<Collider>().bounds.extents.y + 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, out hit, distance);
    }
}

  





