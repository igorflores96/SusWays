using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _diceObject;
    [SerializeField] private AudioSource _diceClicked;
    [SerializeField] private AudioSource _diceRolling;
    [SerializeField] private AudioSource _diceThrow;
    private int _number;

    private void OnEnable() 
    {
        EventManager.OnNewPlayerTurn += Active;
    }

    private void OnDisable() 
    {
        EventManager.OnNewPlayerTurn -= Active;
    }

    private void OnMouseDown() 
    {
        _diceClicked.Play();
        PlayAnimation();   
    }

    public void PlayAnimation()
    {
        switch(_number)
        {
            case 1: _animator.SetTrigger("Roll 1"); break;
            case 2: _animator.SetTrigger("Roll 2"); break;
            case 3: _animator.SetTrigger("Roll 3"); break;
            case 4: _animator.SetTrigger("Roll 4"); break;
            case 5: _animator.SetTrigger("Roll 5"); break;
            case 6: _animator.SetTrigger("Roll 6"); break;
        }
    }

    public void EndAnimation()
    {
        Invoke("Desactive", 1.5f);
    }

    public void Desactive()
    {
        if(TryGetComponent(out BoxCollider collider))
            collider.enabled = false;
        
        _animator.SetTrigger("Hide");
        EventManager.DiceEnd();

    }

    public void Active(Mission playerMission, PlayerBaseState state)
    {

        if(TryGetComponent(out BoxCollider collider))
            collider.enabled = true;
        
        transform.position = new Vector3((state.GetPosition().x + 2f) * 2, 1.0f, state.GetPosition().y * 2);
        _number = state.GetDiceNumber();
        _animator.SetTrigger("Show");
        _diceRolling.Play();
    }

    public void DiceRoll()
    {
        _diceThrow.Play();
    }
}
