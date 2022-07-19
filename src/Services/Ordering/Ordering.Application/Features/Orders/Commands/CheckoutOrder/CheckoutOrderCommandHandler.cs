using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderrepo;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailservice;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderrepo, IMapper mapper, IEmailService emailservice, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderrepo = orderrepo;
            _mapper = mapper;
            _emailservice = emailservice;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderentity = _mapper.Map<Order>(request); //need mapperprofile conf
           var neworder= await _orderrepo.AddAsync(orderentity);
            _logger.LogInformation($"Order {neworder.Id} is succsefully created");
            await SendMail(neworder);
            return neworder.Id;



        }
        private async Task SendMail(Order order)
        {
            var email = new Email() { To = "ezozkme@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

            try
            {
                await _emailservice.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
