using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Info", menuName = "Scriptable Objects/Data/Informações do Jogador", order = 0)]
public class PlayerInfo : ScriptableObject 
{
    public GameObject VisualPrefab;
    public string PlayerName;

    public void SetPlayerName(string value)
    {
        PlayerName = value;
    }
}
