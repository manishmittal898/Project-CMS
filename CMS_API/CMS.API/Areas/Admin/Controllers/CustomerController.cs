using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly IUserMasterService _user;
        public CustomerController(IUserMasterService user) => _user = user;


        [HttpPost]
        public async Task<object> Get(IndexModel model)
        {
            if (model.AdvanceSearchModel == null)
            {
                model.AdvanceSearchModel = new Dictionary<string, object>();
            }
            model.AdvanceSearchModel["roleId"] = (int)RoleEnum.Customer;
            return await _user.GetList(model);
        }
        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(long id)        => await _user.GetById(id);


        // POST api/<CustomerAccount>
        [HttpPost]
        public async Task<object> Save([FromBody] UserMasterPostModel model)
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


        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
