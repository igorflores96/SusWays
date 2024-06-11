using UnityEngine;

public class PlayerFourState : PlayerBaseState
{
    public PlayerFourState(PlayerInfo playerInfo, Vector2Int housePosition) : base(playerInfo, housePosition) {}

    public override void EnterState(GameStateManager playerContext)
    {
        CurrentDiceNumber = Dice.RollSixDice();
        Debug.Log("State Quatro sorteou o dado no valor: " + CurrentDiceNumber);
    }
}
