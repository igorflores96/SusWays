using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    [SerializeField] private List<BusStop> _stops;
    [SerializeField] private Transform _busMesh; 
    [SerializeField] private Animator _animator;

    private List<PlayerBaseState> _playersOnTheBus = new List<PlayerBaseState>();
    private int _currentStop = 0;

    public void GoToNextStop(List<PlayerBaseState> playersInGame)
    {
        for(int i = 0; i < playersInGame.Count; i++)
        {
            Vector2Int playerPosition = playersInGame[i].GetPosition();
            PlayerBaseState player = playersInGame[i];

            if(_stops[_currentStop].StopPositions.Contains(playerPosition) && !_playersOnTheBus.Contains(player))
                _playersOnTheBus.Add(player);
        }

        _currentStop = (_currentStop + 1) % _stops.Count;
        _animator.SetTrigger("Jump");
    }

    public void ChangePosition()
    {
        transform.position = new Vector3(_stops[_currentStop].StopPositions[1].x * 2, transform.position.y, _stops[_currentStop].StopPositions[1].y * 2);

        switch (_currentStop)
        {
            case 3:
                _busMesh.rotation = Quaternion.Euler(0, 90, 0);
                break;
            case 2:
                _busMesh.rotation = Quaternion.Euler(0, 180, 0);
                break;
            default:
                _busMesh.rotation = Quaternion.Euler(0, 0, 0);
                break;
        }
    }

    public void JumpIsOver()
    {
        EventManager.BusJumpEnd();
    }

    public List<Vector2Int> BusPositions => _stops[_currentStop].StopPositions;
}
