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
    private Tile _lastTile;
    private TileFeedback _lastFeedback;

    private void Awake() 
    {
        GameMap = new CustomGrid<Tile>(_width, _height, _cellSize, Vector3.zero, (CustomGrid<Tile> g, int x, int z) => new Tile(x, z));
        _lastTile = null;
        _lastFeedback = null;
        _mapData.InitDicionary(_width, _height);
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
                Vector3 spawnPosition = GameMap.GetWorldPosition(xSize, zSize);
                GameObject objectPrefab = Instantiate(testefloors[0].gameObject, spawnPosition, Quaternion.identity);
                objectPrefab.transform.SetParent(this.transform);
                objectPrefab.name = "Hex: " + xSize + "." + zSize;
                objectPrefab.TryGetComponent<TileFeedback>(out TileFeedback feedback);
                
                Tile tempTile = GameMap.GetGridObject(xSize, zSize);
                tempTile.SetWalkableStatus(_mapData.GetWalkableStatus(new Vector2Int(xSize, zSize)));
                tempTile.SetTrasnformTileFeedback(feedback);
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

        if(_lastFeedback != null)
        {
            _lastFeedback.ShowFeedback();
        }
    }   
}
