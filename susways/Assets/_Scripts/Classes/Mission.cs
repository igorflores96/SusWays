using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Mission
{

    private string _name;
    private string _text;
    private List<MissionObjective> _objectives;
    private bool _missionComplete;
    private int _currentObjective;
    private Sprite _lockedIcon;
    public string Name => _name;
    public string Text => _text;
    public List<MissionObjective> Objectives => _objectives;
    public int CurrentObjective => _currentObjective;
    public bool MissionComplete => _missionComplete;
    public Sprite LockedIcon => _lockedIcon;


    public Mission(CardMission mission)
    {
        _name = mission.Title;
        _text = mission.Text;
        _lockedIcon = mission.LockedIcon;
        _objectives = new List<MissionObjective>();

        foreach (var objective in mission.Objectives)
        {
            MissionObjective objectiveCopy = new MissionObjective
            {
                Name = objective.Name,
                Text = objective.Text,
                IsComplete = objective.IsComplete,
                NormalIcon = objective.NormalIcon,
                CompletedIcon = objective.CompletedIcon,
                GoalPositions = new List<Vector2>(objective.GoalPositions)
            };

            _objectives.Add(objectiveCopy);
        }

        _currentObjective = 0;
        _missionComplete = false;
    }

    public void UpdateToNextObjective()
    {
        if(!_missionComplete)
            _currentObjective++;

        if(_currentObjective > 3 && !_missionComplete)
        {
            _missionComplete = true;
            _currentObjective = 3;
        }
    }
}
