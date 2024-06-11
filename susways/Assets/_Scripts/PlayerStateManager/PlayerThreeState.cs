using UnityEngine;

public class PlayerThreeState : PlayerBaseState
{
    public PlayerThreeState(PlayerInfo playerInfo) : base(playerInfo) {}

    public override void EnterState(GameStateManager playerContext)
    {
        playerInfo.CurrentDiceNumber = Dice.RollSixDice();
    }
}
