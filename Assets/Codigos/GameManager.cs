using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Jugador> players = new List<Jugador>();
    public UI_Manager uiManager;
    public bool gameIsPaused;
    
    public GameObject textPlayersNecesarios;

    private bool gameStarted = false;

    private void Awake()
    {
        uiManager = FindObjectOfType<UI_Manager>();
        // Buscar automáticamente todos los objetos con la etiqueta "Player" y agregarlos a la lista de jugadores
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

    void FindAndAddPlayers()
    {
        // Buscar objetos con la etiqueta "Player" que no estén en la lista y agregarlos
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

