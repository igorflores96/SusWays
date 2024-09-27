using UnityEngine;

public class GameplaySoundManager : MonoBehaviour
{
    [Header("Bus")]
    [SerializeField] private AudioSource _busQuestion;
    
    private void OnEnable() 
    {
        EventManager.OnPlayerTryEnterBusStop += PlayBusQuestion;    
    }

    private void OnDisable() 
    {
        EventManager.OnPlayerTryEnterBusStop -= PlayBusQuestion;    
    }

    private void PlayBusQuestion()
    {
        _busQuestion.Play();
    }
}
