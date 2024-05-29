using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2Int _currentPosition;
    private Vector2Int _lastPosition;

    public Vector2Int CurrentPosition => _currentPosition;
    public Vector2Int PlayerLastPosition => _lastPosition;
}
