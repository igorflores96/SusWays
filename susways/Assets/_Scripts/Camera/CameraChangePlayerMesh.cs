using System.Collections.Generic;
using UnityEngine;

public class CameraChangePlayerMesh : MonoBehaviour
{
    [SerializeField] MeshFilter _meshTarget;

    private void OnEnable() 
    {
        EventManager.OnNewPlayerTurn += UpdateMesh;
         EventManager.OnPlayersEndGame += Hide; 
    }

    private void OnDisable() 
    {
        EventManager.OnNewPlayerTurn -= UpdateMesh; 
         EventManager.OnPlayersEndGame -= Hide; 

    }

    private void UpdateMesh(Mission playerMission, PlayerBaseState playerInfos)
    {
        MeshFilter meshFilter = playerInfos.GetInstantiatePrefab().GetComponentInChildren<MeshFilter>();
        MeshRenderer meshRenderer = playerInfos.GetVisualPrefab().GetComponentInChildren<MeshRenderer>();

        if (meshFilter != null)
        {
            _meshTarget.mesh = meshFilter.mesh;
        }
        else
        {
            Debug.LogWarning("Nenhum MeshFilter foi encontrado no primeiro filho do Prefab.");
        }

        if (meshRenderer != null)
        {
            MeshRenderer targetRenderer = _meshTarget.GetComponent<MeshRenderer>();
            
            if (targetRenderer != null)
            {
                targetRenderer.material.color = playerInfos.GetColor();
            }
            else
            {
                Debug.LogWarning("Nenhum MeshRenderer foi encontrado no MeshFilter alvo.");
            }
        }
        else
        {
            Debug.LogWarning("Nenhum MeshRenderer foi encontrado no primeiro filho do Prefab.");
        }
    }

    private void Hide(List<PlayerBaseState> playerList)
    {
        gameObject.SetActive(false);
    }
}
