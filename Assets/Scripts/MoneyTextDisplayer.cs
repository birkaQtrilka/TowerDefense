public class MoneyTextDisplayer : TextPopUpSpawner
{
    public void ParseMoney(Enemy enemy)
    {
        ShowText(enemy.GetStat<CarriedMoney>().CurrentValue + "$");
    }

}