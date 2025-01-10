using UnityEngine;

public class Pause : State<GameManager>
{
    public Pause(GameManager c, int prio) : base(c, prio) { }

    public override void OnEnter()
    {
        Time.timeScale = 0;
        context.PauseMenuUI.gameObject.SetActive(true);

    }

    public override void OnExit()
    {
        if (context.PauseMenuUI != null)
            context.PauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1;

    }

    public override void Update()
    {

    }
}