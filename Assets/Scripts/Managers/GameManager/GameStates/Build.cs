using UnityEngine;
//Build phase of game
[System.Serializable]
public class Build : State<GameManager>
{
    float _buildingTimer;

    public Build(GameManager c, int prio) : base(c,prio) { }
    public Build() : base() { }
    float _timeScaleBeforeEnter;
    public override void OnEnter()
    {
        _timeScaleBeforeEnter = Time.timeScale;
        Time.timeScale = 1;
        context.Store.gameObject.SetActive(true);
        context.TowerPlacer.enabled = true;
        context.TowerSelector.enabled = true;
        context.BuildTimeUI.gameObject.SetActive(true);
        _buildingTimer = context.BuildTime;

    }

    public override void OnExit()
    {
        if(context.Store != null)
            context.Store.gameObject.SetActive(false);
        if (context.TowerPlacer != null)
        {
            context.TowerPlacer.Deselect();
            context.TowerPlacer.enabled = false;

        }
        if (context.TowerSelector != null)
            context.TowerSelector.Deselect();
        if (context.BuildTimeUI != null)
            context.BuildTimeUI.gameObject.SetActive(false);
        if (context.TowerSelector != null)
            context.TowerSelector.enabled = false;
        Time.timeScale = _timeScaleBeforeEnter;
    }

    public override void Update()
    {
        _buildingTimer -= Time.deltaTime;

        if(_buildingTimer <= 0)
            context.TransitionToState(typeof(Defend));
        else
            context.BuildTimeUI.ShowTime(_buildingTimer);

    }
}
