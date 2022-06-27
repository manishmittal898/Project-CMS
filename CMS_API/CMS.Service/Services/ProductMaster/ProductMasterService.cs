using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductMaster
{
    public class ProductMasterService : BaseService, IProductMasterService
    {
        DB_CMSContext _db;
        private readonly FileHelper _fileHelper;
        public ProductMasterService(DB_CMSContext db, IHostingEnvironment environment)
        {
            _db = db;
            _fileHelper = new FileHelper(environment);
        }


        public async Task<ServiceResponse<IEnumerable<ProductMasterViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<ProductMasterViewModel>> objResult = new ServiceResponse<IEnumerable<ProductMasterViewModel>>();
            try
            {
                model.AdvanceSearchModel.TryGetValue("typeId", out object TypeId);

                var result = (from lkType in _db.TblProductMasters
                              where !lkType.IsDelete && (string.IsNullOrEmpty(model.Search) || lkType.Name.Contains(model.Search))
                              select lkType);
                switch (model.OrderBy)
                {
                    case "Name":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Name ascending select orderData) : (from orderData in result orderby orderData.Name descending select orderData);
                        break;
                    case "CreatedOn":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.ModifiedOn ascending select orderData) : (from orderData in result orderby orderData.ModifiedOn descending select orderData);
                        break;
                }

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new ProductMasterViewModel
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath() : null,
                                            CategoryId = x.CategoryId,
                                            Category = x.Category.Name,
                                            SubCategoryId = x.SubCategoryId,
                                            SubCategory = x.SubCategory.Name,
                                            Desc = x.Desc,
                                            Summary = x.Desc,
                                            Price = x.Price,
                                            CreatedBy = x.CreatedBy,
                                            CreatedOn = x.CreatedOn,
                                            ModifiedBy = x.ModifiedBy,
                                            ModifiedOn = x.ModifiedOn,
                                            IsActive = x.IsActive.Value,
                                            IsDelete = x.IsDelete

                                        }).ToListAsync();

                if (result != null)
                {

                    return CreateResponse(objResult.Data as IEnumerable<ProductMasterViewModel>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
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
        public ServiceResponse<TblProductMaster> GetById(int id)
        {
            ServiceResponse<TblProductMaster> ObjResponse = new ServiceResponse<TblProductMaster>();
            try
            {

                var detail = _db.TblProductMasters.FirstOrDefault(x => x.Id == id && x.IsActive.Value);
                ObjResponse = CreateResponse(detail, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<TblProductMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblProductMaster>> Save(ProductMasterPostModel model)
        {
            try
            {
                TblProductMaster objProduct = new TblProductMaster();
                if (model.Id > 0)
                {
                    objProduct = _db.TblProductMasters.FirstOrDefault(r => r.Id == model.Id);

                    objProduct.Name = model.Name;
                    objProduct.CategoryId = model.CategoryId;
                    objProduct.SubCategoryId = model.SubCategoryId;
                    objProduct.ImagePath = _fileHelper.Save(model.ImagePath, FilePaths.ProductImages_Main);
                    objProduct.Desc = model.Desc;
                    objProduct.Price = model.Price;
                    objProduct.Summary = model.Summary;
                    objProduct.CaptionTagId = model.CaptionTagId;

                    objProduct.ModifiedBy = model.ModifiedBy;
                    var roletype = _db.TblProductMasters.Update(objProduct);
                    _db.SaveChanges();
                    return CreateResponse<TblProductMaster>(objProduct, ResponseMessage.Update, true, (int)ApiStatusCode.Ok);
                }
                else
                {


                    objProduct.Name = model.Name;
                    objProduct.CategoryId = model.CategoryId;
                    objProduct.SubCategoryId = model.SubCategoryId;
                    objProduct.ImagePath = _fileHelper.Save(model.ImagePath, FilePaths.ProductImages_Main);
                    objProduct.Desc = model.Desc;
                    objProduct.Price = model.Price;
                    objProduct.Summary = model.Summary;
                    objProduct.CaptionTagId = model.CaptionTagId;
                    objProduct.IsActive = true;
                    objProduct.CreatedBy = model.CreatedBy;
                    var roletype = await _db.TblProductMasters.AddAsync(objProduct);
                    _db.SaveChanges();

                    return CreateResponse<TblProductMaster>(objProduct, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

                }

            }
            catch (Exception ex)
            {

                return CreateResponse<TblProductMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblProductMaster>> ActiveStatusUpdate(long id)
        {
            try
            {
                TblProductMaster objRole = new TblProductMaster();
                objRole = _db.TblProductMasters.FirstOrDefault(r => r.Id == id);

                objRole.IsActive = !objRole.IsActive;
                await _db.SaveChangesAsync();
                return CreateResponse(objRole as TblProductMaster, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return null;

            }
        }
        public async Task<ServiceResponse<TblProductMaster>> Delete(int id)
        {
            try
            {
                TblProductMaster objRole = new TblProductMaster();
                objRole = _db.TblProductMasters.FirstOrDefault(r => r.Id == id);
                objRole.IsDelete = true;

                var roletype = _db.TblProductMasters.Update(objRole);
                await _db.SaveChangesAsync();
                return CreateResponse(objRole, ResponseMessage.Update, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {
                return CreateResponse<TblProductMaster>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError), ex.Message.ToString());

            }


        }

    }
}
