using UnityEngine;
using UnityEngine.Events;

public class EndPoint : MonoBehaviour
{
    
    public UnityEvent<Enemy> EnemyReached;

    void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponentInParent<Enemy>();
        if (enemy == null)return;
        Destroy(enemy.gameObject);

    }
}
