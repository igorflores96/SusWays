using UnityEngine;

public class Tile
{
    private TileFeedback SelectedTransform;
    private bool _isWalkable;
    private int _x;
    private int _z;

    public bool IsWalkable => _isWalkable;
    public int X => _x;
    public int Z => _z;


    public Tile(int x, int z)
    {
        _x = x;
        _z = z;
    }

    public void Show()
    {
        SelectedTransform.ShowFeedback();
    }

    public void Hide()
    {
        SelectedTransform.HideFeedback();
    }

    public void ShowWalkFeedback()
    {
        SelectedTransform.ShowWalkFeedback();
    }
    
    public void HideWalkFeedback()
    {
        SelectedTransform.HideWalkFeedback();
    }

    public void HideAllFeedbacks()
    {
        SelectedTransform.HideWalkFeedback();
        SelectedTransform.HideFeedback();
    }

    public void SetTrasnformTileFeedback(TileFeedback visual)
    {
        SelectedTransform = visual;
    }

    public void SetWalkableStatus(bool status)
    {
        _isWalkable = status;
    }
}
