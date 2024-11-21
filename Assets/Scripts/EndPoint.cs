using UnityEngine;
using UnityEngine.Events;

public class EndPoint : MonoBehaviour
{
    public UnityEvent<Enemy> enemyReached;

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Enemy>(out var enemy))return;
        Destroy(enemy);

    }
}
