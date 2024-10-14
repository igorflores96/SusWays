using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private AudioSource _playerOnGround;
    [SerializeField] private ParticleSystem _ps;

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
        _ps.Clear();
        EventManager.PlayerMoveDone();
        _ps.Play();

    }

    public void ActiveParticle()
    {
        _ps.Play();
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

        var mainModule = _ps.main;

        mainModule.startColor = new ParticleSystem.MinMaxGradient(color, Color.white);

    }

}
