public abstract class PlayerBaseState
{
    protected PlayerInfo playerInfo;

    public PlayerBaseState(PlayerInfo playerinfo)
    {
        playerInfo = playerinfo;
    }

    public abstract void EnterState(GameStateManager playerContext);
}
