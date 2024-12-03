using System.Collections;
using UnityEngine;
[RequireComponent (typeof(Bullet))]
public class Exploder : MonoBehaviour
{
    [SerializeField] float _addedRadius;

    BoxCollider _col;

    public void Explode()
    {
        StartCoroutine(ExplosiveTimer());
    }

    IEnumerator ExplosiveTimer()
    {
        var oldSize = _col.size;
        _col.size += new Vector3(_addedRadius, _addedRadius);
        yield return new WaitForSeconds(.1f);
        _col.size = oldSize;
    }
}
