using UnityEngine;

public class PlayerOneState : PlayerBaseState
{
    public PlayerOneState(PlayerInfo playerInfo, Vector2Int housePosition) : base(playerInfo, housePosition) {}
    public override void EnterState(GameStateManager playerContext)
    {
        CurrentDiceNumber = Dice.RollSixDice();
        Debug.Log("State Um sorteou o dado no valor: " + CurrentDiceNumber);

    }
}
