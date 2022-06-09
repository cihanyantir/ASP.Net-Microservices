using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository: IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public Task DeleteBasket(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCart> GetBasket(string userName) //ShoppingCart türünde veriler return edilecek.
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
                return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket)); //Controllera gidecek. Name ve nesnenin jsonu lazım post için.
            return await GetBasket(basket.UserName); //Update edilen userameye göre vierler çekilecek.
        }
    }
}
