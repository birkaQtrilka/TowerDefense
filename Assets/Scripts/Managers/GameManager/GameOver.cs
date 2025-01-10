using UnityEngine;

public class GameOver : State<GameManager>
{
    public GameOver(GameManager c, int prio) : base(c, prio) { }

    public override void OnEnter()
    {
        Time.timeScale = 0;
        context.GameOverUI.gameObject.SetActive(true);

    }

    public override void OnExit()
    {
        if (context.GameOverUI != null)
            context.GameOverUI.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public override void Update()
    {

    }
}
