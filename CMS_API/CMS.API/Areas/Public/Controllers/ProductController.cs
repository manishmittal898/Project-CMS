using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.ProductMaster;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<object> GetList(ProductFilterModel model)
        {
            return await _productmstr.GetFilterList(model);
        }

        [HttpPost]
        public async Task<object> GetProductCategory(IndexModel model)
        {
            return await _productmstr.GetProductCategory(model);
        }

        // GET api/<ProductMasterController>/5
        [HttpGet("{id}")]
        public object Get(string id)
        {
            return _productmstr.GetById(id);
        }

    }
}
