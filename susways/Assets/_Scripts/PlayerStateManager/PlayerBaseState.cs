using UnityEngine;

public abstract class PlayerBaseState
{
    protected GameObject VisualPrefab { get; private set; }
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

    public GameObject GetVisualPrefab()
    {
        return VisualPrefab;
    }

    public Vector2Int GetPosition()
    {
        return CurrentPosition;
    }
}
