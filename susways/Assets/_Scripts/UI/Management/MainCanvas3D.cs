using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas3D : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _playLocalButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _backMenuButton;
    [SerializeField] private Button _quitButton;
    
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField[] _playerInputFields;

    [Header("Managers")]
    [SerializeField] private PlayerSelection _playerSelection;

    [Header("Sound Effects")]
    [SerializeField] AudioSource _wooshIn;
    [SerializeField] AudioSource _wooshOut;



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
        _wooshIn.Play();
    }

    private void PlayLocal()
    {
        _wooshIn.Play();
        _playerSelection.InitLocalGame();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void BackToMenu()
    {
        _wooshOut.Play();
        
        for(int i = 0; i < _playerInputFields.Length; i++)
        {
            _playerInputFields[i].text = "";
        }
    }
}
