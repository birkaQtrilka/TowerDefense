using UnityEngine;

public class LifeManager : MonoBehaviour
{
	[SerializeField] int _lifeAmount = 3;
	[SerializeField] EndPoint _endPoint;


	public int LifeAmount
	{
		get { return _lifeAmount; }
		set { _lifeAmount = value; }
	}

    void OnEndPointReached(Enemy enemy)
    {
        _lifeAmount--;

        if (_lifeAmount <= 0)
        {
            GameManager.Instance.DoGameOver();
        }
    }

    void OnEnable()
    {
        _endPoint.EnemyReached.AddListener(OnEndPointReached);    
    }

    void OnDisable()
    {
        _endPoint.EnemyReached.RemoveListener(OnEndPointReached);

    }
}
