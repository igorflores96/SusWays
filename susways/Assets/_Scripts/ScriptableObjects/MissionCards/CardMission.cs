using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Carta de Missões", menuName = "Scriptable Objects/Data/Carta de Missões", order = 0)]
public class CardMission : ScriptableObject
{
    public string Title;
    [TextArea(5, 10)]
    public string Text;
    public List<MissionObjective> Objectives;
    public Sprite LockedIcon;
}
