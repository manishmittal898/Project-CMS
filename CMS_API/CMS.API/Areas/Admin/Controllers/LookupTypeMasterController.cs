using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.LookupTypeMaster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    public class LookupTypeMasterController : ControllerBase
    {
        // GET: api/<LookupTypeMasterController>
        private readonly ILookupTypeMasterService _lookuptypemstr;
        public LookupTypeMasterController(ILookupTypeMasterService lookuptypemstr)
        {
            _lookuptypemstr = lookuptypemstr;
        }
        // GET: api/<LookupTypeMasterController>
        [HttpPost]
        public async Task<object> Get(IndexModel model)
        {
            return await _lookuptypemstr.GetListAsync(model);
        }

        // GET api/<LookupTypeMasterController>/5
        [HttpGet("{id}")]
        public object Get(string id)
        {
            return _lookuptypemstr.GetById(id);
        }

        // POST api/<LookupTypeMasterController>
        [HttpPost]
        public async Task<object> Post([FromBody] LookupTypeMasterViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _lookuptypemstr.Save(model);

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

        // PUT api/<LookupTypeMasterController>/5
        [HttpPut("{id}")]
        public async Task<object> Put(string id, [FromBody] LookupTypeMasterViewModel model)
        {


            if (ModelState.IsValid)
            {


                return await _lookuptypemstr.Edit(id, model);

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

        // DELETE api/<LookupTypeMasterController>/5
        [HttpGet("{id}")]
        public void Delete(string id)
        {

            _lookuptypemstr.Delete(id);



        }
    }
}
