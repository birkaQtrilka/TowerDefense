public class HpTextDisplayer : TextPopUpSpawner
{
    public void ParseHp(int prev, Stat<int> hp)
    {
        ShowText("hp: " + (prev - hp.CurrentValue));
    }

}
