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
    public class GeneralEntryController : ControllerBase
    {
        private readonly IGeneralEntryService _service;
        public GeneralEntryController(IGeneralEntryService Iservice)
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
        [HttpGet("{id}/{isEdit}")]
        public async Task<object> Get(long id, bool isEdit = false)

        {
            return await _service.GetById(id, isEdit);
        }

        // POST api/<GeneralEntryCategory>
        [HttpPost]
        public async Task<object> Save(GeneralEntryPostModel model)
        {
            ServiceResponse<object> objReturn = new ServiceResponse<object>();

            if (ModelState.IsValid)
            {
                return await _service.Save(model);
            }
            else
            {
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

        // DELETE api/<GeneralEntryCategory>/5
        [HttpGet("{id}")]
        public async Task<object> Delete(long id)
        {
            return await _service.Delete(id);
        }

        [HttpGet("{id}")]
        public async Task<object> DeleteGeneralEntryItems(long id)
        {
            return await _service.DeleteGeneralEntryItems(id);
        }

    }
}
