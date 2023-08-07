using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.ProductMaster;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CMS.Service.Services.WishList
{
    public interface IWishListService
    {
        Task<ServiceResponse<IEnumerable<ProductMasterViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<WishListViewModel>> AddProduct(WishListPostModel model);
        Task<ServiceResponse<WishListViewModel>> RemoveProduct(WishListPostModel model);

        


    }
}
