using CMS.Service.Services.CMSPage;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Public.Controllers
{
    [Area("Public")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    public class CMSPageController : ControllerBase
    {
        private readonly ICMSPageService _service;
        public CMSPageController(ICMSPageService Iservice)
        {
            _service = Iservice;
        }

        // GET: api/<LookupMaster>

        // GET api/<LookupMaster>/5
        [HttpGet("{id}")]
        public async Task<object> Get(string id)
        {
            return await _service.GetById(id);
        }
    }
}
