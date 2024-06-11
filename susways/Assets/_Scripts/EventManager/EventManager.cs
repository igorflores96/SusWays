using System;
using UnityEngine;

public static class EventManager
{
    public static event Action<PlayerBaseState> OnPlayerSpawnRequested;

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
}
