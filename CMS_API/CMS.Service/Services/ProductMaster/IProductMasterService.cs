﻿using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductMaster
{
    public interface IProductMasterService
    {
        Task<ServiceResponse<IEnumerable<ProductMasterViewModel>>> GetList(IndexModel model);

        ServiceResponse<ProductMasterViewModel> GetById(string id, bool isThumbnail = false);
        ServiceResponse<ProductStockModel> GetStockDetail(string productId, string sizeId);
        Task<ServiceResponse<object>> Save(ProductMasterPostModel model);
        Task<ServiceResponse<TblProductMaster>> Delete(string id);
        Task<ServiceResponse<TblProductMaster>> ActiveStatusUpdate(string id);
        Task<ServiceResponse<object>> DeleteProductFile(string id);
        Task<ServiceResponse<List<ProductImageViewModel>>> GetProductFile(string productId);

        Task<ServiceResponse<IEnumerable<ProductCategoryViewModel>>> GetProductCategory(IndexModel model);
        Task<ServiceResponse<IEnumerable<ProductMasterViewModel>>> GetFilterList(ProductFilterModel model);
        Task<ServiceResponse<object>> IsSKUExist(string SKU, string id);

    }
}

