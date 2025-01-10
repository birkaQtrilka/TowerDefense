using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Decides if the enemies reached the finish
/// </summary>
public class EndPoint : MonoBehaviour
{
    
    public UnityEvent<Enemy> EnemyReached;

    void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponentInParent<Enemy>();
        if (enemy == null)return;
        EnemyReached.Invoke(enemy);
        Destroy(enemy.gameObject);

    }
}
