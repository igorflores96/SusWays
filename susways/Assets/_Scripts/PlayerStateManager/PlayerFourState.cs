using UnityEngine;

public class PlayerFourState : PlayerBaseState
{
    public PlayerFourState(PlayerInfo playerInfo) : base(playerInfo) {}

    public override void EnterState(GameStateManager playerContext)
    {
        base.playerInfo.CurrentDiceNumber = Dice.RollSixDice();
        Debug.Log("State Quatro sorteou o dado no valor: " + base.playerInfo.CurrentDiceNumber);
    }
}
