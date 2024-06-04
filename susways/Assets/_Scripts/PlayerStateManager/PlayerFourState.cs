using UnityEngine;

public class PlayerFourState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager playerContext, PlayerInfo playerInfo)
    {
        playerInfo.CurrentDiceNumber = Dice.RollSixDice();
    }
}
