using SMediator.Core.Abstractions;

namespace SMediator.Examples.ProductApi.Queries
{
    public record GetProductsQuery() : IRequest<List<Models.ProductDto>>;
}