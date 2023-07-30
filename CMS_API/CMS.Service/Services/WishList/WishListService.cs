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

        public async Task<ServiceResponse<WishListViewModel>> Add(WishListPostModel model)
        {
            ServiceResponse<WishListViewModel> objResult = new ServiceResponse<WishListViewModel>();

            try
            {
                TblUserWishList objProduct = new TblUserWishList();
                objProduct.ProductId = model.ProductId;
                objProduct.UserId = _loginUserDetail.UserId.Value;
                objProduct.AddedOn = DateTime.Now;

                var product = await _db.TblUserWishLists.AddAsync(objProduct);
                await _db.SaveChangesAsync();

                

                return CreateResponse<WishListViewModel>(null, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse<IEnumerable<WishListViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<WishListViewModel>> objResult = new ServiceResponse<IEnumerable<WishListViewModel>>();
            try
            {


                var result = (from data in _db.TblUserWishLists.Include(x => x.Product)
                              where data.UserId == _loginUserDetail.UserId
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
                                        select new WishListViewModel
                                        {
                                            Id = x.Id,
                                            ProductId = x.ProductId,
                                            AddedOn = x.AddedOn,
                                            Product = new ProductMasterViewModel
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
                                                ShippingCharge = x.Product.ShippingCharge ?? null

                                            }

                                        }).ToListAsync();



                if (result != null)
                {

                    return CreateResponse(objResult.Data as IEnumerable<WishListViewModel>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<WishListViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
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
