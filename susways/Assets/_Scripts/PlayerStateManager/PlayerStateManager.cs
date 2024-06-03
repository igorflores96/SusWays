using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private PlayerInfo PlayerOne;
    [SerializeField] private PlayerInfo PlayerSecond;
    [SerializeField] private PlayerInfo PlayerThree;
    [SerializeField] private PlayerInfo PlayerFour;
    private PlayerBaseState PlayerOneState = new PlayerOneState();
    private PlayerBaseState PlayerTwoState = new PlayerTwoState();
    private PlayerBaseState PlayerThreeState = new PlayerThreeState();
    private PlayerBaseState PlayerFourState = new PlayerFourState();
    private PlayerBaseState _currentState;


    public void SwitchState(PlayerBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

}
