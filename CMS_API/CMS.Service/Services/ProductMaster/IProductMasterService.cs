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

        ServiceResponse<ProductMasterViewModel> GetById(long id);
        Task<ServiceResponse<TblProductMaster>> Save(ProductMasterPostModel model);
        Task<ServiceResponse<TblProductMaster>> Delete(long id);
        Task<ServiceResponse<TblProductMaster>> ActiveStatusUpdate(long id);
        Task<ServiceResponse<object>> DeleteProductFile(long id);
        Task<ServiceResponse<List<ProductImageViewModel>>> GetProductFile(long productId);

        Task<ServiceResponse<IEnumerable<ProductCategoryViewModel>>> GetProductCategory(IndexModel model);
        Task<ServiceResponse<IEnumerable<ProductMasterViewModel>>> GetFilterList(ProductFilterModel model);

    }
}

