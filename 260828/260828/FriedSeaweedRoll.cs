public class FriedSeaweedRoll : Menu
{
    public FriedSeaweedRoll(CategoryType category, string name, int price) : base(category, name, price)
    {
        IsSale = true;
    }

    public override int MenuCalculate(int cnt)
    {
        int totalPrice = 0;

        totalPrice = Price * cnt;

        if (IsSale)
        {
            if(Category == CategoryType.Fry)
            {
                if(cnt >= SALE_FRY_COUNT)
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

        str = $"[{SALE_FRY_COUNT}개 이상 구매시 {SALE_PRICE}% 할인]";

        return str;
    }
}