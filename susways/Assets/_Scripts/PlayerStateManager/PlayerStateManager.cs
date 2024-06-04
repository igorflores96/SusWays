using System.Collections;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private PlayerInfo PlayerOne;
    [SerializeField] private PlayerInfo PlayerTwo;
    [SerializeField] private PlayerInfo PlayerThree;
    [SerializeField] private PlayerInfo PlayerFour;
    private PlayerBaseState PlayerOneState = new PlayerOneState();
    private PlayerBaseState PlayerTwoState = new PlayerTwoState();
    private PlayerBaseState PlayerThreeState = new PlayerThreeState();
    private PlayerBaseState PlayerFourState = new PlayerFourState();
    private PlayerBaseState _currentState;

    private void Start()
    {
        //We are going to need to modify this when players have the choice of how many of them are going to play  
        int index = Random.Range(1, 5);
        _currentState = SortState(index);
        _currentState.EnterState(this, GetPlayerInfo(_currentState));
    }
    public void SwitchState(PlayerBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this, GetPlayerInfo(_currentState));
    }

    private PlayerInfo GetPlayerInfo(PlayerBaseState state)
    {
        switch (state.GetType().Name)
        {
            case nameof(PlayerOneState):
                return PlayerOne;
            case nameof(PlayerTwoState):
                return PlayerTwo;
            case nameof(PlayerThreeState):
                return PlayerThree;
            case nameof(PlayerFourState):
                return PlayerFour;
            default:
                return null;
        }
    }

    private PlayerBaseState SortState(int index)
    {
        switch (index)
        {
            case 1:
                return PlayerOneState;
            case 2:
                return PlayerTwoState;
            case 3:
                return PlayerThreeState;
            case 4:
                return PlayerFourState;
            default:
                return null;
        }
    }

}
