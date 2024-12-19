using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBtn : Button
{
    protected override void OnEnable()
    {
        base.OnEnable();

        onClick.AddListener(GoToMainMenu);
    }

    protected override void OnDisable()
    {
        base.OnEnable();
        onClick.RemoveListener(GoToMainMenu);

    }

    void GoToMainMenu()
    {
        LevelsManager.Instance.GoToMainMenu();
    }
}
