using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.CMSPage
{
    public class CMSPageService : BaseService, ICMSPageService
    {
        Task<ServiceResponse<TblCmspageContentMaster>> ICMSPageService.ActiveStatusUpdate(long id)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<TblCmspageContentMaster>> ICMSPageService.Delete(long id)
        {
            throw new NotImplementedException();
        }

        ServiceResponse<CMSPageViewModel> ICMSPageService.GetById(long id)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<IEnumerable<CMSPageViewModel>>> ICMSPageService.GetList(IndexModel model)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse<TblCmspageContentMaster>> ICMSPageService.Save(CMSPagePostModel model)
        {
            throw new NotImplementedException();
        }
    }
}
