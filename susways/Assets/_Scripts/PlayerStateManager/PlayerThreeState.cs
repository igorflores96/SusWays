using UnityEngine;

public class PlayerThreeState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager playerContext, PlayerInfo playerInfo)
    {
        playerInfo.CurrentDiceNumber = Dice.RollSixDice();
    }
}
