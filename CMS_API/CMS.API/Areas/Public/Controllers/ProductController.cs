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
        public ProductController(IProductMasterService productmstr) => _productmstr = productmstr;

        // GET: api/<UserController>
        [HttpPost]
        public async Task<object> GetList(ProductFilterModel model) => await _productmstr.GetFilterList(model);

        [HttpPost]
        public async Task<object> GetProductCategory(IndexModel model) => await _productmstr.GetProductCategory(model);

        // GET api/<ProductMasterController>/5
        [HttpGet("{id}/{isThumbnail}")]
        public object Get(string id, bool isThumbnail = false) => _productmstr.GetById(id, isThumbnail);


        // GET api/<ProductMasterController>/5
        [HttpGet("{id}/{sizeId}")]
        public object GetStockDetail(string id, string sizeId) => _productmstr.GetStockDetail(id, sizeId);


    }
}
