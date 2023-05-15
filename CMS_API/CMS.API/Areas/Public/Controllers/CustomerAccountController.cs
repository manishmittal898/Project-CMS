using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.CustomerAddress;
using CMS.Service.Services.LookupMaster;
using CMS.Service.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Public.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerAccountController : ControllerBase
    {

        private readonly IUserMasterService _user;
        private readonly ICustomerAddressService _address;
        public CustomerAccountController(IUserMasterService user, ICustomerAddressService address)
        {
            _user = user;
            _address = address;
        }


        // GET api/<CustomerAccount>/5
        [HttpGet("{id}")]
        public async Task<object> Get(long id)

        {
            return await _user.GetById(id);

        }


        // POST api/<CustomerAccount>
        [HttpPost]
        public async Task<object> Update([FromBody] UserViewPostModel model)
        {
            if (ModelState.IsValid)
            {
                model.RoleId = (int)RoleEnum.Customer;
                return await _user.Save(model);

            }
            else
            {
                ServiceResponse<object> objReturn = new ServiceResponse<object>();
                objReturn.Message = "Invalid";
                objReturn.IsSuccess = false;
                objReturn.Data = null;

                return objReturn;
            }
            //return _roleTyp
        }


        [HttpPost]
        public async Task<object> GetAddressAsync(IndexModel model)
        {
            return await _address.GetList(model);
        }

        // GET api/<LookupMaster>/5
        [HttpGet("{id}")]
        public object GetAddress(long id)
        {
            return _address.GetById(id);
        }

        // POST api/<LookupMaster>
        [HttpPost]
        public async Task<object> SaveAddress(CustomerAddressPostModel model)
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


    }
}
