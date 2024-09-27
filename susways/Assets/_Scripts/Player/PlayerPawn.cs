using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private AudioSource _playerOnGround;


    public void PlayerMove()
    {
        _animator.SetTrigger("Move");
    }

    public void PlayerCancelMove()
    {
        _animator.SetTrigger("CancelMove");
    }

    public void PlayerGrabed()
    {
        _animator.SetTrigger("Grabed");
    }

    public void PlayerShouldTeleport()
    {
        EventManager.PlayerMoveDone();
    }

    public void PlayerEnterBus()
    {
        _animator.SetTrigger("Enter Bus");
    }

    public void PlayerExitBus()
    {
        _animator.SetTrigger("Exit Bus");
    }

    public void PlayerOnGround()
    {
        _playerOnGround.Play();
    }

    public void SetPrefabCaracteristics(Mesh mesh, Color color)
    {
        _meshFilter.mesh = mesh;
        MeshRenderer targetRenderer = _meshFilter.GetComponent<MeshRenderer>();
        targetRenderer.material.color = color;

    }

}
