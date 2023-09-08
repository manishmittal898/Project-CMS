﻿using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Services.ProductMaster;
using CMS.Service.Services.UserCartProduct;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.UserCartProduct
{


    public class UserCartProductService : BaseService, IUserCartProductService
    {
        DB_CMSContext _db;
        public UserCartProductService(DB_CMSContext db, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
        }

        public async Task<ServiceResponse<UserCartProductViewModel>> AddProduct(UserCartProductPostModel model)
        {
            ServiceResponse<UserCartProductViewModel> objResult = new ServiceResponse<UserCartProductViewModel>();

            try
            {
                List<TblUserCartList> objProducts = await _db.TblUserCartLists.Where(x => x.ProductId == long.Parse(_security.DecryptData(model.ProductId)) && x.UserId == _loginUserDetail.UserId).ToListAsync();
                if (objProducts.Count == 0)
                {
                    TblUserCartList objProduct = new TblUserCartList();
                    objProduct.ProductId = long.Parse(_security.DecryptData(model.ProductId));
                    objProduct.UserId = _loginUserDetail.UserId.Value;
                    objProduct.AddedOn = DateTime.Now;
                    var product = await _db.TblUserCartLists.AddAsync(objProduct);
                    await _db.SaveChangesAsync();
                }
                else if (objProducts.Count > 1)
                {

                    _db.TblUserCartLists.RemoveRange(objProducts.SkipLast(1));
                    await _db.SaveChangesAsync();
                }




                return CreateResponse<UserCartProductViewModel>(null, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse<UserCartProductViewModel>> RemoveProduct(UserCartProductPostModel model)
        {
            ServiceResponse<UserCartProductViewModel> objResult = new ServiceResponse<UserCartProductViewModel>();

            try
            {

                List<TblUserCartList> objProduct = await _db.TblUserCartLists.Where(x => x.ProductId == long.Parse(_security.DecryptData(model.ProductId)) && x.UserId == _loginUserDetail.UserId).ToListAsync();
                _db.TblUserCartLists.RemoveRange(objProduct);
                await _db.SaveChangesAsync();



                return CreateResponse<UserCartProductViewModel>(null, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse<IEnumerable<ProductMasterViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<ProductMasterViewModel>> objResult = new ServiceResponse<IEnumerable<ProductMasterViewModel>>();
            try
            {
                long userId = 0;
                if (model.AdvanceSearchModel != null && model.AdvanceSearchModel.Count > 0 && model.AdvanceSearchModel.ContainsKey("userId"))
                {
                    model.AdvanceSearchModel.TryGetValue("userId", out object _userId);
                    userId = Convert.ToInt64(_userId.ToString());

                }


                var result = (from data in _db.TblUserCartLists.Include(x => x.Product)

                              where ((userId > 0 && data.UserId == userId) || (userId == 0 && data.UserId == _loginUserDetail.UserId))
                              select data);
                switch (model.OrderBy)
                {
                    case "Name":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Product.Name ascending select orderData) : (from orderData in result orderby orderData.Product.Name descending select orderData);
                        break;

                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.AddedOn ascending select orderData) : (from orderData in result orderby orderData.AddedOn descending select orderData);
                        break;
                }
                objResult.TotalRecord = result.Count();
                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new ProductMasterViewModel
                                        {
                                            Id = _security.EncryptData(x.Product.Id.ToString()),
                                            Name = x.Product.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.Product.ImagePath) ? x.Product.ImagePath.ToAbsolutePath() : null,
                                            CategoryId = _security.EncryptData(x.Product.CategoryId.ToString()),
                                            Category = x.Product.Category.Name,
                                            SubCategoryId = x.Product.SubCategoryId.HasValue ? _security.EncryptData(x.Product.SubCategoryId.ToString()) : null,
                                            SubCategory = x.Product.SubCategory.Name,
                                            CaptionTagId = x.Product.CaptionTagId.HasValue ? _security.EncryptData(x.Product.CaptionTagId.ToString()) : null,
                                            CaptionTag = x.Product.CaptionTag.Name,
                                            ViewSectionId = x.Product.ViewSectionId.HasValue ? _security.EncryptData(x.Product.ViewSectionId.ToString()) : null,
                                            ViewSection = x.Product.ViewSection.Name,
                                            DiscountId = x.Product.DiscountId.HasValue ? _security.EncryptData(x.Product.DiscountId.Value) : null,
                                            Discount = x.Product.Discount.Name,
                                            OccasionId = x.Product.OccasionId.HasValue ? _security.EncryptData(x.Product.OccasionId.Value) : null,
                                            Occasion = x.Product.Occasion.Name,
                                            FabricId = x.Product.FabricId.HasValue ? _security.EncryptData(x.Product.FabricId.Value) : null,
                                            Fabric = x.Product.Fabric.Name,
                                            LengthId = x.Product.LengthId.HasValue ? _security.EncryptData(x.Product.LengthId.Value) : null,
                                            Length = x.Product.Length.Name,
                                            ColorId = x.Product.ColorId.HasValue ? _security.EncryptData(x.Product.ColorId.Value) : null,
                                            Color = x.Product.Color.Name,
                                            PatternId = x.Product.PatternId.HasValue ? _security.EncryptData(x.Product.PatternId.Value) : null,
                                            Pattern = x.Product.Pattern.Name,
                                            UniqueId = x.Product.UniqueId,
                                            Desc = x.Product.Desc,
                                            Summary = x.Product.Desc,
                                            Price = x.Product.Price,
                                            SellingPrice = x.Product.DiscountId.HasValue ? Math.Round(x.Product.Price.Value - (x.Product.Price.Value * decimal.Parse(x.Product.Discount.Value)) / 100) : x.Product.Price,

                                            MetaTitle = x.Product.MetaTitle,
                                            MetaDesc = x.Product.MetaDesc,
                                            CreatedBy = x.Product.CreatedBy,
                                            CreatedOn = x.Product.CreatedOn,
                                            ModifiedBy = x.Product.ModifiedBy,
                                            ModifiedOn = x.Product.ModifiedOn,
                                            IsActive = x.Product.IsActive.Value,
                                            IsDelete = x.Product.IsDelete,
                                            Keyword = x.Product.Keyword,
                                            IsWhishList = true
                                        }).ToListAsync();



                if (result != null)
                {

                    return CreateResponse(objResult.Data as IEnumerable<ProductMasterViewModel>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<ProductMasterViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }
            }
            catch (Exception ex)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }

    }
}
