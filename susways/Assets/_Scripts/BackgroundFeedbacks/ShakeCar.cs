using UnityEngine;

public class ShakeCar : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnMouseEnter() 
    {
        _animator.SetTrigger("Shake Car");
    }

    private void OnMouseDown()
    {
        _animator.SetTrigger("Car Jump");
    }
}
