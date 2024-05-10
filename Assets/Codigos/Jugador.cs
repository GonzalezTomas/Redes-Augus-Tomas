using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Jugador : NetworkBehaviour
{

    private CharacterController _controlador;
    private Vector3 _velocidad;
    private bool _saltarPresionado;

    public float VelocidadJugador = 2f;
    public float FuerzaSalto = 5f;
    public float ValorGravedad = -9.81f;

    private void Awake()
    {
        _controlador = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Saltar") && _controlador.isGrounded)
        {
            _saltarPresionado = true;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
        {
            return;
        }

        _velocidad.y += ValorGravedad * Runner.DeltaTime;
        if (_saltarPresionado)
        {
            _velocidad.y += FuerzaSalto;
            _saltarPresionado = false;
        }

        Vector3 mover = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * VelocidadJugador;
        _controlador.Move(mover + _velocidad * Runner.DeltaTime);
    }

}
