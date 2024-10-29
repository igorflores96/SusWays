using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionObjective
{
    public Sprite NormalIcon;
    public Sprite CompletedIcon;
    public string Name;
    [TextArea(5, 10)]
    public string Text;
    public bool IsComplete;
    public List<Vector2> GoalPositions;
}
