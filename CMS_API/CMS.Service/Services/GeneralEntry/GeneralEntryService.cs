using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.GeneralEntry
{
    public class GeneralEntryService : IGeneralEntryService
    {
        public Task<ServiceResponse<TblGeneralEntry>> ActiveStatusUpdate(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TblGeneralEntry>> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TblGeneralEntry>> FlagStatusUpdate(long id, string columnName)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<GeneralEntryViewModel>> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<IEnumerable<GeneralEntryViewModel>>> GetList(IndexModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TblGeneralEntry>> Save(GeneralEntryPostModel model)
        {
            throw new NotImplementedException();
        }
    }
}
