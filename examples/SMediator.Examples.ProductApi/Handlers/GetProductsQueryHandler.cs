using SMediator.Core.Abstractions;
using SMediator.Examples.ProductApi.Models;

namespace SMediator.Examples.ProductApi.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<Queries.GetProductsQuery, List<ProductDto>>
    {
        public Task<List<ProductDto>> Handle(Queries.GetProductsQuery request, CancellationToken cancellationToken)
        {
            // In real-world use a database; here: static list
            var products = new List<ProductDto>
            {
                new(1, "Laptop", 1200m),
                new(2, "Smartphone", 800m),
                new(3, "Headphones", 150m)
            };
            return Task.FromResult(products);
        }
    }
}