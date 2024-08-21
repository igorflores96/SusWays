using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private MatchData _matchData;
    private PlayerControl _playerControl;
    private PlayerBaseState _currentState;
    private List<PlayerInfo> _matchPlayers;
    private List<PlayerBaseState> _matchStatePlayers = new List<PlayerBaseState>();
    private GameObject _currentPlayerGrabed;
    private bool _isPlayerGrabed; //use this until we need to create a state machine
    private List<Vector3Int> _listToMove = new List<Vector3Int>();



    private void OnEnable() 
    {
        _playerControl = new PlayerControl();
        _isPlayerGrabed = false;

        _playerControl.PlayerGraber.ClickPlayer.performed += TryGrabPlayer;
        EventManager.OnListReady += UpdateList;
        
        EnablePlayerInput();
    }

    private void OnDisable() 
    {
        EventManager.OnListReady -= UpdateList;    
    }


    private void Start()
    {
        _matchPlayers = _matchData.MatchPlayerInfos;  
        _currentState = GenerateMatchPlayers(_matchData.MatchPlayerQuantity);
        
        Debug.Log("Game state manager está com o current state como: " + _currentState.GetType().Name);
        _currentState.EnterState(this);
    }
    public void SwitchState(PlayerBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void UpdateList(List<Vector3Int> list)
    {
        _listToMove = list;

        foreach(var position in _listToMove)
        {
            Debug.Log(position.x + " " + position.z);
        }
    }

    #region PlayerManagement

    private PlayerBaseState GenerateMatchPlayers(int playersQuantity)
    {
        _matchStatePlayers.Clear(); //we instantiate to use players in other places

        if(playersQuantity > 1)
        {            
            for(int i = 0; i < playersQuantity; i++)
            {
                PlayerBaseState newPlayerState = CreatePlayerState(i);

                if(newPlayerState != null)
                {
                    _matchStatePlayers.Add(newPlayerState);
                    EventManager.RequestPlayerSpawn(newPlayerState);
                    Debug.Log("Criou o player state: " + newPlayerState.GetType().Name);
                }
            }

        }
        
        int firstPlayerToPlay = Random.Range(0, _matchData.MatchPlayerQuantity);

        return _matchStatePlayers[firstPlayerToPlay];
    }

    private PlayerBaseState CreatePlayerState(int index)
    {
        switch (index)
        {
            case 0:
                PlayerOneState player1 = new PlayerOneState(_matchPlayers[index], _matchData.MatchHouses[index]);
                return player1;
            case 1:
                PlayerTwoState player2 = new PlayerTwoState(_matchPlayers[index], _matchData.MatchHouses[index]);
                return player2;
            case 2:
                PlayerThreeState player3 = new PlayerThreeState(_matchPlayers[index], _matchData.MatchHouses[index]);
                return player3;
            case 3:
                PlayerFourState player4 = new PlayerFourState(_matchPlayers[index], _matchData.MatchHouses[index]);
                return player4;
            default:
                return null;
        }
    }

    #endregion PlayerManagement

    #region Player and Inputs

    private void TryGrabPlayer(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if(!_isPlayerGrabed)
            {
                _currentPlayerGrabed = Mouse3D.GetPlayer();
                
                if(_currentPlayerGrabed == null)
                    return;
                
                if(_currentPlayerGrabed == _currentState.GetInstantiatePrefab())
                {
                    Debug.Log("Player do state: " + _currentState + " selecionado.");
                    _currentPlayerGrabed.transform.position = new Vector3(_currentPlayerGrabed.transform.position.x, 0.5f, _currentPlayerGrabed.transform.position.z);
                    EventManager.PlayerSelected(_currentState);
                    _isPlayerGrabed = true;
                }
            }
            else
                TryMove();

        }
    }

    private void TryMove()
    {
        Vector3 position = Mouse3D.GetMouseWorldPosition();

        int x = Mathf.FloorToInt(position.x / 2); //2 is cell size
        int z = Mathf.FloorToInt(position.z / 2);
        Vector3 newPosition = new Vector3(x * 2, 0f, z * 2);

        
        if(_listToMove.Contains(new Vector3Int(x, 0, z)))
        {
            _currentPlayerGrabed.transform.position = newPosition;
            _currentPlayerGrabed = null;
            _isPlayerGrabed = false;
            //EndTurn();
        }

    }

    private void EndTurn()
    {
        Transform transform = _currentState.GetInstantiatePrefab().transform;
        _currentState.UpdatePosition((int)transform.position.x / 2, (int)transform.position.z / 2);
    }

    public void EnablePlayerInput()
    {
        _playerControl.Enable();
    }

    public void DisablePlayerInput()
    {
        _playerControl.Disable();
    }

    #endregion Player and Inputs

}
