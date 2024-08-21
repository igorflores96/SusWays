using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Animator _animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetTrigger("Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetTrigger("Stop");
    }
}
