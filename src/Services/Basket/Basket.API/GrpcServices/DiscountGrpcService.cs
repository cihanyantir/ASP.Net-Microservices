using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountprotoservice; 
        // FROM Discount.Grpc
        

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountprotoservice)
        {
            _discountprotoservice = discountprotoservice;
        }
        public async Task<CouponModel> GetDiscount (string productName)
        {
            var discountrequest = new GetDiscountRequest { ProductName = productName };
            //GetDiscountRequest metod içindeki parametre. Bu parametreyle grpcden veri çekiyorsun.
            //gRPC ile iletişim için bağlı hizmet ekledik. Böylece discountgrpe basketten erişebildik.
            //Burası discount'un çekildği repository. Controllerda bunu işledin.
            //Restful apilerdeki gibi apiden apiye veri transferini url ile yapamıyorsun. 
           
            return await _discountprotoservice.GetDiscountAsync(discountrequest);
           
        }
    }
}
