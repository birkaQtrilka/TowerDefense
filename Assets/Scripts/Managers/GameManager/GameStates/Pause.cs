using UnityEngine;

[System.Serializable]
public class Pause : State<GameManager>
{
    public Pause(GameManager c, int prio) : base(c, prio) { }
    public Pause() : base() { }
    float _timeScaleBeforePause;

    public override void OnEnter()
    {
        _timeScaleBeforePause = Time.timeScale;
        Time.timeScale = 0;
        context.PauseMenuUI.gameObject.SetActive(true);

    }

    public override void OnExit()
    {
        if (context.PauseMenuUI != null)
            context.PauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = _timeScaleBeforePause;

    }

    public override void Update()
    {

    }
}