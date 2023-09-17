using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Service.Services.GeneralEntry
{
    public interface IGeneralEntryService
    {
        Task<ServiceResponse<List<GeneralEntryViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<GeneralEntryViewModel>> GetById(string id, bool isEdit = false);
        Task<ServiceResponse<object>> Save(GeneralEntryPostModel model);
        Task<ServiceResponse<TblGeneralEntry>> ActiveStatusUpdate(string id);
        Task<ServiceResponse<TblGeneralEntry>> Delete(string id);
        Task<object> DeleteGeneralEntryItems(string id);

        Task<ServiceResponse<List<GeneralEntryViewModel>>> GetDataList(GeneralEntryFilterModel model);
    }
}
