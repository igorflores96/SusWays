using UnityEngine;

public abstract class PlayerBaseState
{
    protected GameObject VisualPrefab { get; private set; }
    protected GameObject InstantiatePrefab { get; private set; }
    protected CardMission CurrentMission { get; private set; }
    protected Vector2Int CurrentPosition { get; set; }
    protected int CurrentDiceNumber { get; set; }

    public PlayerBaseState(PlayerInfo playerinfo, Vector2Int houseToSpawnPosition)
    {
        CurrentDiceNumber = 0;
        this.CurrentPosition = houseToSpawnPosition;
        this.VisualPrefab = playerinfo.VisualPrefab;
        this.CurrentMission = playerinfo.CurrentMission;
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
}
