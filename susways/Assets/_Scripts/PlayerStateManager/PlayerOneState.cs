using UnityEngine;

public class PlayerOneState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager playerContext, PlayerInfo playerInfo)
    {
        playerInfo.CurrentDiceNumber = Dice.RollSixDice();
    }
}
