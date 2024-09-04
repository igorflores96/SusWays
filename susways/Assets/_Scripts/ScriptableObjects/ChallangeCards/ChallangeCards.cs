using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Carta de Desafio", menuName = "Scriptable Objects/Data/Carta de Desafio", order = 0)]
public class ChallangeCards : ScriptableObject
{
    public string Title;
    [TextArea(5, 10)]
    public string Text;
    public List<ChallangeAnswer> _answers;
}
