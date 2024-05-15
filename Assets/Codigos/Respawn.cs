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
            Runner.Spawn(_jugardorPrefab, Vector3.zero);
        }
    }
}
