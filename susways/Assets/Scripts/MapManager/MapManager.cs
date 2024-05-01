using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public CustomGrid<Tile> GameMap;

    [Header("Map Infos")]
    [SerializeField] private int _width;
    [SerializeField] private int _height; 
    [SerializeField] private float _cellSize; 

    public GameObject[] testefloors;
    private Tile _lastTile;

    private void Start() 
    {
        GameMap = new CustomGrid<Tile>(_width, _height, _cellSize, Vector3.zero, (CustomGrid<Tile> g, int x, int z) => new Tile(x, z));
        _lastTile = null;
        GenerateMap();
    }

    private void Update() {
        UpdateTileHoverFeedBack();
    }

    private void GenerateMap()
    {
        for(int xSize = 0; xSize < _width; xSize++)
        {
            for(int zSize = 0; zSize < _height; zSize++)
            {
                int randomfloor = Random.Range(0, 2);
                Vector3 spawnPosition = GameMap.GetWorldPosition(xSize, zSize);
                GameObject objectPrefab = Instantiate(testefloors[randomfloor], spawnPosition, Quaternion.identity);
                objectPrefab.transform.SetParent(this.transform);
                objectPrefab.name = "Hex: " + xSize + "." + zSize;

                Tile tempTile = GameMap.GetGridObject(xSize, zSize);
                tempTile.SetTrasnformTileFeedback(objectPrefab.transform);
                tempTile.SetFloorTile(objectPrefab);
                tempTile.Hide();
            }
        }
    }

    private void UpdateTileHoverFeedBack()
    {

        if(_lastTile != null)
        {
            _lastTile.Hide();
        }

        Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
        Debug.Log(mousePosition);
        
        _lastTile = GameMap.GetGridObject(mousePosition);
        
        if(_lastTile != null)
        {
            _lastTile.Show();
        }

    }   
}
