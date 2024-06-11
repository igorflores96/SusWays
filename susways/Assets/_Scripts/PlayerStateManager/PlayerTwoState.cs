using UnityEngine;

public class PlayerTwoState : PlayerBaseState
{
    public PlayerTwoState(PlayerInfo playerInfo) : base(playerInfo) {}

    public override void EnterState(GameStateManager playerContext)
    {
        base.playerInfo.CurrentDiceNumber = Dice.RollSixDice();
        Debug.Log("State Dois sorteou o dado no valor: " + base.playerInfo.CurrentDiceNumber);
    }
}
