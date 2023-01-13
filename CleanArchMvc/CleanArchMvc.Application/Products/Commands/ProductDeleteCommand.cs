using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Products.Commands
{
    public class ProducDeleteCommand : IRequest<Product>
    {
        public int? Id { get; set; }

        public ProducDeleteCommand(int? id)
        {
            Id = id;
        }
    }
}
