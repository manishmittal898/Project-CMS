using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Service.Services.GeneralEntry
{
    public interface IGECategoryService
    {
        Task<ServiceResponse<IEnumerable<GeneralEntryCategoryViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<GeneralEntryCategoryViewModel>> GetById(string id);
        Task<ServiceResponse<object>> Save(GeneralEntryCategoryPostModel model);
        Task<ServiceResponse<TblGecategoryMater>> ActiveStatusUpdate(string id);
        Task<ServiceResponse<TblGecategoryMater>> FlagStatusUpdate(string id, string columnName);
        Task<ServiceResponse<TblGecategoryMater>> Delete(string id);
    }
}
