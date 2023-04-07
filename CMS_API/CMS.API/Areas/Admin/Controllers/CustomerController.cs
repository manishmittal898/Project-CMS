using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.ProductMaster;
using CMS.Service.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public CustomerController(IUserMasterService user)
        {
            _user = user;
        }

        [HttpPost]
        public async Task<object> GetList(IndexModel model)
        {
            return await _user.GetList(model);
        }
        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(long id)

        {
            return await _user.GetById(id);

        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
