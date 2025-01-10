using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] Button _exitBtn;
    [SerializeField] Button _mainMenuBtn;
    [SerializeField] Button _resumeBtn;

    void Awake()
    {
        _resumeBtn.onClick.AddListener(() => 
        {
            GameManager.Instance.TransitionToState(GameManager.Instance.PreviousState.GetType());
        });    
        _exitBtn.onClick.AddListener(() => LevelsManager.Instance.QuitGame());    
        _mainMenuBtn.onClick.AddListener(() => LevelsManager.Instance.GoToMainMenu());    

    }

}
