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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerAccountController : ControllerBase
    {

        private readonly IUserMasterService _user;
        public CustomerAccountController(IUserMasterService user)
        {
            _user = user;
        }


        // GET api/<CustomerAccountController>/5
        [HttpGet("{id}")]
        public async Task<object> Get(long id)

        {
            return await _user.GetById(id);

        }
    }
}
