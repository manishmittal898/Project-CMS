using CMS.Core.ServiceHelper.Model;

using CMS.Service.Services.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    public class UserMasterController : ControllerBase
    {
        private readonly IUserMasterService _user;
        public UserMasterController(IUserMasterService user)
        {
            _user = user;
        }

        [HttpPost]
        public async Task<object> Get(IndexModel model)
        {
            return await _user.GetList(model);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return _user.GetById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<object> Post([FromBody] UserMasterPostModel model)
        {
            if (ModelState.IsValid)
            {
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


        // DELETE api/<UserController>/5
        [HttpGet("{id}")]
        public void Delete(int id)
        {
            _user.Delete(id);
        }

    }
}
