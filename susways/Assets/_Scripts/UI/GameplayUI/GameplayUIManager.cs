using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{

    [Header("Buttons")]
    [SerializeField] private Button _nextTunButton;
    [SerializeField] private Button _showMissionButton;
    [SerializeField] private Button _confirmObjectiveCompleteButton;

    [Header("Mission Card")]
    [SerializeField] private TextMeshProUGUI _missionTitle;
    [SerializeField] private TextMeshProUGUI _missionText;
    [SerializeField] private List<Image> _objectives; //We goin to populate on the inspector

    [Header("Card Mission Animator")]
    [SerializeField] private Animator _cardAnimator;

    private bool shouldOpenCanvas;

    private void OnEnable() 
    {
        shouldOpenCanvas = true;
        _nextTunButton.onClick.AddListener(UpdateNextTurn);
        _showMissionButton.onClick.AddListener(CheckMissionCanvas);
        _confirmObjectiveCompleteButton.onClick.AddListener(ConfirmObjective);
        
        EventManager.OnNewPlayerTurn += UpdateInfos;
        EventManager.OnPlayerCompleteObjective += ShowObjectives;
    }

    private void OnDisable() 
    {
        _nextTunButton.onClick.RemoveListener(UpdateNextTurn);
        _showMissionButton.onClick.RemoveListener(CheckMissionCanvas);
        _confirmObjectiveCompleteButton.onClick.RemoveListener(ConfirmObjective);
       
        EventManager.OnNewPlayerTurn -= UpdateInfos;
        EventManager.OnPlayerCompleteObjective -= ShowObjectives;
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

    private void UpdateInfos(Mission playerMission)
    {
        _missionTitle.text = playerMission.Name;
        _missionText.text = playerMission.Text;

        _missionText.alignment = TextAlignmentOptions.Justified;

        for(int i = 0; i < playerMission.Objectives.Count; i++)
        {
            if(playerMission.Objectives[i].IsComplete)
                _objectives[i].sprite = playerMission.Objectives[i].CompletedIcon;
            else
                _objectives[i].sprite = playerMission.Objectives[i].NormalIcon;
        }

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
}
