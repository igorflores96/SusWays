using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStateManager : MonoBehaviour
{
    [Header("Match Infos")]
    [SerializeField] private MatchData _matchData;
    
    [Header("Missions")]
    [SerializeField] private List<CardMission> _missions;

    [Header("Bus")]
    [SerializeField] private Bus _bus;
    

    private PlayerControl _playerControl;
    private PlayerBaseState _currentState;
    private List<PlayerInfo> _matchPlayers;
    private List<PlayerBaseState> _matchStatePlayers = new List<PlayerBaseState>();
    private GameObject _currentPlayerGrabed;
    private bool _isPlayerGrabed; //use this until we need to create a state machine
    private List<Vector3Int> _listToMove = new List<Vector3Int>();
    private List<PlayerBaseState> _rankingList;
    private List<PlayerBaseState> _playersOnBus;
    private Vector3 _playerNewPosition;
    private int _busTurn;



    private void OnEnable() 
    {
        _playerControl = new PlayerControl();

        _busTurn = 0;
        _playerNewPosition = Vector3.zero;
       
        _rankingList = new List<PlayerBaseState>();
        _playersOnBus = new List<PlayerBaseState>();
        _isPlayerGrabed = false;

        _playerControl.PlayerGraber.ClickPlayer.performed += TryGrabPlayer;
        EventManager.OnListReady += UpdateList;
        EventManager.OnEndTurn += PlayerPassedTurn;
        EventManager.OnCofirmObjective += PlayerConfirmObjective;
        EventManager.OnAnimation += DisablePlayerInput;
        EventManager.OnAnimationOff += EnablePlayerInput;
        EventManager.OnBusProcessEnd += BusJumped;
        EventManager.OnPlayerEnterBus += PlayerConfirmEnterBus;
        EventManager.OnPlayerCancelEnterBus += PlayerCancelEnterBus;
        EventManager.OnPlayerMoveDone += PlayerGoToPosition;



        
        EnablePlayerInput();
    }

    private void OnDisable() 
    {
        EventManager.OnListReady -= UpdateList;   
        EventManager.OnEndTurn -= PlayerPassedTurn;
        EventManager.OnCofirmObjective -= PlayerConfirmObjective;
        EventManager.OnAnimation -= DisablePlayerInput;
        EventManager.OnAnimationOff -= EnablePlayerInput;
        EventManager.OnBusProcessEnd -= BusJumped;
        EventManager.OnPlayerEnterBus -= PlayerConfirmEnterBus;
        EventManager.OnPlayerCancelEnterBus -= PlayerCancelEnterBus;
        EventManager.OnPlayerMoveDone -= PlayerGoToPosition;
    }


    private void Start()
    {
        _matchPlayers = _matchData.MatchPlayerInfos;  
        _currentState = GenerateMatchPlayers(_matchData.MatchPlayerQuantity);
        _bus.InitFeedback(_matchData.MatchPlayerQuantity);
        
        _currentState.EnterState(this);
        EventManager.NewPlayerTurn(_currentState.GetMission(), _currentState);
    }
    public void SwitchState(PlayerBaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
        EventManager.NewPlayerTurn(_currentState.GetMission(), _currentState);
    }

    private void UpdateList(List<Vector3Int> list)
    {
        _listToMove = list;
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
                }
            }

        }
        
        int firstPlayerToPlay = Random.Range(0, playersQuantity);

        return _matchStatePlayers[firstPlayerToPlay];
    }

    private PlayerBaseState CreatePlayerState(int index)
    {
        Mission mission = GetMission();
        switch (index)
        {
            case 0:
                PlayerOneState player1 = new PlayerOneState(_matchPlayers[index], _matchData.MatchHouses[index], mission);
                return player1;
            case 1:
                PlayerTwoState player2 = new PlayerTwoState(_matchPlayers[index], _matchData.MatchHouses[index], mission);
                return player2;
            case 2:
                PlayerThreeState player3 = new PlayerThreeState(_matchPlayers[index], _matchData.MatchHouses[index], mission);
                return player3;
            case 3:
                PlayerFourState player4 = new PlayerFourState(_matchPlayers[index], _matchData.MatchHouses[index], mission);
                return player4;
            default:
                return null;
        }
    }

    private Mission GetMission()
    {
        CardMission card = _missions[Random.Range(0, _missions.Count)];
        Mission mission = new Mission(card);
        _missions.Remove(card);

        return mission;
    }

    private void PlayerPassedTurn()
    {
        UpdatePlayerPosition();
        if(PlayerAchievedObjective())
        {
            DisablePlayerInput();
            
            Mission playerMission = _currentState.GetMission();
            EventManager.PlayerCompleteObjective(playerMission);
        }
        else
            PassToNextTun();
    }

    private void PlayerConfirmObjective()
    {
        CheckEndGame();
    }

    public void PassToNextTun()
    {
        _busTurn = (_busTurn + 1) % _matchStatePlayers.Count;

        if(_busTurn == 0)
        {
            _playersOnBus = _bus.GoToNextStop(_matchStatePlayers);
            EventManager.ShouldHideUI();
            DisablePlayerInput();

            float offset = 0.5f;
            Vector2[] offsets = new Vector2[]
            {
                new Vector2(offset, offset),
                new Vector2(-offset, offset),
                new Vector2(-offset, -offset),
                new Vector2(offset, -offset)
            };

            for (int i = 0; i < _matchStatePlayers.Count; i++)
            {
                Vector2Int playerPosition = _matchStatePlayers[i].GetPosition();
                Transform playerTransform = _matchStatePlayers[i].GetInstantiatePrefab().transform;

                if(_bus.BusPositions.Contains(playerPosition))
                {
                    _matchStatePlayers[i].UpdatePosition(_bus.LandingSpot.x, _bus.LandingSpot.y);

                    Vector3 targetPosition = new Vector3(
                        _matchStatePlayers[i].GetPosition().x * 2 + offsets[i].x, playerTransform.position.y, _matchStatePlayers[i].GetPosition().y * 2 + offsets[i].y);

                    if (playerTransform.position != targetPosition)
                    {
                        playerTransform.position = targetPosition;
                    }
                }
            }
        }
        else
        {
            int nextState = (_matchStatePlayers.IndexOf(_currentState) + 1) % _matchStatePlayers.Count;
            SwitchState(_matchStatePlayers[nextState]);
        }
    }

    private void BusJumped()
    {
        if(_playersOnBus.Count > 0)
        {
            UpdatePlayersBus();
        }
        else
        {
            int nextState = (_matchStatePlayers.IndexOf(_currentState) + 1) % _matchStatePlayers.Count;
            SwitchState(_matchStatePlayers[nextState]);
        }
    }

    private void CheckEndGame()
    {
        Mission playerMission = _currentState.GetMission();
        
        if(playerMission.MissionComplete)
        {
            _matchStatePlayers.Remove(_currentState);
            _rankingList.Add(_currentState);
        }

        if(_matchStatePlayers.Count == 0)
            EventManager.TheGameIsEnd(_rankingList);
        else
        {
            EnablePlayerInput();
            PassToNextTun();
        }
    }
    private void UpdatePlayerPosition()
    {
        Transform transform = _currentState.GetInstantiatePrefab().transform;
        Vector2 precisePosition = new Vector2(transform.position.x, transform.position.z);
        Vector2 newPosition = new Vector2(Mathf.RoundToInt(precisePosition.x / 2), Mathf.RoundToInt(precisePosition.y / 2));

        _currentState.UpdatePosition((int)newPosition.x, (int)newPosition.y);

        List<PlayerBaseState> playersInSamePosition = new List<PlayerBaseState>();

        foreach (PlayerBaseState player in _matchStatePlayers)
        {
            Vector2Int playerPosition = player.GetPosition();

            if (playerPosition == new Vector2Int((int)newPosition.x, (int)newPosition.y))
            {
                playersInSamePosition.Add(player);
            }
        }

        if (playersInSamePosition.Count > 1)
        {
            float offset = 0.5f;
            Vector2[] offsets = new Vector2[]
            {
                new Vector2(offset, offset),
                new Vector2(-offset, offset),
                new Vector2(-offset, -offset),
                new Vector2(offset, -offset)
            };

            for (int i = 0; i < playersInSamePosition.Count; i++)
            {
                Transform playerTransform = playersInSamePosition[i].GetInstantiatePrefab().transform;

                Vector3 targetPosition = new Vector3(
                    playersInSamePosition[i].GetPosition().x * 2 + offsets[i].x, playerTransform.position.y, playersInSamePosition[i].GetPosition().y * 2 + offsets[i].y);

                if (playerTransform.position != targetPosition)
                {
                    playerTransform.position = targetPosition;
                }
            }
        }
    }

    private void UpdatePlayersBus()
    {
        float offset = 0.5f;
        Vector2[] offsets = new Vector2[]
        {
            new Vector2(offset, offset),
            new Vector2(-offset, offset),
            new Vector2(-offset, -offset),
            new Vector2(offset, -offset)
        };

        for (int i = 0; i < _playersOnBus.Count; i++)
        {
            Transform playerTransform = _playersOnBus[i].GetInstantiatePrefab().transform;
            _playersOnBus[i].UpdatePosition(_bus.LandingSpot.x, _bus.LandingSpot.y);

            Vector3 targetPosition = new Vector3(
                _playersOnBus[i].GetPosition().x * 2 + offsets[i].x, playerTransform.position.y, _playersOnBus[i].GetPosition().y * 2 + offsets[i].y);

            if (playerTransform.position != targetPosition)
            {
                playerTransform.position = targetPosition;
                
                if(_playersOnBus[i].GetInstantiatePrefab().TryGetComponent(out PlayerPawn pawn))
                {
                    pawn.PlayerExitBus();
                }
            }


        }

        _playersOnBus.Clear();
        _bus.ResetPlayerQuantityFeedback(_matchData.MatchPlayerQuantity);
        int nextState = (_matchStatePlayers.IndexOf(_currentState) + 1) % _matchStatePlayers.Count;
        SwitchState(_matchStatePlayers[nextState]);
        EventManager.ShouldShowUI();
    }

    private bool PlayerAchievedObjective()
    {
        return _currentState.PlayerIsOnObjective();
    }

    #endregion PlayerManagement

    #region Player

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
                    EventManager.PlayerSelected(_currentState);
                    EventManager.ShouldHideUI();
                    _isPlayerGrabed = true;

                    if(_currentPlayerGrabed.TryGetComponent(out PlayerPawn pawn))
                    {
                        pawn.PlayerGrabed();
                    }
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
        _playerNewPosition = new Vector3(x * 2, 0f, z * 2);

        
        if(_listToMove.Contains(new Vector3Int(x, 0, z)))
        {
            if(_bus.BusPositions.Contains(new Vector2Int(x, z)))
            {
                EventManager.PlayerTryEnterOnBusStop();
                EventManager.ShouldHideUI();
                DisablePlayerInput();
            }
            else
            {
                if(_currentPlayerGrabed.TryGetComponent(out PlayerPawn pawn))
                {
                    pawn.PlayerMove();
                    DisablePlayerInput();
                }
            }
        }

    }

    public void PlayerGoToPosition() //when player pawn desapears, call this function
    {
        _currentPlayerGrabed.transform.position = _playerNewPosition;
        _currentPlayerGrabed = null;
        _isPlayerGrabed = false;
        _playerNewPosition = Vector3.zero;
        EnablePlayerInput();
        EventManager.ShouldShowUI();
    }

    private void PlayerConfirmEnterBus()
    {
        if(_currentPlayerGrabed.TryGetComponent(out PlayerPawn pawn))
        {
            pawn.PlayerEnterBus();
            _bus.AddPlayerOnBus(_matchData.MatchPlayerQuantity);
            EventManager.ShouldShowUI();
        }  
    }

    private void PlayerCancelEnterBus()
    {
        if(_currentPlayerGrabed.TryGetComponent(out PlayerPawn pawn))
        {
            pawn.PlayerCancelMove();
        }
       
        _currentPlayerGrabed = null;
        _isPlayerGrabed = false;
        _playerNewPosition = Vector3.zero;
        EventManager.ShouldShowUI();
        EnablePlayerInput();
    }

    #endregion Player

    #region Inputs

    public void EnablePlayerInput()
    {
        _playerControl.Enable();
    }

    public void DisablePlayerInput()
    {
        _playerControl.Disable();
    }

    #endregion Inputs

}
