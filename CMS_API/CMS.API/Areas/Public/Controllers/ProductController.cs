using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.ProductMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.API.Areas.Public.Controllers
{
    [Area("Public")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductMasterService _productmstr;
        public ProductController(IProductMasterService productmstr)
        {
            _productmstr = productmstr;
        }

        // GET: api/<UserController>
        [HttpPost]
        public async Task<object> GetList(IndexModel model)
        {
            return await _productmstr.GetList(model);
        } 

        [HttpPost]
        public async Task<object> GetProductCategory(IndexModel model)
        {
            return await _productmstr.GetProductCategory(model);
        }
    }
}
