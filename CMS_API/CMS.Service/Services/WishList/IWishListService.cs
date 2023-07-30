using CMS.Core.ServiceHelper.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.WishList
{
    public interface IWishListService
    {
        Task<ServiceResponse<IEnumerable<WishListViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<WishListViewModel>> Add(WishListPostModel model);

    }
}
