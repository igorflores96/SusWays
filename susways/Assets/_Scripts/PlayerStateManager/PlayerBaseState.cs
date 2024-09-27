using UnityEngine;

public abstract class PlayerBaseState
{
    protected GameObject VisualPrefab { get; private set; }
    protected GameObject InstantiatePrefab { get; private set; }
    protected Mission CurrentMission { get; private set; }
    protected Vector2Int CurrentPosition { get; set; }
    protected int CurrentDiceNumber { get; set; }
    protected string PlayerName { get; set; }
    protected bool GainMoreMovement { get; set; }
    protected Mesh Mesh { get; set; }
    protected Color Color { get; set; }


    public PlayerBaseState(PlayerInfo playerinfo, Vector2Int houseToSpawnPosition, Mission mission)
    {
        CurrentDiceNumber = 0;
        this.CurrentPosition = houseToSpawnPosition;
        this.VisualPrefab = playerinfo.VisualPrefab;
        this.CurrentMission = mission;

        if(playerinfo.PlayerName == "")
            this.PlayerName = GetName();
        else
            this.PlayerName = playerinfo.PlayerName;
        
        if(playerinfo.Mesh == null)
            this.Mesh = playerinfo.DefaultMesh;
        else
            this.Mesh = playerinfo.Mesh;

        if(!playerinfo.ColorChange)
            this.Color = playerinfo.DefaultColor;
        else
            this.Color = playerinfo.Color;
    }

    public abstract void EnterState(GameStateManager playerContext);

    public void UpdatePosition(int x, int z)
    {
        CurrentPosition = new Vector2Int(x, z);
    }

    public GameObject GetVisualPrefab()
    {
        return VisualPrefab;
    }

    public GameObject GetInstantiatePrefab()
    {
        return InstantiatePrefab;
    }

    public Vector2Int GetPosition()
    {
        return CurrentPosition;
    }

    public int GetDiceNumber()
    {
        return CurrentDiceNumber;
    }

    public void SetInstantiatePrefab(GameObject prefab)
    {
        InstantiatePrefab = prefab;
    }

    public Mission GetMission()
    {
        return CurrentMission;
    }

    public string GetPlayerName()
    {
        return PlayerName;
    }

    public Mesh GetMesh()
    {
        return Mesh;
    }

    public Color GetColor()
    {
        return this.Color;
    }

    public bool PlayerIsOnObjective()
    {
        bool isOn = false;

        MissionObjective objectiveToCheck = CurrentMission.Objectives[CurrentMission.CurrentObjective];

        for(int i = 0; i < objectiveToCheck.GoalPositions.Count; i++)
        {
            if(CurrentPosition == objectiveToCheck.GoalPositions[i])
            {
                isOn = true;
                objectiveToCheck.IsComplete = true;
                CurrentMission.UpdateToNextObjective();
                break;
            }
        }

        return isOn;
    }

    public void GainMovement()
    {
        GainMoreMovement = true;
    }

    private string GetName()
    {
        string[] animalNames = { "Leão", "Tigre", "Elefante", "Girafa", "Rinoceronte", "Zebra", "Urso", "Lobo", "Cavalo", "Capivara" };
        int randomIndex = Random.Range(0, animalNames.Length);

        return animalNames[randomIndex];
    }
}
