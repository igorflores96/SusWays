using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    

    private void Update() 
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);

        direction.Normalize();

        direction = Quaternion.Euler(0, transform.eulerAngles.y, 0) * direction;

        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }
}
