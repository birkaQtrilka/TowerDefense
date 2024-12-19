using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button _exitBtn;
    [SerializeField] Button _levelsBtn;
    [SerializeField] Button _playBtn;

    void Start()
    {
        _levelsBtn.onClick.AddListener(() =>
        {
            LevelsManager.Instance.GoToLevelsScene();
        });
        _exitBtn.onClick.AddListener(() => LevelsManager.Instance.QuitGame());
        _playBtn.onClick.AddListener(() => LevelsManager.Instance.GoToFarthestLevel());
    }
}
