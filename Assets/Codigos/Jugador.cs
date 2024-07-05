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
    public string username;

    public static int nextPlayerID = 1; // Variable estática para el próximo ID de jugador
    public int playerID; // ID único del jugador

    private Rigidbody rb;
    private float horizontalInput;
    public bool _disparo;
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
        playerID = nextPlayerID; // Asignar el ID único al jugador
        nextPlayerID++; // Incrementar el contador de ID para el próximo jugador
        anim = GetComponent<Animator>();
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

        Animaciones();

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

    private void Animaciones()
    {
        bool corriendo = Mathf.Abs(horizontalInput) > 0.1f;
        anim.SetBool("run", corriendo);
        anim.SetBool("idle", !corriendo);
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
            gameManager.JugadorMuerto(this); // Llama al GameManager cuando el jugador muere
        }
    }

    public void Muere()
    {
        FindObjectOfType<GameManager>().JugadorMuerto(this);
        Runner.Despawn(Object);
    }

    public void EliminarOtroJugador(Jugador jugadorEliminado)
    {
        jugadorEliminado.Local_RecibirDaño(danio);
        gameManager.JugadorEliminado(this, jugadorEliminado); // Llama al GameManager cuando elimina a otro jugador
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
