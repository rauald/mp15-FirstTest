public abstract class Menu : IMenuInfo
{
    private string _category;
    public string Category
    {
        get
        {
            return _category;
        }
        protected set
        {
            _category = value;
        }
    }

    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        protected set
        {
            _name = value;
        }
    }

    private int _price;
    public int Price
    {
        get
        {
            return _price;
        }
        protected set
        {
            _price = value;
        }
    }

    public Menu(string category, string name, int price)
    {
        _category = category;
        _name = name;
        _price = price;
    }

    public virtual int MenuCalculate(int cnt)
    {
        int totalPrice = 0;

        totalPrice = Price * cnt;

        return totalPrice;
    }
}