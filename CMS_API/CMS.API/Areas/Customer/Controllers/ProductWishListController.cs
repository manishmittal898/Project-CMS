using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.WishList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public ProductWishListController(IWishListService wishList) => _wishList = wishList;


        [HttpPost]
        public async Task<object> Get(IndexModel model) => await _wishList.GetList(model);


        [HttpPost]
        public async Task<object> AddWishListProduct(WishListPostModel model) => await _wishList.AddProduct(model);


        [HttpPost]
        public async Task<object> RemoveWishListProduct(WishListPostModel model) => await _wishList.RemoveProduct(model);
    }
}
