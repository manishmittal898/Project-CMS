using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.UserCartProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMS.API.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class UserCartProductController : ControllerBase
    {

        private readonly IUserCartProductService _cartList;
        public UserCartProductController(IUserCartProductService cartList)
        {
            _cartList = cartList;
        }

        [HttpPost]
        public async Task<object> Get(IndexModel model)

        {
            return await _cartList.GetList(model);

        }

        [HttpPost]
        public async Task<object> AddCartProduct(UserCartProductPostModel model)

        {
            return await _cartList.AddProduct(model);

        }
        [HttpPost]
        public async Task<object> RemoveCartProduct(UserCartProductPostModel model)

        {
            return await _cartList.RemoveProduct(model);

        }
    }
}
