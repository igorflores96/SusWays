using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public CustomGrid<Tile> GameMap;

    [Header("Map Infos")]
    [SerializeField] private MapData _mapData;
    [SerializeField] private int _width;
    [SerializeField] private int _height; 
    [SerializeField] private float _cellSize; 
    [SerializeField] private TileFeedback[] testefloors;
    
    private List<Vector3Int> _listToMove = new List<Vector3Int>();
    private Tile _lastTile;
    private TileFeedback _lastFeedback;
    private bool _shouldShowFeedback = true;
    private bool _shouldInteract = true;
   
    private void Awake() 
    {
        GameMap = new CustomGrid<Tile>(_width, _height, _cellSize, Vector3.zero, (CustomGrid<Tile> g, int x, int z) => new Tile(x, z));
        _lastTile = null;
        _mapData.InitDicionary(_width, _height);
        GenerateMap();
    }

    private void OnEnable() 
    {
        EventManager.OnPlayerSpawnRequested += SpawnPlayer;
        EventManager.OnPlayerSelected += SetTilesToMoveFeedback;
        EventManager.OnEndTurn += UpdateFeedbacks;
        EventManager.OnAnimation += BlockMapAction;
        EventManager.OnAnimationOff += UnlockMapAction;


    }

    private void OnDisable() 
    {
        EventManager.OnPlayerSpawnRequested -= SpawnPlayer;
        EventManager.OnPlayerSelected -= SetTilesToMoveFeedback;
        EventManager.OnEndTurn -= UpdateFeedbacks;
        EventManager.OnAnimation -= BlockMapAction;
        EventManager.OnAnimationOff -= UnlockMapAction;

    }

    private void Update() 
    {
        if(_shouldInteract)
            UpdateTileHoverFeedBack();
    }

    private void GenerateMap()
    {
        for(int xSize = 0; xSize < _width; xSize++)
        {
            for(int zSize = 0; zSize < _height; zSize++)
            {
                Vector3 spawnPosition = GameMap.GetWorldPosition(xSize, zSize);
                GameObject objectPrefab = Instantiate(testefloors[0].gameObject, spawnPosition, Quaternion.identity);
                objectPrefab.transform.SetParent(this.transform);
                objectPrefab.name = "Hex: " + xSize + "." + zSize;
                objectPrefab.TryGetComponent<TileFeedback>(out TileFeedback feedback);
                
                Tile tempTile = GameMap.GetGridObject(xSize, zSize);
                tempTile.SetWalkableStatus(_mapData.GetWalkableStatus(new Vector2Int(xSize, zSize)));
                tempTile.SetTrasnformTileFeedback(feedback);
                tempTile.HideAllFeedbacks();
                
            }
        }
    }

    private void UpdateTileHoverFeedBack()
    {

        if(_lastTile != null)
        {
            _lastTile.Hide();
        }

        if(_lastFeedback != null)
        {
            _lastFeedback.HideFeedback();
        }

        Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
        _lastTile = GameMap.GetGridObject(mousePosition);
        _lastFeedback = Mouse3D.GetFeedback();
        
        if(_lastTile != null && _lastTile.IsWalkable)
        {
            _lastTile.Show();
        }

        if(_lastFeedback != null && _shouldShowFeedback)
        {
            _lastFeedback.ShowFeedback();
        }
    }

    private void SetTilesToMoveFeedback(PlayerBaseState player)
    {
        _shouldShowFeedback = false;

        Vector2Int playerPosition = player.GetPosition();
        int playerDiceNumber = player.GetDiceNumber();
        _listToMove = GameMap.GetPlayerMoveOptions(playerPosition.x, playerPosition.y, playerDiceNumber, GameMap);

        foreach(Vector3Int tilePosition in _listToMove)
        {
            Tile tile = GameMap.GetGridObject(tilePosition.x, tilePosition.z);
            
            if(tile.IsWalkable)
                tile.ShowWalkFeedback();
        }

        EventManager.ListReady(_listToMove);
    }



    private void SpawnPlayer(PlayerBaseState player)
    {
        Vector2Int playerPosition = player.GetPosition();
        Vector3 spawnPosition = GameMap.GetWorldPosition(playerPosition.x, playerPosition.y);
        GameObject playerPrefab = Instantiate(player.GetVisualPrefab(), spawnPosition, Quaternion.identity);
        playerPrefab.transform.SetParent(this.transform);
        playerPrefab.name = player.GetType().Name;
        player.SetInstantiatePrefab(playerPrefab);
    }

    private void UpdateFeedbacks() //we gonna use this on the end of turn
    {
        foreach(Vector3Int tilePosition in _listToMove)
        {
            Tile tile = GameMap.GetGridObject(tilePosition.x, tilePosition.z);
            tile.HideWalkFeedback();
        }

        _shouldShowFeedback = true;
        _listToMove.Clear();
    }

    private void BlockMapAction()
    {
        _shouldInteract = false;
    }

    private void UnlockMapAction()
    {
        _shouldInteract = true;
    }
}
