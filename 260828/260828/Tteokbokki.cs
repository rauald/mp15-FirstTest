public class Tteokbokki : Menu
{
    public Tteokbokki(CategoryType category, string name, int price) : base(category, name, price)
    {
        IsSale = false;
    }

    public override int MenuCalculate(int totalSkewer, int cnt)
    {
        int totalPrice = 0;

        totalPrice = Price * cnt;

        return totalPrice;
    }
}