using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductReview
{
    public interface IProductReviewService
    {
        ServiceResponse<IEnumerable<Data.Models.TblProductReview>> GetList(string Id);
        ServiceResponse<TblProductReview> GetById(string id);

        Task<ServiceResponse<TblProductReview>> Save(ProductReviewViewModel model);
        Task<ServiceResponse<TblProductReview>> Edit(string id, ProductReviewViewModel model);
        Task<ServiceResponse<TblProductReview>> Delete(string id);
    }
}
