using UnityEngine;

public class PlayerTwoState : PlayerBaseState
{
    public PlayerTwoState(PlayerInfo playerInfo, Vector2Int housePosition, Mission mission) : base(playerInfo, housePosition, mission) {}

    public override void EnterState(GameStateManager playerContext)
    {
        if(GainMoreMovement)
        {
            CurrentDiceNumber = Dice.RollSixDice() + 1;
            EventManager.PlayerHasBonus();
        }
        else
            CurrentDiceNumber = Dice.RollSixDice();

        GainMoreMovement = false;
    }
}
