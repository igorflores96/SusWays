using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{

    [Header("Buttons")]
    [SerializeField] private Button _nextTunButton;
    [SerializeField] private Button _showMissionButton;
    [SerializeField] private Button _confirmObjectiveCompleteButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private Button _confirmBus;
    [SerializeField] private Button _confirmEnterBusStop;
    [SerializeField] private Button _cancelEnterBusStop;


    [Header("Mission Card")]
    [SerializeField] private TextMeshProUGUI _missionTitle;
    [SerializeField] private TextMeshProUGUI _missionText;
    [SerializeField] private List<Image> _objectives; //We goin to populate on the inspector
    
    [Header("Current Player Infos")]
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private List<Image> _missionObjectives; //We goin to populate on the inspector
    [SerializeField] private TextMeshProUGUI _playerNewTurnName;
    [SerializeField] private TextMeshProUGUI _playerDiceNumber;

    

    [Header("End Game Screen")]
    [SerializeField] private GameObject _endGameCanvas;
    [SerializeField] private GameObject _gameplayUI;
    [SerializeField] private TextMeshProUGUI[] _playerNames;
    [SerializeField] private GameObject[] _playerRankingObjects;


    [Header("Card Mission Animator")]
    [SerializeField] private Animator _cardAnimator;
    [SerializeField] private Animator _rankingAnimator;
    [SerializeField] private Animator _turnAnimator;


    private bool shouldOpenCanvas;

    private void OnEnable() 
    {
        shouldOpenCanvas = true;
        _nextTunButton.onClick.AddListener(UpdateNextTurn);
        _showMissionButton.onClick.AddListener(CheckMissionCanvas);
        _confirmObjectiveCompleteButton.onClick.AddListener(ConfirmObjective);
        _backToMenuButton.onClick.AddListener(GoToMenu);
        _confirmBus.onClick.AddListener(HideBus);
        _confirmEnterBusStop.onClick.AddListener(ConfirmBusEnter);
        _cancelEnterBusStop.onClick.AddListener(CancelEnterBus);

        
        EventManager.OnNewPlayerTurn += UpdateInfos;
        EventManager.OnPlayersEndGame += ShowEndGameScreen;
        EventManager.OnPlayerCompleteObjective += ShowObjectives;
        EventManager.OnBusJump += ShowBusFeedback;
        EventManager.OnPlayerTryEnterBusStop += PlayerTryEnterBusStop;
        EventManager.OnHideUI += HideUI;
        EventManager.OnShowUI += ShowUI;
    }

    private void OnDisable() 
    {
        _nextTunButton.onClick.RemoveListener(UpdateNextTurn);
        _showMissionButton.onClick.RemoveListener(CheckMissionCanvas);
        _confirmObjectiveCompleteButton.onClick.RemoveListener(ConfirmObjective);
        _backToMenuButton.onClick.RemoveListener(GoToMenu);
        _confirmBus.onClick.RemoveListener(HideBus);
        _confirmEnterBusStop.onClick.RemoveListener(ConfirmBusEnter);
        _cancelEnterBusStop.onClick.RemoveListener(CancelEnterBus);



       
        EventManager.OnNewPlayerTurn -= UpdateInfos;
        EventManager.OnPlayersEndGame -= ShowEndGameScreen;
        EventManager.OnPlayerCompleteObjective -= ShowObjectives;
        EventManager.OnBusJump -= ShowBusFeedback;
        EventManager.OnPlayerTryEnterBusStop -= PlayerTryEnterBusStop;
        EventManager.OnHideUI -= HideUI;
        EventManager.OnShowUI -= ShowUI;

    }

    private void UpdateNextTurn()
    {
        EventManager.EndTurn();
    }

    private void CheckMissionCanvas()
    {
        _nextTunButton.gameObject.SetActive(!shouldOpenCanvas);

        if(shouldOpenCanvas)
            _cardAnimator.SetTrigger("Open");
        else
            _cardAnimator.SetTrigger("Close");

        shouldOpenCanvas = !shouldOpenCanvas;
    }

    private void UpdateInfos(Mission playerMission, PlayerBaseState playerInfo)
    {
        _turnAnimator.SetTrigger("Show");
        EventManager.AnimationOn();
        UpdateMissionInfos(playerMission);
        UpdatePlayerInfos(playerInfo);
    }

    private void UpdateMissionInfos(Mission playerMission)
    {
        _missionTitle.text = playerMission.Name;
        _missionText.text = playerMission.Text;

        _missionText.alignment = TextAlignmentOptions.Justified;

        for(int i = 0; i < playerMission.Objectives.Count; i++)
        {
            if(playerMission.Objectives[i].IsComplete)
            {
                _objectives[i].sprite = playerMission.Objectives[i].CompletedIcon;
                _missionObjectives[i].sprite = playerMission.Objectives[i].CompletedIcon;
            }
            else
            {
                _objectives[i].sprite = playerMission.Objectives[i].NormalIcon;
                _missionObjectives[i].sprite = playerMission.Objectives[i].NormalIcon;
            }
        }
    }

    private void UpdatePlayerInfos(PlayerBaseState playerInfo)
    {
        string playerName = _playerName.text = playerInfo.GetPlayerName();

        _playerName.text = playerName;
        _playerNewTurnName.text = $"Vez de {playerName}";
        _playerDiceNumber.text = $"Dado sorteado: {playerInfo.GetDiceNumber()}";
        HideUI();
    }

    private void ShowObjectives(Mission playerMission)
    {
        _nextTunButton.gameObject.SetActive(false);
        _showMissionButton.gameObject.SetActive(false);

        CheckTextToShow(playerMission);

        for(int i = 0; i < playerMission.Objectives.Count; i++)
        {
            if(playerMission.Objectives[i].IsComplete)
                _objectives[i].sprite = playerMission.Objectives[i].CompletedIcon;
            else
                _objectives[i].sprite = playerMission.Objectives[i].NormalIcon;
        }

        _confirmObjectiveCompleteButton.gameObject.SetActive(true);
        _cardAnimator.SetTrigger("Open");
    }

    private void ConfirmObjective()
    {
        _cardAnimator.SetTrigger("Close");
        _nextTunButton.gameObject.SetActive(true);
        _showMissionButton.gameObject.SetActive(true);
        _confirmObjectiveCompleteButton.gameObject.SetActive(false);

        EventManager.ConfirmObjective();
    }

    private void CheckTextToShow(Mission mission)
    {
        string playerObjectiveName = mission.Objectives[mission.CurrentObjective - 1].Name;

        if(!mission.MissionComplete)
        {  
            string playerNextObjective = mission.Objectives[mission.CurrentObjective].Name;
            _missionTitle.text = "Objetivo Completo!";
            _missionText.text = $"Você chegou em {playerObjectiveName}.\nSeus Selos foram atualizados.\nSeu próximo objetivo é: {playerNextObjective}";
            _missionText.alignment = TextAlignmentOptions.Center;
        }
        else
        {
            _missionTitle.text = "Todos os Objetivos foram Completos!";
            _missionText.text = $"Parabéns!\nVocê completou os Caminhos do SUS.";
            _missionText.alignment = TextAlignmentOptions.Center;
        }

    }

    private void ShowEndGameScreen(List<PlayerBaseState> playerList)
    {
        int players = playerList.Count;

        for (int i = players; i < _playerRankingObjects.Length; i++)
        {
            _playerRankingObjects[i].SetActive(false);
        }

        for (int i = 0; i < players; i++)
        {
            _playerRankingObjects[i].SetActive(true);
            _playerNames[i].text = playerList[i].GetPlayerName();
        }

        _rankingAnimator.SetTrigger("Show");
        _gameplayUI.SetActive(false);
        EventManager.AnimationOn();
    }

    private void ShowBusFeedback()
    {
        _turnAnimator.SetTrigger("Bus");
        EventManager.AnimationOn();
    }

    private void HideBus()
    {
        _turnAnimator.SetTrigger("HideBus");
        EventManager.AnimationOff();
        EventManager.StateShouldBeUpdated();
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void AnimationEnd()
    {
        EventManager.AnimationOff();
        ShowUI();
    }

    private void PlayerTryEnterBusStop()
    {
        _turnAnimator.SetTrigger("EnterBus");
    }

    private void ConfirmBusEnter()
    {
        _turnAnimator.SetTrigger("CancelEnterBus");
        EventManager.PlayerEnterBus();
    }

    private void CancelEnterBus()
    {
        _turnAnimator.SetTrigger("CancelEnterBus");
        EventManager.PlayerCancelEnterBus();
    }

    private void HideUI()
    {
        _nextTunButton.interactable = false;
        _showMissionButton.interactable = false;
    }

    private void ShowUI()
    {
        _nextTunButton.interactable = true;
        _showMissionButton.interactable = true;
    }
}
