using UnityEngine;

public class PlayerThreeState : PlayerBaseState
{
    public PlayerThreeState(PlayerInfo playerInfo, Vector2Int housePosition, Mission mission) : base(playerInfo, housePosition, mission) {}

    public override void EnterState(GameStateManager playerContext)
    {
        CurrentDiceNumber = Dice.RollSixDice();
    }
}
