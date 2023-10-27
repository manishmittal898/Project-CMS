using CMS.Service.Services.GeneralEntry;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Public.Controllers
{
    [Area("Public")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    public class GeneralEntryDataController : ControllerBase
    {
        private readonly IGeneralEntryService _service;
        public GeneralEntryDataController(IGeneralEntryService Iservice)
        {
            _service = Iservice;
        }

        // GET: api/<GeneralEntryController>
        // GET: api/<UserController>
        [HttpPost]
        public async Task<object> GetList(GeneralEntryFilterModel model)
        {
            return await _service.GetDataList(model);
        }
    }
}
