using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dados do Mapa", menuName = "Scriptable Objects/Data/Dados do Mapa", order = 0)]
public class MapData : ScriptableObject
{
    private List<Vector2Int> TilePositions;
    public List<bool> WalkableStatus;
    public Dictionary<Vector2Int, bool> TileMap;

    public void InitDicionary(int width, int height)
    {
        TilePositions = new List<Vector2Int>();
        TileMap = new Dictionary<Vector2Int, bool>();

        for(int xSize = 0; xSize < width; xSize++)
        {
            for(int zSize = 0; zSize < height; zSize++)
            {
                TilePositions.Add(new Vector2Int(xSize, zSize));
            }
        }

        if(TilePositions.Count == WalkableStatus.Count)
        {
            for(int i = 0; i < TilePositions.Count; i++)
            {
                if(!TileMap.ContainsKey(TilePositions[i]))
                {
                    TileMap.Add(TilePositions[i], WalkableStatus[i]);
                }
            }

            // foreach(var key in TileMap) //Debug para verificar os valores do dicionario
            // {
            //     Debug.Log(key.Key + ": " + key.Value);
            // }
        }
        else
        {
            Debug.Log("Lists' size must be the same.");
        }
    }


    public bool GetWalkableStatus(Vector2Int tile)
    {
        if(TileMap.ContainsKey(tile))
            return TileMap[tile];
        else
            return false;
    }
}



/*[System.Serializable]
public class TileInfo
{
    public string Name;
    public bool IsWalkable;
    public Vector2Int[] Positions;
    public Vector2Int[] WalkablePositions;
}*/
