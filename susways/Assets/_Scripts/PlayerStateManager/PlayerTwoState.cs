using UnityEngine;

public class PlayerTwoState : PlayerBaseState
{
    public PlayerTwoState(PlayerInfo playerInfo, Vector2Int housePosition) : base(playerInfo, housePosition) {}

    public override void EnterState(GameStateManager playerContext)
    {
        CurrentDiceNumber = Dice.RollSixDice();
        Debug.Log("State Dois sorteou o dado no valor: " + CurrentDiceNumber);
    }
}
