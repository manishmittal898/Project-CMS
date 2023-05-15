using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Services.LookupMaster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.CustomerAddress
{
    public interface ICustomerAddressService
    {
        Task<ServiceResponse<IEnumerable<CustomerAddressViewModel>>> GetList(IndexModel model);
        ServiceResponse<CustomerAddressViewModel> GetById(long id);
        Task<ServiceResponse<TblUserAddressMaster>> Save(CustomerAddressPostModel model);
        Task<ServiceResponse<TblUserAddressMaster>> PrimaryStatusUpdate(long id);
        Task<ServiceResponse<TblUserAddressMaster>> ActiveStatusUpdate(long id);
 
        Task<ServiceResponse<TblUserAddressMaster>> Delete(long id);
    }
}
