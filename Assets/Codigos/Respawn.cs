using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Respawn : SimulationBehaviour, IPlayerJoined    
{
    [SerializeField] private GameObject _jugardorPrefab;

    public void PlayerJoined(PlayerRef Jugador)
    {
        if (Jugador == Runner.LocalPlayer)
        {
            Vector3 posicion = new Vector3 (-4.25f, 2.5f, 0f);
            Runner.Spawn(_jugardorPrefab, posicion);
        }
    }
}
