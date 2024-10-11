using UnityEngine;
using UnityEngine.UI;

public class CameraMenu3D : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _lerpSpeed = 5f;
    [SerializeField] private Vector3[] _positionsToGo;

    [Header("Buttons")]
    [SerializeField] private Button _playerSelectionButton;
    [SerializeField] private Button _credits;
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private Button _backMenuButtonCredits;


    private Vector3 _targetPosition;
    private bool _isLerping = false;


    private void OnEnable() 
    {
        _playerSelectionButton.onClick.AddListener(() => GoToNextSpot(PositionToCamGo.PlayerSelection));
        _backMenuButton.onClick.AddListener(() => GoToNextSpot(PositionToCamGo.MainButtons));
        _backMenuButtonCredits.onClick.AddListener(() => GoToNextSpot(PositionToCamGo.MainButtons));
        _credits.onClick.AddListener(() => GoToNextSpot(PositionToCamGo.Credits));


    }

    private void OnDisable() 
    {
        _playerSelectionButton.onClick.RemoveListener(() => GoToNextSpot(PositionToCamGo.PlayerSelection));
        _backMenuButton.onClick.RemoveListener(() => GoToNextSpot(PositionToCamGo.MainButtons));
        _backMenuButtonCredits.onClick.RemoveListener(() => GoToNextSpot(PositionToCamGo.MainButtons));
        _credits.onClick.RemoveListener(() => GoToNextSpot(PositionToCamGo.Credits));


    }

    private void Update() 
    {
        if(_isLerping)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _lerpSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
            {
                _isLerping = false;
            }
        }
    }

    private void GoToNextSpot(PositionToCamGo position)
    {
        _targetPosition = new Vector3(_positionsToGo[(int)position].x, _positionsToGo[(int)position].y, _positionsToGo[(int)position].z);

        _isLerping = true;
    }
}

public enum PositionToCamGo
{
    MainButtons, PlayerSelection, Credits
}
