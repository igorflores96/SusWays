using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Match Data", menuName = "Scriptable Objects/Data/Informações da Partida", order = 0)]
public class MatchData : ScriptableObject
{
    public int MatchPlayerQuantity;
    public List<PlayerInfo> MatchPlayerInfos = new List<PlayerInfo>();
    public List<Vector2Int> MatchHouses = new List<Vector2Int>();

    public void AddPlayerOnMatch(PlayerInfo playerInfo)
    {
        if(!MatchPlayerInfos.Contains(playerInfo))
        {
            MatchPlayerQuantity++;
            MatchPlayerInfos.Add(playerInfo);
        }
    }

    public void RemovePlayerOnMatch(PlayerInfo playerInfo)
    {
        if(MatchPlayerInfos.Contains(playerInfo))
        {
            MatchPlayerQuantity--;
            MatchPlayerInfos.Remove(playerInfo);
        }
    }

}
