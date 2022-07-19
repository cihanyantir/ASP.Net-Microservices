using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountgrpcservice; //grpc servisini kullanabilmek için

        public BasketController(IBasketRepository repository, DiscountGrpcService discountgrpcservice)
        {
            _repository = repository;
            _discountgrpcservice = discountgrpcservice;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName)); //yoksa oluştur
        }
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
           //DİSCOUNTGRPC'DEN VERİ ÇEKİLİP İNDİRİM İŞLENİYOR.
            foreach (var item in basket.Items)
            {
                var coupon = await _discountgrpcservice.GetDiscount(item.ProductName);  //grpc controller yok serviceden çekiyor
                              //basketapi içerisindeki grpc servisindeki getdiscount metodu ile veri çekiliyor.
                item.Price -= coupon.Amount;
                 

            }
            return Ok(await _repository.UpdateBasket(basket));
        }
        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }
    }
}
