using UnityEngine;
/// <summary>
/// responsible of handling when and how you lose lives
/// </summary>
public class LifeManager : MonoBehaviour
{
	[field: SerializeField] public Health LifeAmount { get; private set; }
	[SerializeField] EndPoint _endPoint;

    void OnEndPointReached(Enemy enemy)
    {
        LifeAmount.CurrentValue--;
    }

    void OnLifeChanged(int oldVal, Stat<int> health)
    {
        if(health.CurrentValue <= 0)
            GameManager.Instance.TransitionToState(typeof(GameOver));

    }

    void OnEnable()
    {
        _endPoint.EnemyReached.AddListener(OnEndPointReached);
        LifeAmount.CurrentUpdated.AddListener(OnLifeChanged);
    }

    void OnDisable()
    {
        _endPoint.EnemyReached.RemoveListener(OnEndPointReached);
        LifeAmount.CurrentUpdated.RemoveListener(OnLifeChanged);

    }
}
