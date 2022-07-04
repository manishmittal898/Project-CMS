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

        ServiceResponse<ProductMasterViewModel> GetById(int id);
        Task<ServiceResponse<TblProductMaster>> Save(ProductMasterPostModel model);
        Task<ServiceResponse<TblProductMaster>> Delete(int id);
        Task<ServiceResponse<TblProductMaster>> ActiveStatusUpdate(long id);
    }
}

