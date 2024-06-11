using UnityEngine;

public class PlayerThreeState : PlayerBaseState
{
    public PlayerThreeState(PlayerInfo playerInfo) : base(playerInfo) {}

    public override void EnterState(GameStateManager playerContext)
    {
        base.playerInfo.CurrentDiceNumber = Dice.RollSixDice();
        Debug.Log("State Tres sorteou o dado no valor: " + base.playerInfo.CurrentDiceNumber);
    }
}
