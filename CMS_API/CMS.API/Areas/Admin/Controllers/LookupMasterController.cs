using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.LookupMaster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupMasterController : ControllerBase
    {

        private readonly ILookupMasterService _lookupmstr;
        public LookupMasterController(ILookupMasterService lookupmstr)
        {
            _lookupmstr = lookupmstr;
        }
        // GET: api/<LookupMaster>
        [HttpGet]
        public object Get()
        {
            return _lookupmstr.GetList();
        }

        // GET api/<LookupMaster>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return _lookupmstr.GetById(id);
        }

        // POST api/<LookupMaster>
        [HttpPost]
        public async Task<object> Post([FromBody] LookupMasterViewModel model)
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

        // PUT api/<LookupMaster>/5
        [HttpPut("{id}")]
        public async Task<object> Put(int id, [FromBody] LookupMasterViewModel model)
        {
            if (ModelState.IsValid)
            {


                return await _lookupmstr.Edit(id, model);

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

        // DELETE api/<LookupMaster>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _lookupmstr.Delete(id);
        }
    }
}
