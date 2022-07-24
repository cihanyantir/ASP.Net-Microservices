using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
   public interface IOrderRepository: IAsyncRepository<Order> //inh
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
