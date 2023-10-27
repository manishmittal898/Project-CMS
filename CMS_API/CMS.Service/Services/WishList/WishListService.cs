using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Services.ProductMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Service.Services.WishList
{

    public class WishListService : BaseService, IWishListService
    {
        private readonly DB_CMSContext _db;
        public WishListService(DB_CMSContext db, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
        }

        public async Task<ServiceResponse<WishListViewModel>> AddProduct(WishListPostModel model)
        {
            ServiceResponse<WishListViewModel> objResult = new ServiceResponse<WishListViewModel>();

            try
            {
                List<TblUserWishList> objProducts = await _db.TblUserWishLists.Where(x => x.ProductId == long.Parse(_security.DecryptData(model.ProductId)) && x.UserId == _loginUserDetail.UserId).ToListAsync();
                if (objProducts.Count == 0)
                {
                    TblUserWishList objProduct = new TblUserWishList
                    {
                        ProductId = long.Parse(_security.DecryptData(model.ProductId)),
                        UserId = _loginUserDetail.UserId.Value,
                        AddedOn = DateTime.Now
                    };
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblUserWishList> product = await _db.TblUserWishLists.AddAsync(objProduct);
                    _ = await _db.SaveChangesAsync();
                }
                else if (objProducts.Count > 1)
                {

                    _db.TblUserWishLists.RemoveRange(objProducts.SkipLast(1));
                    _ = await _db.SaveChangesAsync();
                }




                return CreateResponse<WishListViewModel>(null, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse<WishListViewModel>> RemoveProduct(WishListPostModel model)
        {
            ServiceResponse<WishListViewModel> objResult = new ServiceResponse<WishListViewModel>();

            try
            {

                List<TblUserWishList> objProduct = await _db.TblUserWishLists.Where(x => x.ProductId == long.Parse(_security.DecryptData(model.ProductId)) && x.UserId == _loginUserDetail.UserId).ToListAsync();
                _db.TblUserWishLists.RemoveRange(objProduct);
                _ = await _db.SaveChangesAsync();



                return CreateResponse<WishListViewModel>(null, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

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
                    _ = model.AdvanceSearchModel.TryGetValue("userId", out object _userId);
                    userId = Convert.ToInt64(_userId.ToString());

                }


                IQueryable<TblUserWishList> result = from data in _db.TblUserWishLists.Include(x => x.Product)

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



                return result != null
                    ? CreateResponse(objResult.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok, TotalRecord: objResult.TotalRecord)
                    : CreateResponse<IEnumerable<ProductMasterViewModel>>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound, TotalRecord: 0);
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
