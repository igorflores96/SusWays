using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action<PlayerBaseState> OnPlayerSpawnRequested;
    public static event Action<PlayerBaseState> OnPlayerSelected;
    public static event Action<Mission> OnNewPlayerTurn;
    public static event Action<Mission> OnPlayerCompleteObjective;
    public static event Action<List<Vector3Int>> OnListReady;
    public static event Action OnEndTurn;
    public static event Action OnCofirmObjective;



    public static void RequestPlayerSpawn(PlayerBaseState playerState) //Disparado por GameStateManager, ouvido por MapManager
    {
        if (OnPlayerSpawnRequested != null)
        {
            OnPlayerSpawnRequested(playerState);
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayerSpawnRequested event.");
        }
    }

    public static void PlayerSelected(PlayerBaseState playerState) //Fired for GameStateManager, listened for MapManager to show tile feedback to go. 
    {
        if (OnPlayerSelected != null)
        {
            OnPlayerSelected(playerState);
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayerSelected event.");
        }
    }

    public static void NewPlayerTurn(Mission playerMission) //Fired for GameStateManager, listened for GameplayUIManager to update UI infos. 
    {
        if (OnNewPlayerTurn != null)
        {
            OnNewPlayerTurn(playerMission);
        }
        else
        {
            Debug.LogWarning("No listeners for OnNewPlayerTurn event.");
        }
    }

    public static void PlayerCompleteObjective(Mission playerMission) //Fired for GameStateManager, listened for GameplayUIManager to update UI infos. 
    {
        if (OnPlayerCompleteObjective != null)
        {
            OnPlayerCompleteObjective(playerMission);
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayerCompleteObjective event.");
        }
    }

    public static void ListReady(List<Vector3Int> list) //Fired for GameStateManager, listened for MapManager for try to move the player. 
    {
        if (OnListReady != null)
        {
            OnListReady(list);
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayerTryMove event.");
        }
    }

    public static void EndTurn() //Fired by GameplayUIManager, listened for MapManager to clen the selection tiles and for GameStateManager to update de turn of the game.
    {
        if (OnEndTurn != null)
        {
            OnEndTurn();
        }
        else
        {
            Debug.LogWarning("No listeners for OnEndTurn event.");
        }
    }

    public static void ConfirmObjective() //Fired by GameplayUIManager, listened for GameStateManager to update de turn of the game.
    {
        if (OnCofirmObjective != null)
        {
            OnCofirmObjective();
        }
        else
        {
            Debug.LogWarning("No listeners for OnCofirmObjective event.");
        }
    }

}
