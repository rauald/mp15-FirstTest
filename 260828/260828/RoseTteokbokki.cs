public class RoseTteokbokki : Menu
{
    public RoseTteokbokki(string category, string name, int price) : base(category, name, price)
    {
    }

    public override int MenuCalculate(int cnt)
    {
        int totalPrice = 0;

        totalPrice = Price * cnt;

        return totalPrice;
    }
}