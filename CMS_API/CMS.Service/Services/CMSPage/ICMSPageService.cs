using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.CMSPage
{
  public  interface ICMSPageService
    {
        Task<ServiceResponse<IEnumerable<CMSPageViewModel>>> GetList(IndexModel model);
        ServiceResponse<CMSPageViewModel> GetById(long id);
        Task<ServiceResponse<TblCmspageContentMaster>> Save(CMSPagePostModel model);
        Task<ServiceResponse<TblCmspageContentMaster>> ActiveStatusUpdate(long id);

        Task<ServiceResponse<TblCmspageContentMaster>> Delete(long id);
    }
}
