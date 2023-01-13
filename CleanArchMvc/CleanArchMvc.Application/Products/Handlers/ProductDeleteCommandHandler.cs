using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers
{
    public class ProductDeleteCommandHandler : IRequestHandler<ProducDeleteCommand, Product>
    {
        private readonly IProductRepository _productRepository;

        public ProductDeleteCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(ProducDeleteCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product is null)
            {
                throw new ApplicationException($"Entity could not be found.");
            }
            else
            {
                await _productRepository.DeleteAsync(request.Id);
                return product;
            }
        }
    }
}
