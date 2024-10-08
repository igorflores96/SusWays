using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _diceObject;
    [SerializeField] private AudioSource _diceClicked;
    [SerializeField] private AudioSource _diceRolling;
    [SerializeField] private AudioSource _diceThrow;
    [SerializeField] private GameObject _feedback;
    private int _number;
    private bool shouldSubtract;

    private void OnEnable() 
    {
        EventManager.OnNewPlayerTurn += Active;
        EventManager.OnPlayerHaveBonus += ActiveFeedback;
        shouldSubtract = false;
    }

    private void OnDisable() 
    {
        EventManager.OnNewPlayerTurn -= Active;
        EventManager.OnPlayerHaveBonus -= ActiveFeedback;
    }

    private void OnMouseDown() 
    {
        _diceClicked.Play();
        
        if(TryGetComponent(out BoxCollider collider))
            collider.enabled = false;
        
        DesactiveFeedback();
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
        DesactiveFeedback();
        EventManager.DiceEnd();

    }

    public void Active(Mission playerMission, PlayerBaseState state)
    {

        if(TryGetComponent(out BoxCollider collider))
            collider.enabled = true;
        
        transform.position = new Vector3((state.GetPosition().x + 2f) * 2, 1.0f, state.GetPosition().y * 2);
        _number = shouldSubtract ? state.GetDiceNumber() - 1 : state.GetDiceNumber();
        _animator.SetTrigger("Show");
        _diceRolling.Play();
    }

    public void DiceRoll()
    {
        _diceThrow.Play();
    }

    private void ActiveFeedback()
    {
        shouldSubtract = true;
        _feedback.SetActive(true);
    }

    private void DesactiveFeedback()
    {
        _feedback.SetActive(false);
        shouldSubtract = false;
    }
}
