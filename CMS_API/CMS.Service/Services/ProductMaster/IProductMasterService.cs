using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductMaster
{
    public interface IProductMasterService
    {
        Task<ServiceResponse<IEnumerable<ProductMasterViewModel>>> GetList(IndexModel model);

        ServiceResponse<ProductMasterViewModel> GetById(string id);
        Task<ServiceResponse<TblProductMaster>> Save(ProductMasterPostModel model);
        Task<ServiceResponse<TblProductMaster>> Delete(string id);
        Task<ServiceResponse<TblProductMaster>> ActiveStatusUpdate(string id);
        Task<ServiceResponse<object>> DeleteProductFile(string id);
        Task<ServiceResponse<List<ProductImageViewModel>>> GetProductFile(string productId);

        Task<ServiceResponse<IEnumerable<ProductCategoryViewModel>>> GetProductCategory(IndexModel model);
        Task<ServiceResponse<IEnumerable<ProductMasterViewModel>>> GetFilterList(ProductFilterModel model);

    }
}

