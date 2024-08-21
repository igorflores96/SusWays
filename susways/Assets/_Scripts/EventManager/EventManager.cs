using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action<PlayerBaseState> OnPlayerSpawnRequested;

    public static event Action<PlayerBaseState> OnPlayerSelected;

    public static event Action<List<Vector3Int>> OnListReady;


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

}
