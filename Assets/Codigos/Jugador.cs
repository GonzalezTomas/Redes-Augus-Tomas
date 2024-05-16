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

    public override void Spawned()
    {
        rb = GetComponent<Rigidbody>();

        if (!HasStateAuthority) return;

        Camera.main.GetComponent<Camara>()?.Target(transform);
    }

    void Update()
    {
        // Obtener la entrada del teclado para moverse horizontalmente
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Aquí puedes agregar cualquier animación de salto que necesites
        }
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        // Mover el jugador horizontalmente
        Vector3 movement = Vector3.right * horizontalInput * Runner.DeltaTime * speed;
        rb.MovePosition(rb.position + movement);
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        float distance = GetComponent<Collider>().bounds.extents.y + 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, out hit, distance);
    }
}






