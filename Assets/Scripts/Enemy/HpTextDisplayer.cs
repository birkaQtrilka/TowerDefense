/// <summary>
/// spawns text that flies up and wobbles
/// </summary>
public class HpTextDisplayer : TextPopUpSpawner
{
    public void ParseHp(int prev, Stat<int> hp)
    {
        if (hp.CurrentValue == 0) return;
        ShowText("hp: " + (prev - hp.CurrentValue));
    }

}
