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


    public PlayerBaseState(PlayerInfo playerinfo, Vector2Int houseToSpawnPosition, Mission mission)
    {
        CurrentDiceNumber = 0;
        this.CurrentPosition = houseToSpawnPosition;
        this.VisualPrefab = playerinfo.VisualPrefab;
        this.CurrentMission = mission;
        this.PlayerName = playerinfo.PlayerName;
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
}
