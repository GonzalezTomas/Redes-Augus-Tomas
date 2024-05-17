using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI playerCountText;

    public void UpdatePlayerCount(int count)
    {
        playerCountText.text = "faltan mas jugadores para empezar la partida ";
    }
}
