public class Defend : State<GameManager>
{
    public Defend(GameManager c, int prio) : base(c, prio) { }

    public override void OnEnter()
    {
        context.EnemySpawner.StartSpawning();
    }

    public override void OnExit()
    {
    }

    public override void Update()
    {

    }
}
