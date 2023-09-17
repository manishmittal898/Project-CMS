using CMS.Core.ServiceHelper.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.Service.Services.CMSPage
{
    public interface ICMSPageService
    {
        Task<ServiceResponse<IEnumerable<CMSPageListViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<List<CMSPageViewModel>>> GetById(string id);
        Task<ServiceResponse<string>> Save(CMSPagePostModel model);
        Task<ServiceResponse<string>> ActiveStatusUpdate(string id);

        Task<ServiceResponse<string>> Delete(string id);
    }
}
