using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.ProductMaster;

using CMS.Service.Services.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductMasterController : ControllerBase
    {
        // GET: api/<ProductMasterController>
        private readonly IProductMasterService _productmstr;
        public ProductMasterController(IProductMasterService productmstr)
        {
            _productmstr = productmstr;
        }
        // GET: api/<ProductMasterController>
        [HttpGet]
        public object Get()
        {
            return _productmstr.GetList();
        }

        // GET api/<ProductMasterController>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return _productmstr.GetById(id);
        }

        // POST api/<ProductMasterController>
        [HttpPost]
        public async Task<object> Post([FromBody] ProductMasterViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _productmstr.Save(model);

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

        // PUT api/<ProductMasterController>/5
        [HttpPut("{id}")]
        public async Task<object> Put(int id, [FromBody] ProductMasterViewModel model)
        {


            if (ModelState.IsValid)
            {


                return await _productmstr.Edit(id, model);

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

        // DELETE api/<ProductMasterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

            _productmstr.Delete(id);



        }
    }
}
