using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
    [SerializeField] private Animator _animator;

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
}
