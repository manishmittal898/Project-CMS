using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.CustomerAddress;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMS.API.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class AddressController : ControllerBase
    {
        private readonly ICustomerAddressService _address;
        public AddressController(ICustomerAddressService address)
        {
            _address = address;
        }

        [HttpPost]
        public async Task<object> Get(IndexModel model)
        {
            return await _address.GetList(model);
        }

        // GET api/<LookupMaster>/5
        [HttpGet("{id}")]
        public object Get(string id)
        {
            return _address.GetById(id);
        }

        // POST api/<LookupMaster>
        [HttpPost]
        public async Task<object> Save(CustomerAddressPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _address.Save(model);

            }
            else
            {
                ServiceResponse<object> objReturn = new ServiceResponse<object>();
                objReturn.Message = "Invalid";
                objReturn.IsSuccess = false;
                objReturn.Data = null;

                return objReturn;
            }
        }



        // DELETE api/<GeneralEntryCategory>/5
        [HttpGet("{id}")]
        public async Task<object> SetPrimary(string id)
        {
            return await _address.PrimaryStatusUpdate(id);
        }

        // DELETE api/<GeneralEntryCategory>/5
        [HttpGet("{id}")]
        public async Task<object> ChangeActiveStatus(string id)
        {
            return await _address.ActiveStatusUpdate(id);
        }



        // DELETE api/<GeneralEntryCategory>/5
        [HttpGet("{id}")]
        public async Task<object> Delete(string id)
        {
            return await _address.Delete(id);
        }

    }
}
