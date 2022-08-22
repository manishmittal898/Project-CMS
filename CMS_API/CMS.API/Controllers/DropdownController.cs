using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DropdownController : ControllerBase
    {

        private readonly ICommonService _common;
        public DropdownController(ICommonService common)
        {
            _common = common;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost("{isTransactionData}")]
        public async Task<ServiceResponse<Dictionary<string, object>>> GetDropDown(string[] key, bool isTransactionData = false)
        {
            return await _common.GetDropDown(key, isTransactionData);
        }

        [HttpPost]
        public async Task<ServiceResponse<Dictionary<string, object>>> GetMultipleFilterDropDown(FilterDropDownPostModel[] model)
        {
            return await _common.GetFilterDropDown(model);
        }
        [HttpPost]
        public async Task<ServiceResponse<Dictionary<string, object>>> GetFilterDropDown(FilterDropDownPostModel model)
        {
            FilterDropDownPostModel[] obj = { model != null ? model : new FilterDropDownPostModel() };
            return await _common.GetFilterDropDown(obj);
        }
    }
}
