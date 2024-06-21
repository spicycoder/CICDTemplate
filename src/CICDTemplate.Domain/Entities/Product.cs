namespace CICDTemplate.Domain.Entities;

/// <summary>
///     Product entity
/// </summary>
public sealed class Product : Entity
{
    private Product()
    {
    }

    /// <summary>
    ///     Product name
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    ///     Product description
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    ///     Creates an instance of <see cref="Product" />
    /// </summary>
    /// <param name="name">Product name</param>
    /// <param name="description">Product description</param>
    /// <returns>Product</returns>
    public static Product Create(
        string name,
        string? description,
        DateTime createdAtUtc)
    {
        return new Product
        {
            Name = name,
            Description = description,
            CreatedAtUtc = createdAtUtc
        };
    }
}
