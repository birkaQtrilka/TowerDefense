using UnityEngine;
[System.Serializable]
public sealed class Health : Stat<int>
{
    public override void OnCurrentValueSet(ref int val)
    {
        if(val < 0) val = 0;
    }

}
