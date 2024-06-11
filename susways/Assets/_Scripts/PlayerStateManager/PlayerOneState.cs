using UnityEngine;

public class PlayerOneState : PlayerBaseState
{
    public PlayerOneState(PlayerInfo playerInfo) : base(playerInfo) {}
    public override void EnterState(GameStateManager playerContext)
    {
        base.playerInfo.CurrentDiceNumber = Dice.RollSixDice();
        Debug.Log("State Um sorteou o dado no valor: " + base.playerInfo.CurrentDiceNumber);

    }
}
