public interface IMenuInfo
{
    public CategoryType Category { get; }
    public string Name { get; }
    public int Price { get; }
    public bool IsSale { get; }
}