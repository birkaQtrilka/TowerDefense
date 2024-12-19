using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinUI : MonoBehaviour
{
    [SerializeField] Button _exitBtn;
    [SerializeField] Button _continueBtn;
    [SerializeField] Button _mainMenuBtn;

    void Awake()
    {
        _continueBtn.onClick.AddListener(() => LevelsManager.Instance.NextLevel() );
        _exitBtn.onClick.AddListener(() => LevelsManager.Instance.QuitGame());
        _mainMenuBtn.onClick.AddListener(() => LevelsManager.Instance.GoToMainMenu());

    }
}
