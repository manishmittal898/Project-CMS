using CMS.Core.ServiceHelper.Cache;
using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DropdownController : ControllerBase
    {

        private readonly ICommonService _common;
        private readonly ICacheService _cacheService;

        public DropdownController(ICommonService common, ICacheService cacheService)
        {
            _common = common;
            _cacheService = cacheService;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost("{isTransactionData}")]
        public async Task<ServiceResponse<Dictionary<string, object>>> GetDropDown(string[] key, bool isTransactionData = false)
        {
            string keyData = "GetDropDown" + string.Join("-", key);
            var cacheData = _cacheService.GetData<ServiceResponse<Dictionary<string, object>>>(keyData);
            if (cacheData != null)
            {
                return cacheData;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(1.0);
            cacheData = await _common.GetDropDown(key, isTransactionData);

            _cacheService.SetData(keyData, cacheData, expirationTime);
            return cacheData;

            // return await _common.GetDropDown(key, isTransactionData);
        }

        [HttpPost]
        public async Task<ServiceResponse<Dictionary<string, object>>> GetMultipleFilterDropDown(FilterDropDownPostModel[] model)
        {

            var cacheData = _cacheService.GetData<ServiceResponse<Dictionary<string, object>>>("GetMultipleFilterDropDown");
            if (cacheData != null)
            {
                return cacheData;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(1.0);
            cacheData = await _common.GetFilterDropDown(model);

            _cacheService.SetData("GetMultipleFilterDropDown", cacheData, expirationTime);
            return cacheData;

        }

        [HttpPost]
        public async Task<ServiceResponse<Dictionary<string, object>>> GetFilterDropDown(FilterDropDownPostModel model)
        {
            FilterDropDownPostModel[] obj = { model != null ? model : new FilterDropDownPostModel() };


            var cacheData = _cacheService.GetData<ServiceResponse<Dictionary<string, object>>>("GetFilterDropDown");
            if (cacheData != null)
            {
                return cacheData;
            }
            var expirationTime = DateTimeOffset.Now.AddMinutes(1.0);
            cacheData = await _common.GetFilterDropDown(obj);

            _cacheService.SetData("GetFilterDropDown", cacheData, expirationTime);
            return cacheData;
        }
    }
}
