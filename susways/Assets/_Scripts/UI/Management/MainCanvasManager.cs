using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _playLocalButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private Button _quitButton;

    [Header("Canvas")]
    [SerializeField] private GameObject _playerSelectionCanvas;
    [SerializeField] private GameObject _buttonsCanvas;
    
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField[] _playerInputFields;

    [Header("Managers")]
    [SerializeField] private PlayerSelection _playerSelection;
    [SerializeField] private Animator _canvasAnimator;


    private void OnEnable() 
    {
        _playLocalButton.onClick.AddListener(PlayLocal);
        _creditsButton.onClick.AddListener(OpenCredits);
        _backMenuButton.onClick.AddListener(BackToMenu);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        _playLocalButton.onClick.RemoveListener(PlayLocal);
        _creditsButton.onClick.RemoveListener(OpenCredits);
        _backMenuButton.onClick.RemoveListener(BackToMenu);
        _quitButton.onClick.RemoveListener(QuitGame);

    }

    private void OpenCredits()
    {
        Debug.Log("Abriu creditos");
    }

    private void PlayLocal()
    {
        _canvasAnimator.SetTrigger("OpenGame");
        _playerSelection.InitLocalGame();
    }

    private void QuitGame()
    {
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }

    private void BackToMenu()
    {
        _canvasAnimator.SetTrigger("BackMenu");

        for(int i = 0; i < _playerInputFields.Length; i++)
        {
            _playerInputFields[i].text = "";
        }
    }
}
