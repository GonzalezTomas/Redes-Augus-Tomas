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
    public Bala _bala;
    public Transform _apareceBala;

    
    private Rigidbody rb;
    private float horizontalInput;
    private bool _disparo;
    private bool canDoubleJump = false;

    private bool dashActivado = false;
    public float duracionDash;
    public float velocidadDash; 
    private float dashEnfriado = 3f;
    private float UltimoDash = -Mathf.Infinity;

    public GameManager gameManager;

    [Networked, OnChangedRender(nameof(OnNetHealtChanged))]
    public float Vida { get; set; } = 100;

    
    void OnNetHealtChanged() => Debug.Log($"Vida = {Vida}");

    

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Muere()
    {
        gameManager.JugadorMuere(this);
        Runner.Despawn(Object);
    }


    public override void Spawned()
    {
        rb = GetComponent<Rigidbody>();

        if (!HasStateAuthority) return;

        Camera.main.GetComponent<Camara>()?.Target(transform);
    }

    
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0f, 0f);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                canDoubleJump = false;
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _disparo = true;
        }

       
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= UltimoDash + dashEnfriado && !dashActivado)
        {
            StartCoroutine(Dash());
            UltimoDash = Time.time;
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

        if (_disparo)
        {
            ApareceBala();
            _disparo = false;
        }
    }

    
    void ApareceBala()
    {
        Runner.Spawn(_bala, _apareceBala.position, _apareceBala.rotation);
    }

    
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
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


    


    private bool IsGrounded()
    {
        RaycastHit hit;
        float distance = GetComponent<Collider>().bounds.extents.y + 0.1f;
        return Physics.Raycast(transform.position, Vector3.down, out hit, distance);
    }

    
    IEnumerator Dash()
    {
        dashActivado = true;

        
        rb.velocity = transform.forward * velocidadDash;

      
        yield return new WaitForSeconds(duracionDash);

        
        rb.velocity = Vector3.zero;

        dashActivado = false;
    }
}







