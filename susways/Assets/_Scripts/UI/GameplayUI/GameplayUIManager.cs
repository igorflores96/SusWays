using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{

    [Header("Buttons")]
    [SerializeField] private Button _nextTunButton;

    private void OnEnable() 
    {
        _nextTunButton.onClick.AddListener(UpdateNextTurn);
    }

    private void OnDisable() 
    {
        _nextTunButton.onClick.RemoveListener(UpdateNextTurn);
    }

    private void UpdateNextTurn()
    {
        EventManager.EndTurn();
    }

}
