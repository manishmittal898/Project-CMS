using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.GeneralEntry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.API.Areas.Admin.Controllers
{
 
    [Area("Admin")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class GeneralEntryCategoryController : ControllerBase
    {
        private readonly IGECategoryService _service;
        public GeneralEntryCategoryController(IGECategoryService Iservice)
        {
            _service = Iservice;
        }
        // GET: api/<GeneralEntryCategory>
        [HttpPost]
        public async Task<object> GetAsync(IndexModel model)
        {
            return await _service.GetList(model);
        }

        // GET api/<GeneralEntryCategory>/5
        [HttpGet("{id}")]
        public async Task<object> Get(long id)

        {
            return await _service.GetById(id);
        }

        // POST api/<GeneralEntryCategory>
        [HttpPost]
        public async Task<object> Save(GeneralEntryCategoryPostModel model)
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
        public async Task<object> ChangeActiveStatus(long id)
        {
            return await _service.ActiveStatusUpdate(id);
        }

        [HttpGet("{id}/{columnName}")]
        public async Task<object> ChangeFlagStatusUpdate(long id, string columnName)
        {
            return await _service.FlagStatusUpdate(id,columnName);
        }

 

        // DELETE api/<GeneralEntryCategory>/5
        [HttpGet("{id}")]
        public async Task<object> Delete(long id)
        {
            return await _service.Delete(id);
        }
    }
}
