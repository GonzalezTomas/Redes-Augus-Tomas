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
            RespawnPlayer(Jugador);
        }
    }

    public void RespawnPlayer(PlayerRef Jugador)
    {
        Vector3 posicion = new Vector3(-4.25f, 3.86f, 0f);
        Runner.Spawn(_jugardorPrefab, posicion);
    }

    public void RespawnPlayers()
    {
        // Aquí debes incluir la lógica para respawnear a todos los jugadores que deben estar en el juego
        foreach (PlayerRef jugador in Runner.ActivePlayers)
        {
            RespawnPlayer(jugador);
        }
    }
}

