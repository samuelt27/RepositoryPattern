namespace RepositoryPattern.DapperORM.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public HashSet<Product>? Products { get; set; }
}