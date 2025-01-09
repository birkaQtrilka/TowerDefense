public class MoneyTextDisplayer : TextPopUpSpawner
{
    public void ParseMoney(Enemy enemy)
    {
        ShowText(enemy.CarriedMoney.CurrentValue + "$");
    }

}