public class HpTextDisplayer : TextPopUpSpawner
{
    public void ParseHp(int prev,int hp)
    {
        ShowText("hp: " + (prev - hp));
    }

}
