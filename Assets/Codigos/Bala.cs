using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : NetworkBehaviour
{
    public float _velocidad;
    public float _danio;
    public float _tiempo;

   private TickTimer _tickTiempo;

    public override void Spawned()
    {
       GetComponent<Rigidbody>().AddForce(transform.forward * _velocidad, ForceMode.VelocityChange); 

        _tickTiempo = TickTimer.CreateFromSeconds(Runner, _tiempo);
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority) return;

        if (!_tickTiempo.Expired(Runner)) return;
        Runner.Despawn(Object);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!HasStateAuthority) return;

        if (other.gameObject.layer == 6)
        {
            other.GetComponent<Jugador>().RPC_RecibirDaño(_danio);
        }

        Runner.Despawn(Object);
    }

}
