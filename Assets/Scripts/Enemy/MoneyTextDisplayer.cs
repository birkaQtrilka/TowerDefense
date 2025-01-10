/// <summary>
/// spawns text that flies up and wobbles
/// </summary>
public class MoneyTextDisplayer : TextPopUpSpawner
{
    public void ParseMoney(Enemy enemy)
    {
        ShowText(enemy.CarriedMoney.CurrentValue + "$");
    }

}