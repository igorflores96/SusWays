using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionObjective
{
    public Sprite NormalIcon;
    public Sprite CompletedIcon;
    public string Name;
    public bool IsComplete;
    public List<Vector2> GoalPositions;
}
