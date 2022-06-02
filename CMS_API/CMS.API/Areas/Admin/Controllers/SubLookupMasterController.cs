using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.SubLookupMaster;
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
    public class SubLookupMasterController : ControllerBase
    {
        // GET: api/<SubLookupMasterController>
        private readonly ISubLookupMasterService _sublookmstr;
        public SubLookupMasterController(ISubLookupMasterService sublookmstr)
        {
            _sublookmstr = sublookmstr;
        }
        // GET: api/<SubLookupMasterController>
        [HttpGet]
        public object Get()
        {
            return _sublookmstr.GetList();
        }

        // GET api/<SubLookupMasterController>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return _sublookmstr.GetById(id);
        }

        // POST api/<SubLookupMasterController>
        [HttpPost]
        public async Task<object> Post([FromBody] SubLookupMasterViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _sublookmstr.Save(model);

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

        // PUT api/<SubLookupMasterController>/5
        [HttpPut("{id}")]
        public async Task<object> Put(int id, [FromBody] SubLookupMasterViewModel model)
        {


            if (ModelState.IsValid)
            {


                return await _sublookmstr.Edit(id, model);

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

        // DELETE api/<SubLookupMasterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            _sublookmstr.Delete(id);



        }
    }
}
