﻿using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.SubLookupMaster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]/[action]")]
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
        [HttpPost]
        public async Task<object> GetAsync(IndexModel model)
        {
            return await _sublookmstr.GetList(model);
        }

        // GET api/<LookupMaster>/5
        [HttpGet("{id}")]
        public object Get(string id)
        {
            return _sublookmstr.GetById(id);
        }

        // POST api/<LookupMaster>
        [HttpPost]
        public async Task<object> Save(SubLookupMasterPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _sublookmstr.Save(model);

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
        [HttpGet("{id}")]
        public async Task<object> ChangeActiveStatus(string id)
        {
            return await _sublookmstr.ActiveStatusUpdate(id);
        }


        // DELETE api/<LookupMaster>/5
        [HttpGet("{id}")]
        public object Delete(string id)
        {
            return _sublookmstr.Delete(id);
        }
    }
}
