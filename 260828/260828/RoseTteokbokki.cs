public class RoseTteokbokki : Menu
{
    public RoseTteokbokki(CategoryType category, string name, int price) : base(category, name, price)
    {
        IsSale = false;
    }
}