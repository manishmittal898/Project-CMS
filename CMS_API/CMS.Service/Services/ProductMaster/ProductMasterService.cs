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


                var result = (from lkType in _db.TblProductMasters
                              where !lkType.IsDelete && (string.IsNullOrEmpty(model.Search) || lkType.Name.Contains(model.Search) || lkType.Category.Name.Contains(model.Search) || lkType.SubCategory.Name.Contains(model.Search) || lkType.CaptionTag.Name.Contains(model.Search))
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
                                            CaptionTagId = x.CaptionTagId,
                                            CaptionTag = x.CaptionTag.Name,
                                            Desc = x.Desc,
                                            Summary = x.Desc,
                                            Price = x.Price,
                                            CreatedBy = x.CreatedBy,
                                            CreatedOn = x.CreatedOn,
                                            ModifiedBy = x.ModifiedBy,
                                            ModifiedOn = x.ModifiedOn,
                                            IsActive = x.IsActive.Value,
                                            IsDelete = x.IsDelete,
                                            Keyword = x.Keyword,
                                            ShippingCharge = x.ShippingCharge ?? null

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
        public ServiceResponse<ProductMasterViewModel> GetById(long id)
        {
            ServiceResponse<ProductMasterViewModel> ObjResponse = new ServiceResponse<ProductMasterViewModel>();
            try
            {

                var detail = _db.TblProductMasters.Include(x => x.TblProductStocks).Select(x => new ProductMasterViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath() : null,
                    CategoryId = x.CategoryId,
                    Category = x.Category.Name,
                    SubCategoryId = x.SubCategoryId,
                    SubCategory = x.SubCategory.Name,
                    CaptionTagId = x.CaptionTagId,
                    CaptionTag = x.CaptionTag.Name,
                    Desc = x.Desc,
                    Summary = x.Summary,
                    Price = x.Price,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedOn = x.ModifiedOn,
                    IsActive = x.IsActive.Value,
                    IsDelete = x.IsDelete,
                    Keyword = x.Keyword,
                    ShippingCharge = x.ShippingCharge ?? null,

                    Stocks = x.TblProductStocks.Count > 0 ? x.TblProductStocks.Select(st => new ProductStockModel
                    {
                        Id = st.Id,
                        ProductId = st.ProductId,
                        SizeId = st.SizeId,
                        Size = st.Size.Name,
                        UnitPrice = st.UnitPrice,
                        Quantity = st.Quantity

                    }).ToList() : null



                }).FirstOrDefault(x => x.Id == id && x.IsActive.Value);
                var productFiles = GetProductFile(id).Result;
                if (productFiles.IsSuccess)
                {
                    detail.Files = productFiles.Data;
                }

                ObjResponse = CreateResponse(detail, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<ProductMasterViewModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblProductMaster>> Save(ProductMasterPostModel model)
        {
            try
            {
                TblProductMaster objProduct = new TblProductMaster();
                List<TblProductImage> productImages = new List<TblProductImage>();
                List<TblProductStock> productStock = new List<TblProductStock>();

                if (model.Id > 0)
                {
                    objProduct = _db.TblProductMasters.Include(x=>x.TblProductStocks).FirstOrDefault(r => r.Id == model.Id);

                    objProduct.Name = model.Name;
                    objProduct.CategoryId = model.CategoryId;
                    objProduct.SubCategoryId = model.SubCategoryId.HasValue ? model.SubCategoryId : null;
                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {

                        objProduct.ImagePath = !string.IsNullOrEmpty(objProduct.ImagePath) && model.ImagePath.Contains(objProduct.ImagePath.Replace("\\", "/")) ? objProduct.ImagePath : _fileHelper.Save(model.ImagePath, FilePaths.ProductImages_Main);
                    }
                    else
                    {
                        _fileHelper.Delete(objProduct.ImagePath);
                        objProduct.ImagePath = null;
                    }

                    objProduct.Desc = model.Desc;
                    objProduct.Price = model.Price;
                    objProduct.Summary = model.Summary;
                    objProduct.CaptionTagId = model.CaptionTagId;
                    objProduct.ModifiedBy = _loginUserDetail.UserId.Value;
                    objProduct.ShippingCharge = model.ShippingCharge ?? null;
                    objProduct.Keyword = !string.IsNullOrEmpty(model.Keyword) ? model.Keyword : model.Name;
                    var product = _db.TblProductMasters.Update(objProduct);

                    if (model.Stocks != null && model.Stocks.Count > 0)
                    {
                        var exIds = model.Stocks.Select(x => x.Id).ToArray();

                        var deleteStock = objProduct.TblProductStocks.Where(x => !exIds.Contains(x.Id)).ToList();
                        if (deleteStock.Count > 0)
                        {
                            _db.TblProductStocks.RemoveRange(deleteStock);
                            _db.SaveChanges();
                        }


                        List<TblProductStock> prStocks = new List<TblProductStock>();
                        model.Stocks.Where(wh => wh.Id > 0).ToList().ForEach(xs =>
                        {
                            TblProductStock prStock = objProduct.TblProductStocks.Where(p => p.Id == xs.Id).FirstOrDefault();
                            if (prStock != null)
                            {
                                prStock.SizeId = xs.SizeId;
                                prStock.UnitPrice = xs.UnitPrice;
                                prStock.Quantity = xs.Quantity;
                                prStocks.Add(prStock);
                            }


                        });
                        if (prStocks.Count > 0)
                        {
                            _db.TblProductStocks.UpdateRange(prStocks);
                            _db.SaveChanges();
                        }


                        productStock = model.Stocks.Where(x => x.Id == default || x.Id == 0).Select(x => new TblProductStock
                        {

                            ProductId = model.Id,
                            SizeId = x.SizeId,
                            UnitPrice = x.UnitPrice,
                            Quantity = x.Quantity

                        }).ToList();
                        if (productStock.Count > 0)
                        {
                            await _db.TblProductStocks.AddRangeAsync(productStock);
                            _db.SaveChanges();
                        }


                    }

                    _db.SaveChanges();
                    if (model.Files != null && model.Files.Count > 0)
                    {
                        string[] existingfilePaths = _db.TblProductImages.Where(x => x.ProductId == model.Id).Select(x => x.FilePath).ToArray();

                        model.Files = model.Files.FindAll(x => !existingfilePaths.Contains(x.Replace("\\", "/")));

                        productImages = model.Files.Select(x => new TblProductImage
                        {
                            FilePath = _fileHelper.Save(x, FilePaths.ProductImages_Gallery),
                            ProductId = product.Entity.Id,
                            CreatedBy = _loginUserDetail.UserId.Value,
                            ModifiedBy = _loginUserDetail.UserId.Value,
                            IsActive = true,
                            IsDeleted = false
                        }).ToList();
                        await _db.TblProductImages.AddRangeAsync(productImages);
                        _db.SaveChanges();
                    }
                    objProduct.TblProductStocks = null;
                    
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
                    objProduct.ShippingCharge = model.ShippingCharge ?? null;
                    objProduct.Keyword = !string.IsNullOrEmpty(model.Keyword) ? model.Keyword : model.Name;
                    objProduct.CreatedBy = _loginUserDetail.UserId.Value;
                    objProduct.ModifiedBy = _loginUserDetail.UserId.Value;
                    var product = await _db.TblProductMasters.AddAsync(objProduct);
                    _db.SaveChanges();

                    if (model.Files != null && model.Files.Count > 0)
                    {
                        productImages = model.Files.Select(x => new TblProductImage
                        {
                            FilePath = _fileHelper.Save(x, FilePaths.ProductImages_Gallery),
                            ProductId = product.Entity.Id,
                            CreatedBy = _loginUserDetail.UserId.Value,
                            ModifiedBy = _loginUserDetail.UserId.Value,
                            IsActive = true,
                            IsDeleted = false
                        }).ToList();
                        await _db.TblProductImages.AddRangeAsync(productImages);
                        _db.SaveChanges();

                    }

                    if (model.Stocks != null && model.Stocks.Count > 0)
                    {
                        productStock = model.Stocks.Select(x => new TblProductStock
                        {

                            ProductId = product.Entity.Id,
                            SizeId = x.SizeId,
                            UnitPrice = x.UnitPrice,
                            Quantity = x.Quantity

                        }).ToList();
                        await _db.TblProductStocks.AddRangeAsync(productStock);
                        _db.SaveChanges();

                    }
                    objProduct.TblProductStocks = null;
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
                TblProductMaster objProduct = new TblProductMaster();
                objProduct = _db.TblProductMasters.FirstOrDefault(r => r.Id == id);

                objProduct.IsActive = !objProduct.IsActive;
                objProduct.ModifiedBy = _loginUserDetail.UserId ?? objProduct.ModifiedBy;
                objProduct.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse(objProduct as TblProductMaster, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return null;

            }
        }
        public async Task<ServiceResponse<TblProductMaster>> Delete(long id)
        {
            try
            {
                TblProductMaster objProduct = new TblProductMaster();
                objProduct = _db.TblProductMasters.FirstOrDefault(r => r.Id == id);

                objProduct.IsDelete = (bool)true;
                objProduct.ModifiedBy = _loginUserDetail.UserId ?? objProduct.ModifiedBy;
                objProduct.ModifiedOn = DateTime.Now;
                _db.SaveChanges();
                return CreateResponse(objProduct as TblProductMaster, ResponseMessage.Delete, true, ((int)ApiStatusCode.Ok));

            }
            catch (Exception ex)
            {
                return CreateResponse<TblProductMaster>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError), ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblProductImage>> DeleteProductFile(long id)
        {
            try
            {
                TblProductImage objProductFile = _db.TblProductImages.FirstOrDefault(r => r.Id == id);

                if (objProductFile != null)
                {
                    objProductFile.IsDeleted = (bool)true;
                    objProductFile.ModifiedBy = _loginUserDetail.UserId ?? objProductFile.ModifiedBy;
                    objProductFile.ModifiedOn = DateTime.Now;
                    _db.TblProductImages.Remove(objProductFile);
                    _fileHelper.Delete(objProductFile.FilePath);
                    await _db.SaveChangesAsync();
                    return CreateResponse(objProductFile, ResponseMessage.Delete, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<TblProductImage>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));

                }


            }
            catch (Exception ex)
            {
                return CreateResponse<TblProductImage>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError), ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<List<ProductImageViewModel>>> GetProductFile(long productId)
        {
            ServiceResponse<List<ProductImageViewModel>> objResponse = new ServiceResponse<List<ProductImageViewModel>>();
            try
            {

                objResponse.Data = await _db.TblProductImages.Where(x => x.ProductId == productId).Select(x => new ProductImageViewModel
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    FilePath = !string.IsNullOrEmpty(x.FilePath) ? x.FilePath.ToAbsolutePath() : null,

                }).ToListAsync();
                objResponse = CreateResponse(objResponse.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                objResponse = CreateResponse<List<ProductImageViewModel>>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return objResponse;
        }

        public async Task<ServiceResponse<IEnumerable<ProductCategoryViewModel>>> GetProductCategory(IndexModel model)
        {
            ServiceResponse<IEnumerable<ProductCategoryViewModel>> objResult = new ServiceResponse<IEnumerable<ProductCategoryViewModel>>();
            try
            {


                var result = (from lkType in _db.TblProductMasters
                              where !lkType.IsDelete && (string.IsNullOrEmpty(model.Search) || lkType.Name.Contains(model.Search) || lkType.Category.Name.Contains(model.Search) || lkType.SubCategory.Name.Contains(model.Search) || lkType.CaptionTag.Name.Contains(model.Search))
                              select lkType.Category).Distinct();
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
                                        select new ProductCategoryViewModel
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath() : null,


                                        }).ToListAsync();

                if (objResult.Data != null)
                {

                    return CreateResponse(objResult.Data as IEnumerable<ProductCategoryViewModel>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: result.Count());
                }
                else
                {
                    return CreateResponse<IEnumerable<ProductCategoryViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
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
