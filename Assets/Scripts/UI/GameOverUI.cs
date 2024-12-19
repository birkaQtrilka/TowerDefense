using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Button _exitBtn;
    [SerializeField] Button _restartBtn;
    [SerializeField] Button _mainMenuBtn;

    void Awake()
    {
        _restartBtn.onClick.AddListener(() =>
        {
            LevelsManager.Instance.RestartScene();
        });
        _exitBtn.onClick.AddListener(() => LevelsManager.Instance.QuitGame());
        _mainMenuBtn.onClick.AddListener(() => LevelsManager.Instance.GoToMainMenu());

    }
}
