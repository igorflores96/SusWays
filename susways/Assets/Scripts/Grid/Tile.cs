using UnityEngine;

public class Tile
{
    private Transform SelectedTransform;
    private GameObject FloorVisual;
    private int _x;
    private int _z;

    public Tile(int x, int z)
    {
        _x = x;
        _z = z;
    }

    public void Show()
    {
        SelectedTransform.Find("Selected").gameObject.SetActive(true);
    }

    public void Hide()
    {
        SelectedTransform.Find("Selected").gameObject.SetActive(false);
    }

    public void SetFloorTile(GameObject floor)
    {
        FloorVisual = floor;
    }

    public void ClearFloorTile()
    {
        FloorVisual = null;
    }

    public void SetTrasnformTileFeedback(Transform visual)
    {
        SelectedTransform = visual;
    }
}
