using UnityEngine;

public class PlayerFourState : PlayerBaseState
{
    public PlayerFourState(PlayerInfo playerInfo) : base(playerInfo) {}

    public override void EnterState(GameStateManager playerContext)
    {
        playerInfo.CurrentDiceNumber = Dice.RollSixDice();
    }
}
