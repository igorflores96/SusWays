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
            MatchPlayerInfos.Add(playerInfo);
        }
    }

    public void RemovePlayerOnMatch(PlayerInfo playerInfo)
    {
        if(MatchPlayerInfos.Contains(playerInfo))
        {
            MatchPlayerInfos.Remove(playerInfo);
        }
    }

    public void PrepareNewGame()
    {
        MatchPlayerInfos.Clear();
        MatchPlayerQuantity = 2;
    }

    public void UpdateMatchPlayerQuantity(bool playerShouldBeAdded)
    {
        if(playerShouldBeAdded)
            MatchPlayerQuantity++;
        else
            MatchPlayerQuantity--;

        if(MatchPlayerQuantity < 2)
            MatchPlayerQuantity = 2;
        else if(MatchPlayerQuantity > 4)
            MatchPlayerQuantity = 4;

    }

}
