using UnityEngine;

public class PlayerTwoState : PlayerBaseState
{
    public PlayerTwoState(PlayerInfo playerInfo, Vector2Int housePosition, Mission mission) : base(playerInfo, housePosition, mission) {}

    public override void EnterState(GameStateManager playerContext)
    {
        CurrentDiceNumber = GainMoreMovement ? Dice.RollSixDice() + 1 : Dice.RollSixDice();
        GainMoreMovement = false;
    }
}
