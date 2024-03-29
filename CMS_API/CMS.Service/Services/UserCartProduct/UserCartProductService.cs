﻿using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Services.ProductMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Service.Services.UserCartProduct
{


    public class UserCartProductService : BaseService, IUserCartProductService
    {
        private readonly DB_CMSContext _db;
        public UserCartProductService(DB_CMSContext db, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
        }

        public async Task<ServiceResponse<UserCartProductViewModel>> AddProduct(UserCartProductPostModel model)
        {
            ServiceResponse<UserCartProductViewModel> objResult = new ServiceResponse<UserCartProductViewModel>();

            try
            {
                TblUserCartList objProducts = await _db.TblUserCartLists.Where(x => x.ProductId == long.Parse(_security.DecryptData(model.ProductId)) && x.SizeId == long.Parse(_security.DecryptData(model.SizeId)) && x.UserId == _loginUserDetail.UserId).FirstOrDefaultAsync();
                if (objProducts == null)
                {
                    TblUserCartList objProduct = new TblUserCartList
                    {
                        ProductId = long.Parse(_security.DecryptData(model.ProductId)),
                        SizeId = long.Parse(_security.DecryptData(model.SizeId)),
                        Quantity = model.Quantity,
                        UserId = _loginUserDetail.UserId.Value,
                        AddedOn = DateTime.Now
                    };
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblUserCartList> product = await _db.TblUserCartLists.AddAsync(objProduct);
                    _ = await _db.SaveChangesAsync();
                    objProducts ??= new TblUserCartList();
                    objProducts = product.Entity;
                }
                else if (objProducts != null)
                {
                    objProducts.Quantity += model.Quantity;
                    _ = _db.TblUserCartLists.Update(objProducts);
                    _ = await _db.SaveChangesAsync();
                }
                objResult.Data = new UserCartProductViewModel
                {
                    Id = _security.EncryptData(objProducts.Id),
                    AddedOn = objProducts.AddedOn,
                    ProductId = _security.EncryptData(objProducts.ProductId),
                    Quantity = objProducts.Quantity,
                    SizeId = _security.EncryptData(objProducts.SizeId)

                };

                return CreateResponse<UserCartProductViewModel>(objResult.Data, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse<UserCartProductViewModel>> UpdateCartQuantity(UpdateCartItemPostModel model)
        {
            ServiceResponse<UserCartProductViewModel> objResult = new ServiceResponse<UserCartProductViewModel>();

            try
            {
                TblUserCartList objProducts = await _db.TblUserCartLists.Where(x => x.Id == long.Parse(_security.DecryptData(model.Id)) && x.UserId == _loginUserDetail.UserId).FirstOrDefaultAsync();
                if (objProducts != null)
                {
                    objProducts.Quantity = model.Quantity;
                    _ = _db.TblUserCartLists.Update(objProducts);
                    _ = await _db.SaveChangesAsync();
                    objResult.Data = new UserCartProductViewModel
                    {
                        Id = _security.EncryptData(objProducts.Id),
                        AddedOn = objProducts.AddedOn,
                        ProductId = _security.EncryptData(objProducts.ProductId),
                        Quantity = objProducts.Quantity,
                        SizeId = _security.EncryptData(objProducts.SizeId)

                    };
                    return CreateResponse(objResult.Data, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

                }
                else
                {
                    return CreateResponse<UserCartProductViewModel>(null, ResponseMessage.NotFound, false, (int)ApiStatusCode.NotFound);

                }


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

                List<TblUserCartList> objProduct = await _db.TblUserCartLists.Where(x => x.ProductId == long.Parse(_security.DecryptData(model.ProductId)) && x.UserId == _loginUserDetail.UserId && x.SizeId == long.Parse(_security.DecryptData(model.SizeId))).ToListAsync();
                _db.TblUserCartLists.RemoveRange(objProduct);
                _ = await _db.SaveChangesAsync();
                return CreateResponse<UserCartProductViewModel>(null, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse<IEnumerable<UserCartProductViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<UserCartProductViewModel>> objResult = new ServiceResponse<IEnumerable<UserCartProductViewModel>>();
            try
            {
                long userId = 0;
                if (model.AdvanceSearchModel != null && model.AdvanceSearchModel.Count > 0 && model.AdvanceSearchModel.ContainsKey("userId"))
                {
                    _ = model.AdvanceSearchModel.TryGetValue("userId", out object _userId);
                    userId = Convert.ToInt64(_userId.ToString());

                }


                IQueryable<TblUserCartList> result = from data in _db.TblUserCartLists.Include(x => x.Product).ThenInclude(x => x.TblProductStocks)

                                                     where (userId > 0 && data.UserId == userId) || (userId == 0 && data.UserId == _loginUserDetail.UserId)
                                                     select data;
                result = model.OrderBy switch
                {
                    "Name" => model.OrderByAsc ? (from orderData in result orderby orderData.Product.Name ascending select orderData) : (from orderData in result orderby orderData.Product.Name descending select orderData),
                    _ => model.OrderByAsc ? (from orderData in result orderby orderData.AddedOn ascending select orderData) : (from orderData in result orderby orderData.AddedOn descending select orderData),
                };
                objResult.TotalRecord = result.Count();
                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new UserCartProductViewModel
                                        {
                                            Id = _security.EncryptData(x.Id.ToString()),
                                            ProductId = _security.EncryptData(x.ProductId),
                                            SizeId = _security.EncryptData(x.SizeId),
                                            Size = x.Size.Name,
                                            Quantity = x.Quantity,
                                            AddedOn = x.AddedOn,
                                            Product = new ProductMasterViewModel
                                            {
                                                Id = _security.EncryptData(x.Product.Id.ToString()),
                                                Name = x.Product.Name,
                                                ImagePath = !string.IsNullOrEmpty(x.Product.ImagePath) ? x.Product.ImagePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Medium)) : null,
                                                CategoryId = _security.EncryptData(x.Product.CategoryId.ToString()),
                                                Category = x.Product.Category.Name,
                                                SubCategoryId = x.Product.SubCategoryId.HasValue ? _security.EncryptData(x.Product.SubCategoryId.ToString()) : null,
                                                SubCategory = x.Product.SubCategory.Name,
                                                CaptionTagId = x.Product.CaptionTagId.HasValue ? _security.EncryptData(x.Product.CaptionTagId.ToString()) : null,
                                                CaptionTag = x.Product.CaptionTag.Name,
                                                ViewSectionId = x.Product.ViewSectionId.HasValue ? _security.EncryptData(x.Product.ViewSectionId.ToString()) : null,
                                                ViewSection = x.Product.ViewSection.Name,
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
                                                SellingPrice = x.Product.SellingPrice != null && x.Product.SellingPrice.HasValue ? x.Product.SellingPrice : x.Product.Price,
                                                Discount = x.Product.Discount != null && x.Product.Discount.HasValue ? x.Product.Discount.Value : 0,

                                                Stocks = x.Product.TblProductStocks.Count > 0 ? x.Product.TblProductStocks.OrderBy(x => x.Size.SortedOrder).Select(st => new ProductStockModel
                                                {
                                                    Id = _security.EncryptData(st.Id),
                                                    ProductId = _security.EncryptData(x.Id),
                                                    SizeId = _security.EncryptData(st.SizeId),
                                                    Size = st.Size.Name,
                                                    UnitPrice = st.UnitPrice,
                                                    SellingPrice = st.SellingPrice != null && st.SellingPrice.HasValue ? st.SellingPrice : st.UnitPrice,
                                                    Discount = st.Discount != null && st.Discount.HasValue ? st.Discount.Value : 0,
                                                    Quantity = st.Quantity

                                                }).ToList() : null

                                            }
                                        }).ToListAsync();



                return result != null
                    ? CreateResponse(objResult.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok, TotalRecord: objResult.TotalRecord)
                    : CreateResponse<IEnumerable<UserCartProductViewModel>>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound, TotalRecord: 0);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }

    }
}
