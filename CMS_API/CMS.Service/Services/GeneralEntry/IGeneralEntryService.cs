using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.GeneralEntry
{
   public interface IGeneralEntryService
    {
        Task<ServiceResponse<IEnumerable<GeneralEntryViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<GeneralEntryViewModel>> GetById(long id);
        Task<ServiceResponse<TblGeneralEntry>> Save(GeneralEntryPostModel model);
        Task<ServiceResponse<TblGeneralEntry>> ActiveStatusUpdate(long id);
         Task<ServiceResponse<TblGeneralEntry>> Delete(long id);
    }
}
