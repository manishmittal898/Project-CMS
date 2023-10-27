using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.RoleType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RoleTypeController : ControllerBase
    {
        private readonly IRoleTypeService _roleType;
        public RoleTypeController(IRoleTypeService roleType)
        {
            _roleType = roleType;
        }

        // GET: api/<RoleTypeController>
        [HttpGet]
        public object Get()
        {
            return _roleType.GetList();
        }

        // GET api/<RoleTypeController>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return _roleType.GetById(id);
        }

        // POST api/<RoleTypeController>
        [HttpPost]
        public async Task<object> Post([FromBody] RoleTypePostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _roleType.Save(model);

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
            //return _roleTyp
        }

        // PUT api/<RoleTypeController>/5
        [HttpPut("{id}")]
        public async Task<object> Put(int id, [FromBody] RoleTypePostModel model)
        {

            if (ModelState.IsValid)
            {
                return await _roleType.Edit(id, model);
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

        // DELETE api/<RoleTypeController>/5
        [HttpGet("{id}")]
        public void Delete(int id)
        {
            _ = _roleType.Delete(id);
        }
    }
}
