using UnityEngine;
[CreateAssetMenu(menuName = "Wave")]
public class WavesAsset : ScriptableObject
{
    [SerializeField] EnemyWave[] _waves;

    public EnemyWave[] GetWaves()
    {
        return _waves.Clone() as EnemyWave[];
    }
}
