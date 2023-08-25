using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.CMSPage;
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
    public class CMSPageController : ControllerBase
    {
        private readonly ICMSPageService _service;
        public CMSPageController(ICMSPageService Iservice)
        {
            _service = Iservice;
        }
        // GET: api/<LookupMaster>
        [HttpPost]
        public async Task<object> GetAsync(IndexModel model)
        {
            return await _service.GetList(model);
        }

        // GET api/<LookupMaster>/5
        [HttpGet("{id}")]
        public async Task<object> Get(string id)
        
        {
            return await _service.GetById(id);
        }

        // POST api/<LookupMaster>
        [HttpPost]
        public async Task<object> Save(CMSPagePostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _service.Save(model);

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
        [HttpGet("{id}")]
        public async Task<object> ChangeActiveStatus(string id)
        {
            return await _service.ActiveStatusUpdate(id);
        }

        // DELETE api/<LookupMaster>/5
        [HttpGet("{id}")]
        public async Task<object> Delete(string id)
        {
            return await _service.Delete(id);
        }
    }
}
