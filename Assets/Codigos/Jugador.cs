using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Jugador : NetworkBehaviour
{
    public float speed;
    public float jumpForce;
    public float danio = 20f;
    public Animator anim;
   // public LayerMask LayerDisapro;
    public Bala _bala;
    public Transform _apareceBala;

    private Rigidbody rb;
    private float horizontalInput;
    private bool _disparo;

    [Networked, OnChangedRender(nameof(OnNetHealtChanged))]
    public float Vida { get; set; } = 100;
    void OnNetHealtChanged() => Debug.Log($"Vida = {Vida}");
    
   

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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _disparo = true;
        }

    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (horizontalInput != 0)
        {
            transform.forward = horizontalInput > 0 ? Vector3.right : Vector3.left;
        }

        Vector3 movement = Vector3.right * horizontalInput * Runner.DeltaTime * speed;
       
        rb.MovePosition(rb.position + movement);  

        if(_disparo)
        {
           // Disparo();

            ApareceBala();

            _disparo =false;
        }

    }

  /*  void Disparo()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red, 2f);

        if(Runner.GetPhysicsScene().Raycast(transform.position, transform.forward, out var raycastHit, LayerDisapro)) 
        {
            var jug = raycastHit.transform.GetComponent<Jugador>();

            jug.RPC_RecibirDaño(danio);
        }
    } */

    void ApareceBala()
    {
        Runner.Spawn(_bala, _apareceBala.position, _apareceBala.rotation);
    }

    [Rpc(RpcSources.All,RpcTargets.StateAuthority)]
    public void RPC_RecibirDaño(float RecibirDanio)
    {
        Local_RecibirDaño(RecibirDanio);
    }

    public void Local_RecibirDaño(float RecibirDanio)
    {
        Vida -= RecibirDanio;

        if (Vida <= 0)
        {
            Muere();
        }
    }

    void Muere()
    {
        Runner.Despawn(Object);
    }
   

    private bool IsGrounded()
    {
        RaycastHit hit;
        float distance = GetComponent<Collider>().bounds.extents.y + 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, out hit, distance);
    }
}






