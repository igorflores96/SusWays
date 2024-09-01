using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static event Action<PlayerBaseState> OnPlayerSpawnRequested;
    public static event Action<PlayerBaseState> OnPlayerSelected;
    public static event Action<Mission, PlayerBaseState> OnNewPlayerTurn;
    public static event Action<Mission> OnPlayerCompleteObjective;
    public static event Action<List<Vector3Int>> OnListReady;
    public static event Action<List<PlayerBaseState>> OnPlayersEndGame;
    public static event Action OnEndTurn;
    public static event Action OnCofirmObjective;
    public static event Action OnAnimation;
    public static event Action OnAnimationOff;
    public static event Action OnBusJump;
    public static event Action OnBusProcessEnd;

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

    public static void NewPlayerTurn(Mission playerMission, PlayerBaseState playerInfos) //Fired for GameStateManager, listened for GameplayUIManager to update UI infos and CameraPlayer to change the mesh. 
    {
        if (OnNewPlayerTurn != null)
        {
            OnNewPlayerTurn(playerMission, playerInfos);
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

    public static void TheGameIsEnd(List<PlayerBaseState> list) //Fired for GameStateManager, listened for Gameplay UI for show the player's ranking. 
    {
        if (OnPlayersEndGame != null)
        {
            OnPlayersEndGame(list);
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayersEndGame event.");
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

    public static void AnimationOn() //Fired by any animation manager to block other scripts usages
    {
        if (OnAnimation != null)
        {
            OnAnimation();
        }
        else
        {
            Debug.LogWarning("No listeners for OnAnimation event.");
        }
    }
    
    public static void AnimationOff() //Fired by any animation manager to block other scripts usages
    {
        if (OnAnimationOff != null)
        {
            OnAnimationOff();
        }
        else
        {
            Debug.LogWarning("No listeners for OnAnimationOff event.");
        }
    }

    public static void BusJumpEnd() //Fired by bus for gameplay UI show de confirmation for players.
    {
        if (OnBusJump != null)
        {
            OnBusJump();
        }
        else
        {
            Debug.LogWarning("No listeners for OnBusJump event.");
        }
    }

    public static void StateShouldBeUpdated() //Fired by  gameplay UI for game state manager update game turn.
    {
        if (OnBusProcessEnd != null)
        {
            OnBusProcessEnd();
        }
        else
        {
            Debug.LogWarning("No listeners for OnBusProcessEnd event.");
        }
    }
}
