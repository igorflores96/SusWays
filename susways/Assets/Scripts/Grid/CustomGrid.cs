using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs
    {
        public int x;
        public int z;
    }

    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _originPosition;
    private TGridObject[,] _gridArray;

    public CustomGrid(int width, int height, float cellSize, Vector3 originPositon, Func<CustomGrid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;
        this._originPosition = originPositon;

        _gridArray = new TGridObject[_width, _height];

        for(int x = 0; x < _gridArray.GetLength(0); x++)
        {
            for(int z = 0; z < _gridArray.GetLength(1); z++)
            {
                _gridArray[x, z] = createGridObject(this, x, z);
            }
        }
    }

    public void TiggerGridObjectChanged(int x, int z)
    {
        if(OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs { x = x, z = z});
    }

    public TGridObject GetGridObject(int x, int z)
    {
        if(x >= 0 && z >= 0 && x < _width && z < _height)
        {
            return _gridArray[x,z];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        
        return GetGridObject(x, z);
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        z = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * _cellSize + _originPosition;
    }
}
