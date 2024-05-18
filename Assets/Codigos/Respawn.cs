using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Respawn : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] private GameObject _jugardorPrefab;
    [SerializeField] private Transform[] _spawnPoints; // Los dos puntos de spawn

    public void PlayerJoined(PlayerRef Jugador)
    {
        if (Jugador == Runner.LocalPlayer)
        {
            // Seleccionar aleatoriamente uno de los dos puntos de spawn
            Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            // Spawnear el jugador en el punto seleccionado
            Runner.Spawn(_jugardorPrefab, spawnPoint.position);
        }
    }
}


