public class Tteokbokki : Menu
{
    public Tteokbokki(CategoryType category, string name, int price) : base(category, name, price)
    {
        IsSale = false;
    }
}