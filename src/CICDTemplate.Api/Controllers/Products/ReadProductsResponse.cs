using System.Collections.ObjectModel;

namespace CICDTemplate.Api.Controllers.Products;

/// <summary>
///     Read products - response
/// </summary>
public record ReadProductsResponse(Collection<ProductResponse> Products);