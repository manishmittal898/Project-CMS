using CMS.Core.ServiceHelper.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Service.Services.UserCartProduct
{
    public interface IUserCartProductService
    {
        Task<ServiceResponse<IEnumerable<UserCartProductViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<UserCartProductViewModel>> AddProduct(UserCartProductPostModel model);
        Task<ServiceResponse<UserCartProductViewModel>> RemoveProduct(UserCartProductPostModel model);


    }
}
