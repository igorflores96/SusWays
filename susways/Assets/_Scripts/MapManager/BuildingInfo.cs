using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    [SerializeField] private Sprite _buildingInfo;

    public Sprite BuildingInfoSprite => _buildingInfo;
}
