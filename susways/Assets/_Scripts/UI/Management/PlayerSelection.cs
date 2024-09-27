using System.Collections.Generic;
using TMPro;
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

    [Header(" - Cancel Confirm Buttons")]
    [SerializeField] private Button _cancelConfirmPlayerOne;
    [SerializeField] private Button _cancelConfirmPlayerTwo;
    [SerializeField] private Button _cancelConfirmPlayerThree;
    [SerializeField] private Button _cancelConfirmPlayerFour;

    [Header(" - Player Meeples Changers Buttons")]
    [SerializeField] private Button _returnMeshOneButton;
    [SerializeField] private Button _nextMeshOneButton;
    [SerializeField] private Button _returnMeshTwoButton;
    [SerializeField] private Button _nextMeshTwoButton;
    [SerializeField] private Button _returnMeshThreeButton;
    [SerializeField] private Button _nextMeshThreeButton;
    [SerializeField] private Button _returnMeshFourButton;
    [SerializeField] private Button _nextMeshFourButton;

    [Header(" - Player Color Changers Buttons")]
    [SerializeField] private Button _returnColorOneButton;
    [SerializeField] private Button _nextColorOneButton;
    [SerializeField] private Button _returnColorTwoButton;
    [SerializeField] private Button _nextColorTwoButton;
    [SerializeField] private Button _returnColorThreeButton;
    [SerializeField] private Button _nextColorThreeButton;
    [SerializeField] private Button _returnColorFourButton;
    [SerializeField] private Button _nextColorFourButton;
    [SerializeField] private Image _feedbackOneColor;
    [SerializeField] private Image _feedbackTwoColor;
    [SerializeField] private Image _feedbackThreeColor;
    [SerializeField] private Image _feedbackFourColor;


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

    [Header("Meeple Meshs and Colors")]
    [SerializeField] private List<Mesh> _meeplesMesh; //populate in inspector
    [SerializeField] private List<Color> _colors; //populate in inspector

    [Header("Sound Effects")]
    [SerializeField] AudioSource _buttonAudio;
    [SerializeField] AudioSource _backButtonAudio;

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


        _nextMeshOneButton.onClick.AddListener(() => ChangePlayerMeeple(_playerOne, true));
        _nextMeshTwoButton.onClick.AddListener(() => ChangePlayerMeeple(_playerTwo, true));
        _nextMeshThreeButton.onClick.AddListener(() => ChangePlayerMeeple(_playerThree, true));
        _nextMeshFourButton.onClick.AddListener(() => ChangePlayerMeeple(_playerFour, true));

        _returnMeshOneButton.onClick.AddListener(() => ChangePlayerMeeple(_playerOne, false));
        _returnMeshTwoButton.onClick.AddListener(() => ChangePlayerMeeple(_playerTwo, false));
        _returnMeshThreeButton.onClick.AddListener(() => ChangePlayerMeeple(_playerThree, false));
        _returnMeshFourButton.onClick.AddListener(() => ChangePlayerMeeple(_playerFour, false));

        _nextColorOneButton.onClick.AddListener(() => ChangePlayerColor(_playerOne, true));
        _nextColorTwoButton.onClick.AddListener(() => ChangePlayerColor(_playerTwo, true));
        _nextColorThreeButton.onClick.AddListener(() => ChangePlayerColor(_playerThree, true));
        _nextColorFourButton.onClick.AddListener(() => ChangePlayerColor(_playerFour, true));

        _returnColorOneButton.onClick.AddListener(() => ChangePlayerColor(_playerOne, false));
        _returnColorTwoButton.onClick.AddListener(() => ChangePlayerColor(_playerTwo, false));
        _returnColorThreeButton.onClick.AddListener(() => ChangePlayerColor(_playerThree, false));
        _returnColorFourButton.onClick.AddListener(() => ChangePlayerColor(_playerFour, false));

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

        _nextMeshOneButton.onClick.RemoveListener(() => ChangePlayerMeeple(_playerOne, true));
        _nextMeshTwoButton.onClick.RemoveListener(() => ChangePlayerMeeple(_playerTwo, true));
        _nextMeshThreeButton.onClick.RemoveListener(() => ChangePlayerMeeple(_playerThree, true));
        _nextMeshFourButton.onClick.RemoveListener(() => ChangePlayerMeeple(_playerFour, true));

        _returnMeshOneButton.onClick.RemoveListener(() => ChangePlayerMeeple(_playerOne, false));
        _returnMeshTwoButton.onClick.RemoveListener(() => ChangePlayerMeeple(_playerTwo, false));
        _returnMeshThreeButton.onClick.RemoveListener(() => ChangePlayerMeeple(_playerThree, false));
        _returnMeshFourButton.onClick.RemoveListener(() => ChangePlayerMeeple(_playerFour, false));

        _nextColorOneButton.onClick.RemoveListener(() => ChangePlayerColor(_playerOne, true));
        _nextColorTwoButton.onClick.RemoveListener(() => ChangePlayerColor(_playerTwo, true));
        _nextColorThreeButton.onClick.RemoveListener(() => ChangePlayerColor(_playerThree, true));
        _nextColorFourButton.onClick.RemoveListener(() => ChangePlayerColor(_playerFour, true));

        _returnColorOneButton.onClick.RemoveListener(() => ChangePlayerColor(_playerOne, false));
        _returnColorTwoButton.onClick.RemoveListener(() => ChangePlayerColor(_playerTwo, false));
        _returnColorThreeButton.onClick.RemoveListener(() => ChangePlayerColor(_playerThree, false));
        _returnColorFourButton.onClick.RemoveListener(() => ChangePlayerColor(_playerFour, false));

        _confirmMatch.onClick.RemoveListener(StartMatch);
    }

    private void OpenThreePlayer()
    {
        _buttonAudio.Play();
        _playerThreeClosed.SetActive(false);
        _playerThreeOpened.SetActive(true);
        _matchData.UpdateMatchPlayerQuantity(true);
        UpdatePlayButton();
    }

    private void OpenFourPlayer()
    {
        _buttonAudio.Play();
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
        _buttonAudio.Play();
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
        _backButtonAudio.Play();
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
        _buttonAudio.Play();
        SceneManager.LoadScene("GameplayLocal");
    }

    private void ChangePlayerMeeple(PlayerInfo player, bool isAvanceChange)
    {
        _buttonAudio.Play();
        int currentIndex = player.CurrentMeshIndex;

        if(isAvanceChange)
            currentIndex = (currentIndex + 1) % _meeplesMesh.Count;
        else
            currentIndex = (currentIndex - 1 + _meeplesMesh.Count) % _meeplesMesh.Count;

        player.SetPlayerMesh(_meeplesMesh[currentIndex]);

        player.CurrentMeshIndex = currentIndex;
    }

    private void ChangePlayerColor(PlayerInfo player, bool isAvanceChange)
    {
        _buttonAudio.Play();
        int currentIndex = player.CurrentColorIndex;

        if(isAvanceChange)
            currentIndex = (currentIndex + 1) % _colors.Count;
        else
            currentIndex = (currentIndex - 1 + _colors.Count) % _colors.Count;

        player.SetPlayerColor(_colors[currentIndex]);

        ChangeFeedbackColor(player);

        player.CurrentColorIndex = currentIndex;
    }

    public void InitLocalGame()
    {
        CloseThreePlayer();
        CloseFourPlayer();
        _confirmMatch.interactable = false;
        _currentPlayersConfirmed = 0;
        _matchData.PrepareNewGame();
    }

    private void ChangeFeedbackColor(PlayerInfo player)
    {
        if(player == _playerOne)
            _feedbackOneColor.color = player.Color;

        if(player == _playerTwo)
            _feedbackTwoColor.color = player.Color;

        if(player == _playerThree)
            _feedbackThreeColor.color = player.Color;

        if(player == _playerFour)
            _feedbackFourColor.color = player.Color;
    }

}
