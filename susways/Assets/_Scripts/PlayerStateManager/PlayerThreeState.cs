using UnityEngine;

public class PlayerThreeState : PlayerBaseState
{
    public PlayerThreeState(PlayerInfo playerInfo, Vector2Int housePosition) : base(playerInfo, housePosition) {}

    public override void EnterState(GameStateManager playerContext)
    {
        CurrentDiceNumber = Dice.RollSixDice();
        Debug.Log("State Tres sorteou o dado no valor: " + CurrentDiceNumber);
    }
}
