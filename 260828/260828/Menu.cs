public abstract class Menu : IMenuInfo
{
    protected const int SALE_FRY_COUNT = 3;
    protected const int SALE_SKEWER_COUNT = 5;
    protected const int SALE_PRICE = 10;

    private CategoryType _category;
    public CategoryType Category
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

    private bool _isSale;
    public bool IsSale
    {
        get
        {
            return _isSale;
        }
        protected set
        {
            _isSale = value;
        }
    }

    public Menu(CategoryType category, string name, int price)
    {
        _category = category;
        _name = name;
        _price = price;
    }

    public string CategoryName(CategoryType category)
    {
        string categoryName = "";

        switch (category)
        {
            case CategoryType.RiceCakes:
                categoryName = "분식류";
                break;
            case CategoryType.Fry:
                categoryName = "튀김류";
                break;
            case CategoryType.Skewer:
                categoryName = "꼬치류";
                break;
            case CategoryType.Kimbap:
                categoryName = "김밥류";
                break;
        }

        return categoryName;
    }

    public abstract int MenuCalculate(int totalSkewer, int cnt);

    public virtual string PrintSale()
    {
        return "";
    }
}