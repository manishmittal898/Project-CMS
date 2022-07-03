using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.LookupMaster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Admin.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class LookupMasterController : ControllerBase
    {

        private readonly ILookupMasterService _lookupmstr;
        public LookupMasterController(ILookupMasterService lookupmstr)
        {
            _lookupmstr = lookupmstr;
        }
        // GET: api/<LookupMaster>
        [HttpPost]
        public async Task<object> GetAsync(IndexModel model)
        {
            return await _lookupmstr.GetList(model);
        }

        // GET api/<LookupMaster>/5
        [HttpGet("{id}")]
        public object Get(long id)
        {
            return _lookupmstr.GetById(id);
        }

        // POST api/<LookupMaster>
        [HttpPost]
        public async Task<object> Save(LookupMasterPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _lookupmstr.Save(model);

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
        public async Task<object> ChangeActiveStatus(long id)
        {
            return await _lookupmstr.ActiveStatusUpdate(id);
        }

        // DELETE api/<LookupMaster>/5
        [HttpDelete("{id}")]
        public async Task<object> Delete(long id)
        {
        return  await  _lookupmstr.Delete(id);
        }
    }
}
