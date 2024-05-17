using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI playerCountText;

    public void UpdatePlayerCount(int count)
    {
        playerCountText.text = "faltan mas jugadores para empezar la partida ";
    }

    public GameObject pantallaPerdiste;
    public GameObject pantallaGanaste;

    public void MostrarPantallaPerdiste()
    {
        pantallaPerdiste.SetActive(true);
    }

    public void MostrarPantallaGanaste()
    {
        pantallaGanaste.SetActive(true);
    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
