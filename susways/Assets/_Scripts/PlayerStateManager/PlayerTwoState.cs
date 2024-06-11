using UnityEngine;

public class PlayerTwoState : PlayerBaseState
{
    public PlayerTwoState(PlayerInfo playerInfo) : base(playerInfo) {}

    public override void EnterState(GameStateManager playerContext)
    {
        playerInfo.CurrentDiceNumber = Dice.RollSixDice();
    }
}
