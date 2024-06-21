using CICDTemplate.Application.Products.Queries;
using CICDTemplate.Domain.Entities;
using CICDTemplate.Domain.Repositories;

using FluentAssertions;

using Moq;

namespace CICDTemplate.Application.UnitTests.Products.ReadProducts;

public class ReadProductsQueryHandlerTests
{
    private Mock<IProductsRepository> _mockProductsRepository;
    
    [SetUp]
    public void Setup()
    {
        _mockProductsRepository = new Mock<IProductsRepository>();
    }

    [Test]
    public async Task Handle_HappyPath_ReturnsProducts()
    {
        // arrange
        Product[] products = new[]
        {
            Product.Create("Cookie", "Yummy!", DateTime.UtcNow),
            Product.Create("Milkshake", "Strayberry flavor", DateTime.UtcNow)
        };

        _mockProductsRepository
            .Setup(x => x.GetProductsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(products);

        var handler = new ReadProductsQueryHandler(_mockProductsRepository.Object);

        // act
        var result = await handler
            .Handle(new ReadProductsQuery(), new CancellationToken())
            .ConfigureAwait(false);

        // assert
        result.Should().BeEquivalentTo(products);
    }
}
