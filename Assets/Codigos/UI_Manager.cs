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

    public void UpdatePlayerCount(int count)
    {
        playerCountText.text = count < 2 ? "Faltan más jugadores para empezar la partida" : "Jugadores en la partida: " + count;
    }

    public void MostrarPantallaPerdiste()
    {
        pantallaPerdiste.SetActive(true);
    }

    public void MostrarPantallaGanaste(Jugador ganador)
    {
        pantallaGanaste.SetActive(true);
        // Aquí puedes personalizar el mensaje de ganador si es necesario
    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}




