using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BusStop
{
    public int StopIndex;
    public List<Vector2Int> StopPositions;
    public Vector2Int SafePosition;

}
