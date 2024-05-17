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

    private bool gameStarted = false;

    private void Awake()
    {
        uiManager = FindObjectOfType<UI_Manager>();
        // Buscar autom�ticamente todos los objetos con la etiqueta "Player" y agregarlos a la lista de jugadores
        FindAndAddPlayers();
    }

    private void Update()
    {
        FindAndAddPlayers();

        if (!gameStarted)
        {
            // Verificar si hay al menos dos jugadores antes de comenzar la partida
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

        // Actualizar el texto de los jugadores
        uiManager.UpdatePlayerCount(players.Count);
    }

    
    public void JugadorMuere(Jugador jugadorMuerto)
    {
        // Mostrar la pantalla de "PERDISTE" al jugador que muri�
        uiManager.MostrarPantallaPerdiste();

        bool alguienGano = true; // Suponemos que alguien gan�

        // Verificar si otro jugador est� vivo
        foreach (Jugador jugador in players)
        {
            if (jugador != jugadorMuerto)
            {
                alguienGano = false; // Si otro jugador est� vivo, nadie ha ganado a�n
                break;
            }
        }

        // Si nadie m�s est� vivo, mostrar la pantalla de "GANASTE"
        if (alguienGano)
        {
            uiManager.MostrarPantallaGanaste();
        }
    }





    void FindAndAddPlayers()
    {
        // Buscar objetos con la etiqueta "Player" que no est�n en la lista y agregarlos
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
}

