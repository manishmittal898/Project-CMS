using CMS.Core.ServiceHelper.Model;
using CMS.Service.Services.ProductReview;

using CMS.Service.Services.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS.API.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]/[action]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        // GET: api/<ProductReviewController>
        private readonly IProductReviewService _productrw;
        public ProductReviewController(IProductReviewService productrw)
        {
            _productrw = productrw;
        }
        // GET: api/<ProductReviewController>
        [HttpGet]
        public object Get()
        {
            return _productrw.GetList();
        }

        // GET api/<ProductReviewController>/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            return _productrw.GetById(id);
        }

        // POST api/<ProductReviewController>
        [HttpPost]
        public async Task<object> Post([FromBody] ProductReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _productrw.Save(model);

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

        // PUT api/<ProductReviewController>/5
        [HttpPut("{id}")]
        public async Task<object> Put(int id, [FromBody] ProductReviewViewModel model)
        {


            if (ModelState.IsValid)
            {


                return await _productrw.Edit(id, model);

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

        // DELETE api/<ProductReviewController>/5
        [HttpGet("{id}")]
        public void Delete(int id)
        {

            _productrw.Delete(id);



        }
    }
}
