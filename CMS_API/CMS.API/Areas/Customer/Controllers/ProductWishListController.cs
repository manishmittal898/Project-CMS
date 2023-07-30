using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.CustomerAddress;
using CMS.Service.Services.User;
using CMS.Service.Services.WishList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore  .Mvc;
using System.Threading.Tasks;

namespace CMS.API.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class ProductWishListController : ControllerBase
    {

        private readonly IWishListService _wishList;
        public ProductWishListController(IWishListService wishList)
        {
            _wishList=wishList;
        }

        [HttpGet]
        public async Task<object> Get(IndexModel model)

        {
            return await _wishList.GetList(model);

        }

        [HttpPost]
        public async Task<object> AddWishListProduct(WishListPostModel model)

        {
            return await _wishList.AddProduct(model);

        }
        [HttpPost]
        public async Task<object> RemoveWishListProduct(WishListPostModel model)

        {
            return await _wishList.RemoveProduct(model);

        }
    }
}
