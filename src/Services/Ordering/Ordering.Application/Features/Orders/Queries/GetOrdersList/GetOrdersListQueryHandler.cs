using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
    {   //Trigger

        private readonly IOrderRepository _orderrepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderrepository, IMapper mapper)
        {
            _orderrepository = orderrepository;
            _mapper = mapper;
        }

        public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orderlist =await _orderrepository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<OrdersVm>>(orderlist);

        }
    }
}
