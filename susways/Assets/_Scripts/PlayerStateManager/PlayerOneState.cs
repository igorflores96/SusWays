using UnityEngine;

public class PlayerOneState : PlayerBaseState
{
    public PlayerOneState(PlayerInfo playerInfo, Vector2Int housePosition, Mission mission) : base(playerInfo, housePosition, mission) {}
    
    public override void EnterState(GameStateManager playerContext)
    {
        CurrentDiceNumber = GainMoreMovement ? Dice.RollSixDice() + 1 : Dice.RollSixDice();
        
        if(GainMoreMovement)
            Debug.Log("Estava com bonus");
            
        GainMoreMovement = false;
    }
}
