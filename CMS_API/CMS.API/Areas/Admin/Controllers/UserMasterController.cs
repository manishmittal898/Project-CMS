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
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class UserMasterController : ControllerBase
    {
        private readonly IUserMasterService _user;
        public UserMasterController(IUserMasterService user)
        {
            _user = user;
        }
        // GET: api/<UserController>
        [HttpGet]
        public object Get()
        {
            return _user.GetList();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return _user.GetById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<object> Post([FromBody] UserViewPostModel model)
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

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<object> Put(int id, [FromBody] UserViewPostModel model)
        {


            if (ModelState.IsValid)
            {


                return await _user.Edit(id, model);

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

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            _user.Delete(id);



        }

    }
}
