using Microsoft.Extensions.Logging;
using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders()); //if theres no data, add column
                //orderContext.Orders.AddRange(new Order {UserName="Cihan" });
                //orderContext.Orders.AddRange(DS);

                await orderContext.SaveChangesAsync(); //one more seeding like datemtime by SaveChangesAsync in OrderContext
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }
       //private static List<Order> DS = new List<Order>(){  new Order() {UserName="Cigab" }   } ;
      
        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            return new List<Order>
            {
                new Order() {UserName = "swn", FirstName = "Cihan", LastName = "Yantır", EmailAddress = "cihanyantir98@gmail.com", AddressLine = "Bahcelievler", Country = "Turkey", TotalPrice = 350 }
            };
        }
    }
}