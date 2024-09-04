using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChallangeManager : MonoBehaviour
{
    [SerializeField] private Button _choiceOne;
    [SerializeField] private Button _choiceTwo;
    [SerializeField] private Button _choiceThree;
    [SerializeField] private Button _choiceFour;
    [SerializeField] private Button _confirmAnswer;
    [SerializeField] private TextMeshProUGUI _textOne;
    [SerializeField] private TextMeshProUGUI _textTwo;
    [SerializeField] private TextMeshProUGUI _textThree;
    [SerializeField] private TextMeshProUGUI _textFour;
    [SerializeField] private TextMeshProUGUI _cardText;
    [SerializeField] private GameObject _correctPanelFeedback;
    [SerializeField] private GameObject _wrongPanelFeedback;
    [SerializeField] private GameObject _buttonsPanel;
    [SerializeField] private Animator _animator;
    private ChallangeCards _currentCard;
    private bool _isCorrectAnswer;


    private void OnEnable() 
    {
        _currentCard = null;
        _isCorrectAnswer = false;
        _choiceOne.onClick.AddListener(ChoiceOne);
        _choiceTwo.onClick.AddListener(ChoiceTwo);
        _choiceThree.onClick.AddListener(ChoiceThree);
        _choiceFour.onClick.AddListener(ChoiceFour);
        _confirmAnswer.onClick.AddListener(ConfirmAnswer);

    }

    private void OnDisable() 
    {
        _choiceOne.onClick.RemoveListener(ChoiceOne);
        _choiceTwo.onClick.RemoveListener(ChoiceTwo);
        _choiceThree.onClick.RemoveListener(ChoiceThree);
        _choiceFour.onClick.RemoveListener(ChoiceFour);
        _confirmAnswer.onClick.RemoveListener(ConfirmAnswer);

    }
    public void SetCardChallange(ChallangeCards card)
    {
        _currentCard = card;

        _textOne.text = card._answers[0].OptionText;
        _textTwo.text = card._answers[1].OptionText;
        _textThree.text = card._answers[2].OptionText;
        _textFour.text = card._answers[3].OptionText;
        _cardText.text = card.Text;

        _buttonsPanel.SetActive(true);
        _wrongPanelFeedback.SetActive(false);
        _correctPanelFeedback.SetActive(false);

        _animator.SetTrigger("Open");

    }

    private void ChoiceFour()
    {
        if(_currentCard._answers[3].IsCorrect)
            _isCorrectAnswer = true;
        else
            _isCorrectAnswer = false;

        _choiceOne.interactable = true;
        _choiceTwo.interactable = true;
        _choiceThree.interactable = true;
        _choiceFour.interactable = false;

    }

    private void ChoiceThree()
    {
        if(_currentCard._answers[2].IsCorrect)
            _isCorrectAnswer = true;
        else
            _isCorrectAnswer = false;

        _choiceOne.interactable = true;
        _choiceTwo.interactable = true;
        _choiceThree.interactable = false;
        _choiceFour.interactable = true;
    }

    private void ChoiceTwo()
    {
        if(_currentCard._answers[1].IsCorrect)
            _isCorrectAnswer = true;
        else
            _isCorrectAnswer = false;

        _choiceOne.interactable = true;
        _choiceTwo.interactable = false;
        _choiceThree.interactable = true;
        _choiceFour.interactable = true;
    }

    private void ChoiceOne()
    {
        if(_currentCard._answers[0].IsCorrect)
            _isCorrectAnswer = true;
        else
            _isCorrectAnswer = false;

        _choiceOne.interactable = false;
        _choiceTwo.interactable = true;
        _choiceThree.interactable = true;
        _choiceFour.interactable = true;
    }

    private void ConfirmAnswer()
    {
        _choiceOne.interactable = true;
        _choiceTwo.interactable = true;
        _choiceThree.interactable = true;
        _choiceFour.interactable = true;
        _buttonsPanel.SetActive(false);
        
        if(_isCorrectAnswer)
            _correctPanelFeedback.SetActive(true); 
        else
            _wrongPanelFeedback.SetActive(true);

        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        yield return new WaitForSeconds(0.4f);

        EventManager.PlayersAnswerChallange(_isCorrectAnswer);
        _animator.SetTrigger("Close");
        EventManager.ShouldShowUI();
    }

}
