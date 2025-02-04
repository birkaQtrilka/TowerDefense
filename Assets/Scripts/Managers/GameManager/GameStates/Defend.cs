//Defend phase of game
[System.Serializable]
public class Defend : State<GameManager>
{
    public Defend(GameManager c, int prio) : base(c, prio) { }
    public Defend() : base() { }

    public override void OnEnter()
    {
        context.EnemySpawner.StartSpawning();
        context.CanChangeTime = true;
    }

    public override void OnExit()
    {
        context.CanChangeTime = false;

    }

    public override void Update()
    {

    }
}
