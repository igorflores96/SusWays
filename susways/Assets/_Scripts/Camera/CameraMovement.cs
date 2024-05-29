using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Criar um vetor de direção sem movimento no eixo Y
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        // Normalizar o vetor de direção para manter a mesma velocidade em todas as direções
        direction.Normalize();

        // Converter a direção local em direção global, considerando a rotação da câmera
        direction = Quaternion.Euler(0, transform.eulerAngles.y, 0) * direction;

        // Aplicar o movimento apenas nas direções horizontal e vertical
        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }
}
