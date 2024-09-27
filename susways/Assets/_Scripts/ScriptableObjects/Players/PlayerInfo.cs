using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Info", menuName = "Scriptable Objects/Data/Informações do Jogador", order = 0)]
public class PlayerInfo : ScriptableObject 
{
    public GameObject VisualPrefab;
    public string PlayerName;
    public Mesh DefaultMesh;
    public Color DefaultColor;
    [NonSerialized] public Mesh Mesh;
    [NonSerialized] public Color Color;
    [NonSerialized] public int CurrentMeshIndex = 0;
    [NonSerialized] public int CurrentColorIndex = 0;
    [NonSerialized] public bool ColorChange = false;

    public void SetPlayerName(string value)
    {
        PlayerName = value;
    }

    public void SetPlayerMesh(Mesh mesh)
    {
        Mesh = mesh;
    }

    public void SetPlayerColor(Color color)
    {
        ColorChange = true;
        Color = color;
    }
}
