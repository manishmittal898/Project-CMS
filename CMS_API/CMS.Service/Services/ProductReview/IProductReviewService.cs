using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductReview
{
  public interface IProductReviewService
    {
        ServiceResponse<IEnumerable<Data.Models.TblProductReview>> GetList();
        ServiceResponse<TblProductReview> GetById(int id);

        Task<ServiceResponse<TblProductReview>> Save(ProductReviewViewModel model);
        Task<ServiceResponse<TblProductReview>> Edit(int id, ProductReviewViewModel model);
        Task<ServiceResponse<TblProductReview>> Delete(int id);
    }
}
