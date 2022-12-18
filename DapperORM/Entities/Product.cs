namespace RepositoryPattern.DapperORM.Entities;

public class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }

    public Category Category { get; set; } = null!;
}