using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.ProductMaster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProductMasterController : ControllerBase
    {
        // GET: api/<ProductMasterController>
        private readonly IProductMasterService _productmstr;
        public ProductMasterController(IProductMasterService productmstr)
        {
            _productmstr = productmstr;
        }

        // GET: api/<ProductMasterController>
        // GET: api/<LookupMaster>
        [HttpPost]
        public async Task<object> GetList(IndexModel model)
        {
            return await _productmstr.GetList(model);
        }


        // GET api/<ProductMasterController>/5
        [HttpGet("{id}")]
        public object Get(string id)
        {
            return _productmstr.GetById(id);
        }


        // POST api/<ProductMasterController>
        [HttpPost]
        public async Task<object> Post(ProductMasterPostModel model)
        {
            if (ModelState.IsValid)
            {
                return await _productmstr.Save(model);

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
            return await _productmstr.ActiveStatusUpdate(id);
        }


        // DELETE api/<ProductMasterController>/5
        [HttpGet("{id}")]
        public async Task<object> Delete(string id)
        {
            return await _productmstr.Delete(id);
        }

        // DELETE api/<ProductMasterController>/5
        [HttpGet("{id}")]
        public async Task<object> DeleteProductFile(string id)
        {
            return await _productmstr.DeleteProductFile(id);
        }

        [HttpGet("{skuNumber}/{id}")]
        public async Task<object> IsSKUExist(string skuNumber, string id = null)
        {
            return await _productmstr.IsSKUExist(skuNumber, id);
        }
    }
}
