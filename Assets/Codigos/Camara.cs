using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    private Transform _jugador;

    public void Target(Transform targetJugador)
    {
        _jugador = targetJugador;  
    }

    
   private void LateUpdate()
   {
        if (!_jugador) return;

        Vector3 posicion = transform.position;
        posicion.x = _jugador.position.x;   
        transform.position = posicion;

        
   }
}
