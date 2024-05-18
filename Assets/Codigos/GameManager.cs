using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Fusion;
using System.Runtime.CompilerServices;

public class GameManager : NetworkBehaviour
{
    public List<Jugador> players = new List<Jugador>();
    public UI_Manager uiManager;
    public bool gameIsPaused;
    public GameObject textPlayersNecesarios;

    private bool gameStarted = false;

    private void Awake()
    {
        uiManager = FindObjectOfType<UI_Manager>();
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
            CondicionDeVictoriaODerrota();
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

    void CondicionDeVictoriaODerrota()
    {
        players.RemoveAll(player => player == null);

        if (players.Count == 1)
        {
            Jugador jugadorRestante = players[0];

            if (jugadorRestante.Vida > 0)
            {
                // Si solo queda un jugador con vida mayor que cero, muestra la pantalla de "Ganaste"
                uiManager.MostrarPantallaGanaste(jugadorRestante);
                PauseGame(); // Pausar el juego cuando se muestra la pantalla de ganaste
            }
            else
            {
                // Si el único jugador restante tiene vida cero, muestra la pantalla de "Perdiste"
                uiManager.MostrarPantallaPerdiste();
                PauseGame(); // Pausar el juego cuando se muestra la pantalla de perdiste
            }
        }
    }




    public void JugadorMuerto(Jugador jugador)
    {
        if (!gameStarted) return; // Evita mostrar la pantalla si el juego no ha comenzado aún
        uiManager.MostrarPantallaPerdiste();
    }

    public void JugadorEliminado(Jugador jugadorQueElimina, Jugador jugadorEliminado)
    {
        uiManager.MostrarPantallaGanaste(jugadorQueElimina);
    }
}



