﻿using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.CustomerAddress;
using CMS.Service.Services.UserMaster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Route("api/[area]/[controller]/[action]")]
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
        [HttpGet]
        public async Task<object> Get()
        {
            return await _user.GetById();
        }



        // POST api/<CustomerAccount>
        [HttpPost]
        public async Task<object> Save(UserDetailPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _user.updateProfileDetail(model);

            }
            else
            {
                ServiceResponse<object> objReturn = new ServiceResponse<object>
                {
                    Message = "Invalid",
                    IsSuccess = false,
                    Data = null
                };

                return objReturn;
            }

        }

        // POST api/<CustomerAccount>
        [HttpPost]
        public async Task<object> UpdateProfilePic([FromBody] UserProfilePostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _user.UpdateProfilePic(model);
            }
            else
            {
                ServiceResponse<object> objReturn = new ServiceResponse<object>
                {
                    Message = "Invalid",
                    IsSuccess = false,
                    Data = null
                };
                return objReturn;
            }

        }
    }
}
