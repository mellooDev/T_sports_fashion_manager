using BusinessLogicLayer;
using DataModel;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.BanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingDetailsController : ControllerBase
    {
        private IShippingDetailsBusiness _shippingDetailsBusiness;
        public ShippingDetailsController(IShippingDetailsBusiness shippingDetailsBusiness) 
        {
            _shippingDetailsBusiness = shippingDetailsBusiness;
        }


        [Route("create-shippingDetails")]
        [HttpPost]
        public Shipping_detailsModel CreateShipping([FromBody] Shipping_detailsModel model)
        {
            _shippingDetailsBusiness.Create(model);
            return model;
        }

        [Route("update-shippingDetails")]
        [HttpPost]
        public Shipping_detailsModel UpdateShipping([FromBody] Shipping_detailsModel model)
        {
            _shippingDetailsBusiness.Update(model);
            return model;
        }

    }
}
