using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private MatchData _matchData;
    
    [Header("Buttons")]
    [SerializeField] private Button _openPlayerThree;
    [SerializeField] private Button _openPlayerFour;
    [SerializeField] private Button _confirmMatch;

    [Header(" - Close Buttons")]
    [SerializeField] private Button _closePlayerThree;
    [SerializeField] private Button _closePlayerFour;
    
    [Header(" - Confirm Buttons")]
    [SerializeField] private Button _confirmPlayerOne;
    [SerializeField] private Button _confirmPlayerTwo;
    [SerializeField] private Button _confirmPlayerThree;
    [SerializeField] private Button _confirmPlayerFour;

    [Header("Cancel Confirm Buttons")]
    [SerializeField] private Button _cancelConfirmPlayerOne;
    [SerializeField] private Button _cancelConfirmPlayerTwo;
    [SerializeField] private Button _cancelConfirmPlayerThree;
    [SerializeField] private Button _cancelConfirmPlayerFour;

    [Header("Canvases")]
    [SerializeField] private GameObject _playerThreeClosed;
    [SerializeField] private GameObject _playerFourClosed;
    [SerializeField] private GameObject _playerThreeOpened;
    [SerializeField] private GameObject _playerFourOpened;

    [Header("Player Infos")]
    [SerializeField] private PlayerInfo _playerOne;
    [SerializeField] private PlayerInfo _playerTwo;
    [SerializeField] private PlayerInfo _playerThree;
    [SerializeField] private PlayerInfo _playerFour;

    private int _currentPlayersConfirmed;
    private bool _playerThreeConfirmed;
    private bool _playerFourConfirmed;


    private void OnEnable() 
    {
        _currentPlayersConfirmed = 0;

        _confirmMatch.interactable = false;
        _playerThreeConfirmed = false;
        _playerFourConfirmed = false;
       
        _openPlayerThree.onClick.AddListener(OpenThreePlayer);
        _openPlayerFour.onClick.AddListener(OpenFourPlayer);
        
        _confirmPlayerOne.onClick.AddListener(() => ConfirmPlayer(0));
        _confirmPlayerTwo.onClick.AddListener(() => ConfirmPlayer(1));
        _confirmPlayerThree.onClick.AddListener(() => ConfirmPlayer(2));
        _confirmPlayerFour.onClick.AddListener(() => ConfirmPlayer(3));
        
        _closePlayerThree.onClick.AddListener(CloseThreePlayer);
        _closePlayerFour.onClick.AddListener(CloseFourPlayer);
        
        _cancelConfirmPlayerOne.onClick.AddListener(() => CancelPlayerConfirm(0));
        _cancelConfirmPlayerTwo.onClick.AddListener(() => CancelPlayerConfirm(1));
        _cancelConfirmPlayerThree.onClick.AddListener(() => CancelPlayerConfirm(2));
        _cancelConfirmPlayerFour.onClick.AddListener(() => CancelPlayerConfirm(3));

        _confirmMatch.onClick.AddListener(StartMatch);

    }

    private void OnDisable() 
    {
        _openPlayerThree.onClick.RemoveListener(OpenThreePlayer);
        _openPlayerFour.onClick.RemoveListener(OpenFourPlayer);
        
        _confirmPlayerOne.onClick.RemoveListener(() => ConfirmPlayer(0));
        _confirmPlayerTwo.onClick.RemoveListener(() => ConfirmPlayer(1));
        _confirmPlayerThree.onClick.RemoveListener(() => ConfirmPlayer(2));
        _confirmPlayerFour.onClick.RemoveListener(() => ConfirmPlayer(3));
        
        _closePlayerThree.onClick.RemoveListener(CloseThreePlayer);
        _closePlayerFour.onClick.RemoveListener(CloseFourPlayer);

        _cancelConfirmPlayerOne.onClick.RemoveListener(() => CancelPlayerConfirm(0));
        _cancelConfirmPlayerTwo.onClick.RemoveListener(() => CancelPlayerConfirm(1));
        _cancelConfirmPlayerThree.onClick.RemoveListener(() => CancelPlayerConfirm(2));
        _cancelConfirmPlayerFour.onClick.RemoveListener(() => CancelPlayerConfirm(3));

        _confirmMatch.onClick.RemoveListener(StartMatch);
    }

    private void OpenThreePlayer()
    {
        _playerThreeClosed.SetActive(false);
        _playerThreeOpened.SetActive(true);
        _matchData.UpdateMatchPlayerQuantity(true);
        UpdatePlayButton();
    }

    private void OpenFourPlayer()
    {
        _playerFourClosed.SetActive(false);
        _playerFourOpened.SetActive(true);
        _matchData.UpdateMatchPlayerQuantity(true);
        UpdatePlayButton();
    }

    private void CloseThreePlayer()
    {
        _playerThreeClosed.SetActive(true);
        _playerThreeOpened.SetActive(false);
        _matchData.UpdateMatchPlayerQuantity(false);

        if(_playerThreeConfirmed)
            CancelPlayerConfirm(2);

        UpdatePlayButton();
    }

    private void CloseFourPlayer()
    {
        _playerFourClosed.SetActive(true);
        _playerFourOpened.SetActive(false);
        _matchData.UpdateMatchPlayerQuantity(false);

        if(_playerFourConfirmed)
            CancelPlayerConfirm(3);

        UpdatePlayButton();
    }

    private void ConfirmPlayer(int index)
    {
        _matchData.AddPlayerOnMatch(GetPlayerByIndex(index));
        
        _currentPlayersConfirmed++;
        if(_currentPlayersConfirmed > 4)
            _currentPlayersConfirmed = 4;

        if(index == 2)
            _playerThreeConfirmed = true;

        if(index == 3)
            _playerFourConfirmed = true;

        UpdatePlayButton();
    }

    private void CancelPlayerConfirm(int index)
    {
        _matchData.RemovePlayerOnMatch(GetPlayerByIndex(index));
        
        _currentPlayersConfirmed--;
        if(_currentPlayersConfirmed < 0)
            _currentPlayersConfirmed = 0;

        if(index == 2)
            _playerThreeConfirmed = false;

        if(index == 3)
            _playerFourConfirmed = false;

        UpdatePlayButton();
    }

    private PlayerInfo GetPlayerByIndex(int index)
    {
        switch(index)
        {
            case 0: return _playerOne;
            case 1: return _playerTwo;
            case 2: return _playerThree;
            case 3: return _playerFour;
            default: return null;
        }
    }

    private void UpdatePlayButton()
    {
        bool haveMinimalPlayers = _currentPlayersConfirmed > 1;

        if(_currentPlayersConfirmed == _matchData.MatchPlayerQuantity && haveMinimalPlayers)
            _confirmMatch.interactable = true;
        else
            _confirmMatch.interactable = false;

    }

    private void StartMatch()
    {
        SceneManager.LoadScene("GameplayLocal");
    }

    public void InitLocalGame()
    {
        CloseThreePlayer();
        CloseFourPlayer();
        _matchData.PrepareNewGame();
    }
}
