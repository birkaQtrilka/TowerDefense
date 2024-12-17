using UnityEngine;

public class LifeManager : MonoBehaviour
{
	[field: SerializeField] public Health LifeAmount { get; private set; }
	[SerializeField] EndPoint _endPoint;


    void OnEndPointReached(Enemy enemy)
    {
        LifeAmount.CurrentValue--;
    }

    void OnLifeChanged(int oldVal, int newVal)
    {
        if(newVal <=0)
            GameManager.Instance.TransitionToState(typeof(GameOver));

    }

    void OnEnable()
    {
        _endPoint.EnemyReached.AddListener(OnEndPointReached);
        LifeAmount.OnCurrentUpdate.AddListener(OnLifeChanged);
    }

    void OnDisable()
    {
        _endPoint.EnemyReached.RemoveListener(OnEndPointReached);
        LifeAmount.OnCurrentUpdate.RemoveListener(OnLifeChanged);

    }
}
