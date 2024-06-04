using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Info", menuName = "Scriptable Objects/Data/Informações do Jogador", order = 0)]
public class PlayerInfo : ScriptableObject 
{
    public GameObject VisualPrefab;
    public CardMission CurrentMission;
    [NonSerialized] public Vector2Int CurrentPosition;
    [NonSerialized] public int CurrentDiceNumber = 0;
}
