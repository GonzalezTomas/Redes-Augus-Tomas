using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class GameManager : NetworkBehaviour
{
    public List<Jugador> players = new List<Jugador>();
    public UI_Manager uiManager;
    public bool gameIsPaused;
    public GameObject textPlayersNecesarios;
    public Respawn respawn;

    private bool gameStarted = false;

    private void Awake()
    {
        uiManager = FindObjectOfType<UI_Manager>();
        respawn = FindObjectOfType<Respawn>();
        FindAndAddPlayers();
    }

    private void Update()
    {
        FindAndAddPlayers();

        if (!gameStarted)
        {
            if (players.Count < 2)
            {
                textPlayersNecesarios.SetActive(true);
                PauseGame();
            }
            else
            {
                StartGame();
            }
        }

        if (gameStarted)
        {
            CheckPlayerStates();
        }

        uiManager.UpdatePlayerCount(players.Count);
    }

    void FindAndAddPlayers()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("jugador");
        foreach (GameObject playerObject in playerObjects)
        {
            Jugador playerComponent = playerObject.GetComponent<Jugador>();
            if (playerComponent != null && !players.Contains(playerComponent))
            {
                players.Add(playerComponent);
            }
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        textPlayersNecesarios.SetActive(false);
        gameStarted = true;
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
    }

    void CheckPlayerStates()
    {
        players.RemoveAll(player => player == null);

        if (gameStarted && players.Count == 1)
        {
            Jugador jugadorGanador = players[0];
            Jugador jugadorPerdedor = players.Find(player => player != jugadorGanador);

            // Mostrar pantalla de ganaste al jugador ganador
            uiManager.MostrarPantallaGanaste(jugadorGanador);

            // Mostrar pantalla de perdiste al jugador perdedor
            uiManager.MostrarPantallaPerdiste(jugadorPerdedor);
        }
        
    }


    public void ReiniciarJuego()
    {
        players.Clear();
        gameStarted = false;
        FindAndAddPlayers();
        respawn.RespawnPlayers();
        StartGame();
    }
}



