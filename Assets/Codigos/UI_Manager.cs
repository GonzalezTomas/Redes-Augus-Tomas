using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI playerCountText;

    public GameObject pantallaPerdiste;
    public GameObject pantallaGanaste;
    public GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void UpdatePlayerCount(int count)
    {
        playerCountText.text = count < 2 ? "Faltan m�s jugadores para empezar la partida" : "Jugadores en la partida: " + count;
    }

    public void MostrarPantallaPerdiste(Jugador perdedor)
    {
        pantallaPerdiste.SetActive(true);
        // Puedes personalizar el mensaje de perdiste aqu� si es necesario
    }

    public void MostrarPantallaGanaste(Jugador ganador)
    {
        pantallaGanaste.SetActive(true);
        // Puedes personalizar el mensaje de ganador aqu� si es necesario
    }

    public void ReiniciarJuego()
    {
        pantallaPerdiste.SetActive(false);
        pantallaGanaste.SetActive(false);
        gameManager.ReiniciarJuego();
    }
}




