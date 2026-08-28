public class SpicyFishCakes : Menu
{
    public SpicyFishCakes(CategoryType category, string name, int price) : base(category, name, price)
    {
        IsSale = true;
    }

    public override int MenuCalculate(int totalSkewer, int cnt)
    {
        int totalPrice = 0;

        totalPrice = Price * cnt;

        if (IsSale)
        {
            if (Category == CategoryType.Skewer)
            {
                if (totalSkewer >= SALE_SKEWER_COUNT)
                {
                    int sale = Price * cnt;
                    sale /= SALE_PRICE;

                    totalPrice -= sale;
                }
            }
        }

        return totalPrice;
    }

    public override string PrintSale()
    {
        string str = "";

        str = $"[{CategoryName(Category) } {SALE_SKEWER_COUNT}개 이상 구매시 {SALE_PRICE}% 할인]";

        return str;
    }
}