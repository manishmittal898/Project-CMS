using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.GeneralEntry
{
   public interface IGECategoryService
    {
        Task<ServiceResponse<IEnumerable<GeneralEntryCategoryViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<GeneralEntryCategoryViewModel>> GetById(long id);
        Task<ServiceResponse<TblGecategoryMater>> Save(GeneralEntryCategoryPostModel model);
        Task<ServiceResponse<TblGecategoryMater>> ActiveStatusUpdate(long id);

        Task<ServiceResponse<TblGecategoryMater>> Delete(long id);
    }
}
