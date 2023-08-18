using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Services.ProductMaster;
using CMS.Service.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.WishList
{

    public class WishListService : BaseService, IWishListService
    {
        DB_CMSContext _db;
        public WishListService(DB_CMSContext db, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;

        }

        public async Task<ServiceResponse<WishListViewModel>> AddProduct(WishListPostModel model)
        {
            ServiceResponse<WishListViewModel> objResult = new ServiceResponse<WishListViewModel>();

            try
            {
                List<TblUserWishList> objProducts = await _db.TblUserWishLists.Where(x => x.ProductId == model.ProductId && x.UserId == _loginUserDetail.UserId).ToListAsync();
                if (objProducts.Count == 0)
                {
                    TblUserWishList objProduct = new TblUserWishList();
                    objProduct.ProductId = model.ProductId;
                    objProduct.UserId = _loginUserDetail.UserId.Value;
                    objProduct.AddedOn = DateTime.Now;
                    var product = await _db.TblUserWishLists.AddAsync(objProduct);
                    await _db.SaveChangesAsync();
                }
                else if (objProducts.Count > 1)
                {

                    _db.TblUserWishLists.RemoveRange(objProducts.SkipLast(1));
                    await _db.SaveChangesAsync();
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

                List<TblUserWishList> objProduct = await _db.TblUserWishLists.Where(x => x.ProductId == model.ProductId && x.UserId == _loginUserDetail.UserId).ToListAsync();
                _db.TblUserWishLists.RemoveRange(objProduct);
                await _db.SaveChangesAsync();



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
                    model.AdvanceSearchModel.TryGetValue("userId", out object _userId);
                    userId = Convert.ToInt64(_userId.ToString());

                }


                var result = (from data in _db.TblUserWishLists.Include(x => x.Product)

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
                                            Id = x.Product.Id,
                                            Name = x.Product.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.Product.ImagePath) ? x.Product.ImagePath.ToAbsolutePath() : null,
                                            CategoryId = x.Product.CategoryId,
                                            Category = x.Product.Category.Name,
                                            SubCategoryId = x.Product.SubCategoryId,
                                            SubCategory = x.Product.SubCategory.Name,
                                            CaptionTagId = x.Product.CaptionTagId,
                                            CaptionTag = x.Product.CaptionTag.Name,
                                            ViewSectionId = x.Product.ViewSectionId,
                                            ViewSection = x.Product.ViewSection.Name,
                                            Desc = x.Product.Desc,
                                            Summary = x.Product.Desc,
                                            Price = x.Product.Price,
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
