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
    public static event Action<bool> OnPlayerAnswerChallange;
    public static event Action<BuildingInfo> OnBuildingClicked;
    public static event Action OnEndTurn;
    public static event Action OnCofirmObjective;
    public static event Action OnAnimation;
    public static event Action OnAnimationOff;
    public static event Action OnBusJump;
    public static event Action OnBusProcessEnd;
    public static event Action OnPlayerTryEnterBusStop;
    public static event Action OnPlayerEnterBus;
    public static event Action OnPlayerCancelEnterBus;
    public static event Action OnPlayerMoveDone;
    public static event Action OnHideUI;
    public static event Action OnShowUI;
    public static event Action OnDiceEnd;
    public static event Action OnPlayerHaveBonus;
    public static event Action OnPlayerEnableRollDice;







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

    public static void PlayerTryEnterOnBusStop() //Fired by game state manager to UI manager show the confirmation panel.
    {
        if (OnPlayerTryEnterBusStop != null)
        {
            OnPlayerTryEnterBusStop();
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayerTryEnterBusStop event.");
        }
    }

    public static void PlayerEnterBus() //Fired by UI manager to game state manager move the player
    {
        if (OnPlayerEnterBus != null)
        {
            OnPlayerEnterBus();
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayerEnterBus event.");
        }
    }

    public static void PlayerCancelEnterBus() //Fired by UI manager to game state manager not move the player
    {
        if (OnPlayerCancelEnterBus != null)
        {
            OnPlayerCancelEnterBus();
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayerCancelEnterBus event.");
        }
    }

    public static void PlayerMoveDone() //Fired by Player Pawn to Game State Manager Update Position
    {
        if (OnPlayerMoveDone != null)
        {
            OnPlayerMoveDone();
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayerMoveDone event.");
        }
    }

    public static void PlayersAnswerChallange(bool value) //Fired by ChallangeManager to GameStateManager add plus one in players movement.
    {
        if (OnPlayerAnswerChallange != null)
        {
            OnPlayerAnswerChallange(value);
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayerChallangeCorrectAnswer event.");
        }
    }

    public static void BuildingClicked(BuildingInfo value) //Fired by ChallangeManager to GameStateManager add plus one in players movement.
    {
        if (OnBuildingClicked != null)
        {
            OnBuildingClicked(value);
        }
        else
        {
            Debug.LogWarning("No listeners for OnBuildingClicked event.");
        }
    }

    public static void ShouldShowUI() //Use to show importante UI
    {
        if (OnShowUI != null)
        {
            OnShowUI();
        }
        else
        {
            Debug.LogWarning("No listeners for OnShowUI event.");
        }
    }

    public static void ShouldHideUI() //Use to hide importante UI
    {
        if (OnHideUI != null)
        {
            OnHideUI();
        }
        else
        {
            Debug.LogWarning("No listeners for OnHideUI event.");
        }
    }

    public static void DiceEnd() //Use to hide importante UI
    {
        if (OnDiceEnd != null)
        {
            OnDiceEnd();
        }
        else
        {
            Debug.LogWarning("No listeners for OnDiceEnd event.");
        }
    }

    public static void PlayerHasBonus() //Players to dice active the feedback
    {
        if (OnPlayerHaveBonus != null)
        {
            OnPlayerHaveBonus();
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayerHaveBonus event.");
        }
    }

    public static void PlayerCanRollDice() //Players to dice active the feedback
    {
        if (OnPlayerEnableRollDice != null)
        {
            OnPlayerEnableRollDice();
        }
        else
        {
            Debug.LogWarning("No listeners for OnPlayerEnableRollDice event.");
        }
    }
}
