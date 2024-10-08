using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lerpSpeed = 5f;
    [SerializeField] private float _zCamOffset = 15f;
    [SerializeField] private float _plusGapX;
    [SerializeField] private float _plusGapZ;
    [SerializeField] private float _minusGapX;
    [SerializeField] private float _minusGapZ;

    private Vector3 _targetPosition;
    private bool _isLerping = false;

    private void OnEnable() 
    {
        EventManager.OnNewPlayerTurn += GoToPlayer;
    }

    private void OnDisable() 
    {
        EventManager.OnNewPlayerTurn -= GoToPlayer;
    }

    private void Update() 
    {
        if(!_isLerping)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0, vertical);
            direction.Normalize();
            direction = Quaternion.Euler(0, transform.eulerAngles.y, 0) * direction;

            transform.Translate(direction * _speed * Time.deltaTime, Space.World);

            Vector3 currentPosition = transform.position;

            currentPosition.x = Mathf.Clamp(currentPosition.x, _minusGapX, _plusGapX);
            currentPosition.z = Mathf.Clamp(currentPosition.z, _minusGapZ, _plusGapZ);

            transform.position = currentPosition;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _lerpSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
            {
                _isLerping = false;
            }
        }
    }

    private void GoToPlayer(Mission mission, PlayerBaseState player)
    {
        _targetPosition = new Vector3(player.GetPosition().x * 2, transform.position.y, player.GetPosition().y * 2 - _zCamOffset);

        _isLerping = true;
    }
}
