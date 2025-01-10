using UnityEngine;

public class GameWin : State<GameManager>
{
    public GameWin(GameManager c, int prio) : base(c, prio) { }

    public override void OnEnter()
    {
        Time.timeScale = 0;
        context.GameWinUI.gameObject.SetActive(true);
        LevelsManager.Instance.MarkNextLevelUnlocked();
    }

    public override void OnExit()
    {
        if (context.GameWinUI != null)
            context.GameWinUI.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public override void Update()
    {

    }
}
