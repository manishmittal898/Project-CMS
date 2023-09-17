﻿using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductReview
{
    public class ProductReviewService : BaseService, IProductReviewService
    {
        DB_CMSContext _db;
        public ProductReviewService(DB_CMSContext db, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
        }


        public ServiceResponse<IEnumerable<Data.Models.TblProductReview>> GetList(string Id)
        {
            ServiceResponse<IEnumerable<Data.Models.TblProductReview>> objResult = new ServiceResponse<IEnumerable<Data.Models.TblProductReview>>();
            try
            {
                var objData = _db.TblProductReviews.Where(x => string.IsNullOrEmpty(Id) || x.ProductId == long.Parse(_security.DecryptData(Id))).ToList();
                objResult = CreateResponse(objData as IEnumerable<Data.Models.TblProductReview>, "Success", true);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }
        public ServiceResponse<TblProductReview> GetById(string id)
        {
            ServiceResponse<TblProductReview> ObjResponse = new ServiceResponse<TblProductReview>();
            try
            {

                var detail = _db.TblProductReviews.FirstOrDefault(x => x.Id == long.Parse(_security.DecryptData(id)) && x.IsActive.Value);
                ObjResponse = CreateResponse(detail, "Success", true);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<TblProductReview>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblProductReview>> Save(ProductReviewViewModel model)
        {
            try
            {
                TblProductReview objRole = new TblProductReview();
                // objRole.RoleId = model.RoleId;
                objRole.Title = model.Title;
                objRole.ShortDescription = model.ShortDescription;
                objRole.Description = model.Description;
                objRole.Rating = model.Rating;
                objRole.ProductId = model.ProductId;
                objRole.IsActive = true;
                objRole.CreatedBy = model.CreatedBy;
                var roletype = await _db.TblProductReviews.AddAsync(objRole);
                _db.SaveChanges();
                return CreateResponse(objRole, "Added", true);


            }
            catch (Exception ex)
            {

                return CreateResponse<TblProductReview>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblProductReview>> Edit(string id, ProductReviewViewModel model)
        {
            try
            {
                TblProductReview objRole = new TblProductReview();

                objRole = _db.TblProductReviews.FirstOrDefault(r => r.Id == int.Parse(_security.DecryptData(id)));

                objRole.Title = model.Title;
                objRole.ShortDescription = model.ShortDescription;
                objRole.Description = model.Description;
                objRole.Rating = model.Rating;
                objRole.ProductId = model.ProductId;

                objRole.ModifiedBy = model.ModifiedBy;
                var roletype = _db.TblProductReviews.Update(objRole);
                _db.SaveChanges();


                return CreateResponse(objRole, "Updated", true);

            }
            catch (Exception ex)
            {

                return CreateResponse<TblProductReview>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }

        }


        public async Task<ServiceResponse<TblProductReview>> Delete(string id)
        {
            try
            {
                TblProductReview objRole = new TblProductReview();
                objRole = _db.TblProductReviews.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(id)));

                var roletype = _db.TblProductReviews.Remove(objRole);
                await _db.SaveChangesAsync();
                return CreateResponse(objRole, "Deleted", true);
            }
            catch (Exception)
            {

                return null;

            }


        }

    }
}
