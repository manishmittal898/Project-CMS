using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.ProductMaster;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.UserCartProduct
{
    public interface IUserCartProductService
    {
        Task<ServiceResponse<IEnumerable<ProductMasterViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<UserCartProductViewModel>> AddProduct(UserCartProductPostModel model);
        Task<ServiceResponse<UserCartProductViewModel>> RemoveProduct(UserCartProductPostModel model);


    }
}
