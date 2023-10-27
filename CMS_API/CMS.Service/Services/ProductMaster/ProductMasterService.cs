using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductMaster
{
    public class ProductMasterService : BaseService, IProductMasterService
    {
        private readonly DB_CMSContext _db;
        private readonly FileHelper _fileHelper;
        public ProductMasterService(DB_CMSContext db, IHostingEnvironment environment, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
            _fileHelper = new FileHelper(environment);
        }

        public async Task<ServiceResponse<IEnumerable<ProductMasterViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<ProductMasterViewModel>> objResult = new ServiceResponse<IEnumerable<ProductMasterViewModel>>();
            try
            {
                IQueryable<TblProductMaster> result = from pdct in _db.TblProductMasters
                                                      where !pdct.IsDelete && (string.IsNullOrEmpty(model.Search) || pdct.Name.Contains(model.Search) || pdct.Category.Name.Contains(model.Search) || pdct.SubCategory.Name.Contains(model.Search) || pdct.CaptionTag.Name.Contains(model.Search) || pdct.UniqueId.Contains(model.Search))
                                                      select pdct;
                result = (object)model.OrderBy switch
                {
                    "Name" => model.OrderByAsc ? (from orderData in result orderby orderData.Name ascending select orderData) : (from orderData in result orderby orderData.Name descending select orderData),
                    "CreatedOn" => model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData),
                    _ => model.OrderByAsc ? (from orderData in result orderby orderData.ModifiedOn ascending select orderData) : (from orderData in result orderby orderData.ModifiedOn descending select orderData),
                };
                objResult.TotalRecord = result.Count();
                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new ProductMasterViewModel
                                        {
                                            Id = _security.EncryptData(x.Id),
                                            Name = x.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Medium)) : null,
                                            CategoryId = _security.EncryptData(x.CategoryId),
                                            Category = x.Category.Name,
                                            SubCategoryId = x.SubCategoryId.HasValue ? _security.EncryptData(x.SubCategoryId.Value) : null,
                                            SubCategory = x.SubCategory.Name,
                                            CaptionTagId = x.CaptionTagId.HasValue ? _security.EncryptData(x.CaptionTagId.Value) : null,
                                            CaptionTag = x.CaptionTag.Name,
                                            ViewSectionId = x.ViewSectionId.HasValue ? _security.EncryptData(x.ViewSectionId.Value) : null,
                                            ViewSection = x.ViewSection.Name,
                                            OccasionId = x.OccasionId.HasValue ? _security.EncryptData(x.OccasionId.Value) : null,
                                            Occasion = x.Occasion.Name,
                                            FabricId = x.FabricId.HasValue ? _security.EncryptData(x.FabricId.Value) : null,
                                            Fabric = x.Fabric.Name,
                                            LengthId = x.LengthId.HasValue ? _security.EncryptData(x.LengthId.Value) : null,
                                            Length = x.Length.Name,
                                            ColorId = x.ColorId.HasValue ? _security.EncryptData(x.ColorId.Value) : null,
                                            Color = x.Color.Name,
                                            PatternId = x.PatternId.HasValue ? _security.EncryptData(x.PatternId.Value) : null,
                                            Pattern = x.Pattern.Name,
                                            UniqueId = x.UniqueId,
                                            Desc = x.Desc,
                                            Summary = x.Desc,
                                            Price = x.Price,
                                            SellingPrice = x.SellingPrice != null && x.SellingPrice.HasValue ? x.SellingPrice : x.Price,
                                            Discount = x.Discount != null && x.Discount.HasValue ? x.Discount.Value : 0,
                                            MetaTitle = x.MetaTitle,
                                            MetaDesc = x.MetaDesc,
                                            CreatedBy = x.CreatedBy,
                                            CreatedOn = x.CreatedOn,
                                            ModifiedBy = x.ModifiedBy,
                                            ModifiedOn = x.ModifiedOn,
                                            IsActive = x.IsActive.Value,
                                            IsDelete = x.IsDelete,
                                            Keyword = x.Keyword,
                                            IsWhishList = _loginUserDetail != null && x.TblUserWishLists.Count(x => x.UserId == _loginUserDetail.UserId && x.ProductId == x.Id) > 0,

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
        public ServiceResponse<ProductMasterViewModel> GetById(string id, bool isThumbnail = false)
        {
            ServiceResponse<ProductMasterViewModel> ObjResponse = new ServiceResponse<ProductMasterViewModel>();
            try
            {

                ProductMasterViewModel detail = _db.TblProductMasters.Include(x => x.TblProductStocks).Where(x => x.Id == long.Parse(_security.DecryptData(id)) && !x.IsDelete).Select(x => new ProductMasterViewModel
                {
                    Id = _security.EncryptData(x.Id.ToString()),
                    Name = x.Name,
                    ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? !isThumbnail ? x.ImagePath.ToAbsolutePath() : x.ImagePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Medium)) : null,
                    CategoryId = _security.EncryptData(x.CategoryId),
                    Category = x.Category.Name,
                    SubCategoryId = x.SubCategoryId.HasValue ? _security.EncryptData(x.SubCategoryId.Value) : null,
                    SubCategory = x.SubCategory.Name,
                    CaptionTagId = x.CaptionTagId.HasValue ? _security.EncryptData(x.CaptionTagId.Value) : null,
                    CaptionTag = x.CaptionTag.Name,
                    ViewSectionId = x.ViewSectionId.HasValue ? _security.EncryptData(x.ViewSectionId.Value) : null,
                    ViewSection = x.ViewSection.Name,
                    OccasionId = x.OccasionId.HasValue ? _security.EncryptData(x.OccasionId.Value) : null,
                    Occasion = x.Occasion.Name,
                    FabricId = x.FabricId.HasValue ? _security.EncryptData(x.FabricId.Value) : null,
                    Fabric = x.Fabric.Name,
                    LengthId = x.LengthId.HasValue ? _security.EncryptData(x.LengthId.Value) : null,
                    Length = x.Length.Name,
                    ColorId = x.ColorId.HasValue ? _security.EncryptData(x.ColorId.Value) : null,
                    Color = x.Color.Name,
                    PatternId = x.PatternId.HasValue ? _security.EncryptData(x.PatternId.Value) : null,
                    Pattern = x.Pattern.Name,
                    UniqueId = x.UniqueId,
                    Desc = x.Desc,
                    Summary = x.Summary,
                    Price = x.Price,
                    SellingPrice = x.SellingPrice != null && x.SellingPrice.HasValue ? x.SellingPrice : x.Price,
                    Discount = x.Discount != null && x.Discount.HasValue ? x.Discount.Value : 0,
                    MetaTitle = x.MetaTitle,
                    MetaDesc = x.MetaDesc,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedOn = x.ModifiedOn,
                    IsActive = x.IsActive.Value,
                    IsDelete = x.IsDelete,
                    Keyword = x.Keyword,
                    IsWhishList = x.TblUserWishLists.Count > 0 && _loginUserDetail != null && x.TblUserWishLists.Any(y => y.ProductId == x.Id && y.UserId == _loginUserDetail.UserId),

                    Stocks = x.TblProductStocks.Count > 0 ? x.TblProductStocks.OrderBy(x => x.Size.SortedOrder).Select(st => new ProductStockModel
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
                }).FirstOrDefault();
                if (detail != null)
                {
                    ServiceResponse<List<ProductImageViewModel>> productFiles = GetProductFile(id).Result;
                    if (productFiles.IsSuccess)
                    {
                        detail.Files = productFiles.Data;
                    }
                }


                ObjResponse = CreateResponse(detail, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<ProductMasterViewModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public ServiceResponse<ProductStockModel> GetStockDetail(string productId, string sizeId)
        {
            ServiceResponse<ProductStockModel> ObjResponse = new ServiceResponse<ProductStockModel>();
            try
            {

                ProductStockModel detail = _db.TblProductStocks.Where(x => x.ProductId == long.Parse(_security.DecryptData(productId)) && x.SizeId == long.Parse(_security.DecryptData(sizeId))).Select(st => new ProductStockModel
                {
                    Id = _security.EncryptData(st.Id),
                    ProductId = _security.EncryptData(st.ProductId),
                    SizeId = _security.EncryptData(st.SizeId),
                    Size = st.Size.Name,
                    UnitPrice = st.UnitPrice,
                    SellingPrice = st.SellingPrice != null && st.SellingPrice.HasValue ? st.SellingPrice : st.UnitPrice,
                    Discount = st.Discount != null && st.Discount.HasValue ? st.Discount.Value : 0,
                    Quantity = st.Quantity
                }).FirstOrDefault();
                ObjResponse = CreateResponse(detail, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<ProductStockModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<object>> Save(ProductMasterPostModel model)
        {
            try
            {
                TblProductMaster objProduct = new TblProductMaster();
                List<TblProductImage> productImages = new List<TblProductImage>();
                List<TblProductStock> productStock = new List<TblProductStock>();

                if (!string.IsNullOrEmpty(model.Id))
                {
                    objProduct = _db.TblProductMasters.Include(x => x.TblProductStocks).FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(model.Id)));
                    objProduct.Name = model.Name;
                    objProduct.CategoryId = long.Parse(_security.DecryptData(model.CategoryId));
                    objProduct.SubCategoryId = !string.IsNullOrEmpty(model.SubCategoryId) ? long.Parse(_security.DecryptData(model.SubCategoryId)) : null as Nullable<long>;
                    objProduct.ViewSectionId = !string.IsNullOrEmpty(model.ViewSectionId) ? long.Parse(_security.DecryptData(model.ViewSectionId)) : null as Nullable<long>;
                    objProduct.CaptionTagId = !string.IsNullOrEmpty(model.CaptionTagId) ? long.Parse(_security.DecryptData(model.CaptionTagId)) : null as Nullable<long>;
                    objProduct.OccasionId = !string.IsNullOrEmpty(model.OccasionId) ? long.Parse(_security.DecryptData(model.OccasionId)) : null as Nullable<long>;
                    objProduct.FabricId = !string.IsNullOrEmpty(model.FabricId) ? long.Parse(_security.DecryptData(model.FabricId)) : null as Nullable<long>;
                    objProduct.LengthId = !string.IsNullOrEmpty(model.LengthId) ? long.Parse(_security.DecryptData(model.LengthId)) : null as Nullable<long>;
                    objProduct.ColorId = !string.IsNullOrEmpty(model.ColorId) ? long.Parse(_security.DecryptData(model.ColorId)) : null as Nullable<long>;
                    objProduct.PatternId = !string.IsNullOrEmpty(model.PatternId) ? long.Parse(_security.DecryptData(model.PatternId)) : null as Nullable<long>;
                    objProduct.UniqueId = model.UniqueId;
                    objProduct.Desc = model.Desc;
                    objProduct.Price = model.Price;
                    objProduct.SellingPrice = model.SellingPrice != null && model.SellingPrice.HasValue ? model.SellingPrice : model.Price;
                    objProduct.Discount = getDiscount(objProduct.Price.Value, objProduct.SellingPrice.Value);
                    objProduct.Summary = model.Summary;

                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {
                        objProduct.ImagePath = !string.IsNullOrEmpty(objProduct.ImagePath) && model.ImagePath.Contains(objProduct.ImagePath.Replace("\\", "/")) ? objProduct.ImagePath : await _fileHelper.Save(model.ImagePath, FilePaths.ProductImages_Main, isThumbnail: true);
                    }
                    else
                    {
                        _ = _fileHelper.Delete(objProduct.ImagePath);
                        objProduct.ImagePath = null;
                    }
                    objProduct.ModifiedBy = _loginUserDetail.UserId.Value;
                    objProduct.Keyword = !string.IsNullOrEmpty(model.Keyword) ? model.Keyword : model.Name;
                    objProduct.MetaTitle = model.MetaTitle;
                    objProduct.MetaDesc = model.MetaDesc;
                    objProduct.ModifiedOn = DateTime.Now;

                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblProductMaster> product = _db.TblProductMasters.Update(objProduct);

                    if (model.Stocks != null && model.Stocks.Count > 0)
                    {
                        long[] exIds = model.Stocks.Where(x => !string.IsNullOrEmpty(x.Id)).Select(x => long.Parse(_security.DecryptData(x.Id))).ToArray();

                        List<TblProductStock> deleteStock = objProduct.TblProductStocks.Where(x => !exIds.Contains(x.Id)).ToList();
                        if (deleteStock.Count > 0)
                        {
                            _db.TblProductStocks.RemoveRange(deleteStock);
                            _ = _db.SaveChanges();
                        }


                        List<TblProductStock> prStocks = new List<TblProductStock>();
                        model.Stocks.Where(wh => !string.IsNullOrEmpty(wh.Id) && long.Parse(_security.DecryptData(wh.Id)) > 0).ToList().ForEach(xs =>
                        {
                            TblProductStock prStock = objProduct.TblProductStocks.Where(p => p.Id == long.Parse(_security.DecryptData(xs.Id))).FirstOrDefault();
                            if (prStock != null)
                            {
                                prStock.SizeId = long.Parse(_security.DecryptData(xs.SizeId));
                                prStock.UnitPrice = xs.UnitPrice;
                                prStock.SellingPrice = xs.SellingPrice != null && xs.SellingPrice.HasValue ? xs.SellingPrice : model.SellingPrice;
                                prStock.Discount = getDiscount(prStock.UnitPrice.Value, prStock.SellingPrice.Value);
                                prStock.Quantity = xs.Quantity;
                                prStocks.Add(prStock);
                            }
                        });
                        if (prStocks.Count > 0)
                        {
                            _db.TblProductStocks.UpdateRange(prStocks);
                            _ = _db.SaveChanges();
                        }


                        productStock = model.Stocks.Where(x => x.Id == default || long.Parse(_security.DecryptData(x.Id)) == 0).Select(x => new TblProductStock
                        {

                            ProductId = long.Parse(_security.DecryptData(model.Id)),
                            SizeId = long.Parse(_security.DecryptData(x.SizeId)),
                            UnitPrice = x.UnitPrice,
                            SellingPrice = x.SellingPrice != null && x.SellingPrice.HasValue ? x.SellingPrice : x.UnitPrice,
                            Discount = getDiscount(x.UnitPrice.Value, x.SellingPrice != null && x.SellingPrice.HasValue ? x.SellingPrice.Value : x.UnitPrice.Value),
                            Quantity = x.Quantity

                        }).ToList();
                        if (productStock.Count > 0)
                        {
                            await _db.TblProductStocks.AddRangeAsync(productStock);
                            _ = _db.SaveChanges();
                        }


                    }

                    _ = _db.SaveChanges();
                    if (model.Files != null && model.Files.Count > 0)
                    {
                        string[] existingfilePaths = _db.TblProductImages.Where(x => x.ProductId == long.Parse(_security.DecryptData(model.Id))).Select(x => x.FilePath).ToArray();

                        model.Files = model.Files.FindAll(x => x != null && !existingfilePaths.Contains(x.Replace("\\", "/")));

                        productImages = model.Files.Where(x => !string.IsNullOrEmpty(x)).Select(x => new TblProductImage
                        {
                            FilePath = _fileHelper.Save(x, FilePaths.ProductImages_Gallery, isThumbnail: true).Result,
                            ProductId = product.Entity.Id,
                            CreatedBy = _loginUserDetail.UserId.Value,
                            ModifiedBy = _loginUserDetail.UserId.Value,
                            IsActive = true,
                            IsDeleted = false
                        }).ToList();
                        await _db.TblProductImages.AddRangeAsync(productImages);
                        _ = _db.SaveChanges();
                    }
                    objProduct.TblProductStocks = null;

                    return CreateResponse((object)_security.EncryptData(product.Entity.Id), ResponseMessage.Update, true, (int)ApiStatusCode.Ok);
                }
                else
                {
                    objProduct.Name = model.Name;
                    objProduct.CategoryId = long.Parse(_security.DecryptData(model.CategoryId));
                    objProduct.SubCategoryId = !string.IsNullOrEmpty(model.SubCategoryId) ? long.Parse(_security.DecryptData(model.SubCategoryId)) : null as Nullable<long>;
                    objProduct.ViewSectionId = !string.IsNullOrEmpty(model.ViewSectionId) ? long.Parse(_security.DecryptData(model.ViewSectionId)) : null as Nullable<long>;
                    objProduct.CaptionTagId = !string.IsNullOrEmpty(model.CaptionTagId) ? long.Parse(_security.DecryptData(model.CaptionTagId)) : null as Nullable<long>;
                    objProduct.OccasionId = !string.IsNullOrEmpty(model.OccasionId) ? long.Parse(_security.DecryptData(model.OccasionId)) : null as Nullable<long>;
                    objProduct.FabricId = !string.IsNullOrEmpty(model.FabricId) ? long.Parse(_security.DecryptData(model.FabricId)) : null as Nullable<long>;
                    objProduct.LengthId = !string.IsNullOrEmpty(model.LengthId) ? long.Parse(_security.DecryptData(model.LengthId)) : null as Nullable<long>;
                    objProduct.ColorId = !string.IsNullOrEmpty(model.ColorId) ? long.Parse(_security.DecryptData(model.ColorId)) : null as Nullable<long>;
                    objProduct.PatternId = !string.IsNullOrEmpty(model.PatternId) ? long.Parse(_security.DecryptData(model.PatternId)) : null as Nullable<long>;
                    objProduct.UniqueId = model.UniqueId;
                    objProduct.ImagePath = !string.IsNullOrEmpty(model.ImagePath) ? await _fileHelper.Save(model.ImagePath, FilePaths.ProductImages_Main, isThumbnail: true) : null;
                    objProduct.Desc = model.Desc;
                    objProduct.Price = model.Price;
                    objProduct.SellingPrice = model.SellingPrice != null && model.SellingPrice.HasValue ? model.SellingPrice : model.Price;
                    objProduct.Discount = getDiscount(objProduct.Price.Value, objProduct.SellingPrice.Value);
                    objProduct.Summary = model.Summary;
                    objProduct.IsActive = true;
                    objProduct.Keyword = !string.IsNullOrEmpty(model.Keyword) ? model.Keyword : model.Name;
                    objProduct.CreatedBy = _loginUserDetail.UserId.Value;
                    objProduct.ModifiedBy = _loginUserDetail.UserId.Value;
                    objProduct.MetaTitle = model.MetaTitle;
                    objProduct.MetaDesc = model.MetaDesc;
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblProductMaster> product = await _db.TblProductMasters.AddAsync(objProduct);
                    _ = _db.SaveChanges();

                    if (model.Files != null && model.Files.Count > 0)
                    {
                        productImages = model.Files.Where(x => !string.IsNullOrEmpty(x)).Select(x => new TblProductImage
                        {
                            FilePath = _fileHelper.Save(x, FilePaths.ProductImages_Gallery, isThumbnail: true).Result,
                            ProductId = product.Entity.Id,
                            CreatedBy = _loginUserDetail.UserId.Value,
                            ModifiedBy = _loginUserDetail.UserId.Value,
                            IsActive = true,
                            IsDeleted = false
                        }).ToList();
                        await _db.TblProductImages.AddRangeAsync(productImages);
                        _ = _db.SaveChanges();

                    }

                    if (model.Stocks != null && model.Stocks.Count > 0)
                    {
                        productStock = model.Stocks.Select(x => new TblProductStock
                        {

                            ProductId = product.Entity.Id,
                            SizeId = long.Parse(_security.DecryptData(x.SizeId)),
                            UnitPrice = x.UnitPrice,
                            SellingPrice = x.SellingPrice != null && x.SellingPrice.HasValue ? x.SellingPrice : x.UnitPrice,
                            Discount = getDiscount(x.UnitPrice.Value, x.SellingPrice != null && x.SellingPrice.HasValue ? x.SellingPrice.Value : x.UnitPrice.Value),
                            Quantity = x.Quantity

                        }).ToList();
                        await _db.TblProductStocks.AddRangeAsync(productStock);
                        _ = _db.SaveChanges();
                    }

                    objProduct.TblProductStocks = null;

                    return CreateResponse((object)_security.EncryptData(product.Entity.Id), ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

                }

            }
            catch (Exception ex)
            {

                return CreateResponse<object>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblProductMaster>> ActiveStatusUpdate(string id)
        {
            try
            {
                TblProductMaster objProduct = new TblProductMaster();
                objProduct = _db.TblProductMasters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(id)));

                objProduct.IsActive = !objProduct.IsActive;
                objProduct.ModifiedBy = _loginUserDetail.UserId ?? objProduct.ModifiedBy;
                objProduct.ModifiedOn = DateTime.Now;
                _ = await _db.SaveChangesAsync();
                return CreateResponse(objProduct, ResponseMessage.Update, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                return CreateResponse<TblProductMaster>(null, ResponseMessage.Fail, true, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }
        public async Task<ServiceResponse<TblProductMaster>> Delete(string id)
        {
            try
            {
                TblProductMaster objProduct = new TblProductMaster();
                objProduct = await _db.TblProductMasters.FirstOrDefaultAsync(r => r.Id == long.Parse(_security.DecryptData(id)));

                objProduct.IsDelete = true;
                objProduct.ModifiedBy = _loginUserDetail.UserId ?? objProduct.ModifiedBy;
                objProduct.ModifiedOn = DateTime.Now;
                _ = await _db.SaveChangesAsync();
                return CreateResponse(objProduct, ResponseMessage.Delete, true, (int)ApiStatusCode.Ok);

            }
            catch (Exception ex)
            {
                return CreateResponse<TblProductMaster>(null, ResponseMessage.Fail, true, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<object>> DeleteProductFile(string id)
        {
            try
            {
                List<TblProductImage> objProductFile = _db.TblProductImages.Where(r => r.Id == long.Parse(_security.DecryptData(id)) || string.IsNullOrEmpty(r.FilePath)).ToList();

                objProductFile.ForEach(x =>
                {
                    _ = _fileHelper.Delete(x.FilePath);
                });
                if (objProductFile != null)
                {

                    _db.TblProductImages.RemoveRange(objProductFile);

                    //add for thumbnail
                    _ = await _db.SaveChangesAsync();
                    return CreateResponse(true as object, ResponseMessage.Delete, true, (int)ApiStatusCode.Ok);
                }
                else
                {
                    return CreateResponse(null as object, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound);

                }


            }
            catch (Exception ex)
            {
                return CreateResponse(null as object, ResponseMessage.Fail, true, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<List<ProductImageViewModel>>> GetProductFile(string productId)
        {
            ServiceResponse<List<ProductImageViewModel>> objResponse = new ServiceResponse<List<ProductImageViewModel>>();
            try
            {

                objResponse.Data = await _db.TblProductImages.Where(x => x.ProductId == long.Parse(_security.DecryptData(productId)) && x.IsActive.Value && !x.IsDeleted && !string.IsNullOrEmpty(x.FilePath)).Select(x => new ProductImageViewModel
                {
                    Id = _security.EncryptData(x.Id),
                    ProductId = productId,
                    FilePath = !string.IsNullOrEmpty(x.FilePath) ? x.FilePath.ToAbsolutePath() : null,
                    ThumbnailPath = !string.IsNullOrEmpty(x.FilePath) ? x.FilePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Small)) : null,
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


                IQueryable<TblLookupMaster> result = (from lkType in _db.TblProductMasters
                                                      where !lkType.IsDelete && lkType.IsActive.Value && !lkType.Category.IsDelete && lkType.Category.IsActive == true && (string.IsNullOrEmpty(model.Search) || lkType.Name.Contains(model.Search) || lkType.Category.Name.Contains(model.Search) || lkType.SubCategory.Name.Contains(model.Search) || lkType.CaptionTag.Name.Contains(model.Search))
                                                      select lkType.Category).Distinct();
                result = (object)model.OrderBy switch
                {
                    "Name" => model.OrderByAsc ? (from orderData in result orderby orderData.Name ascending select orderData) : (from orderData in result orderby orderData.Name descending select orderData),
                    "CreatedOn" => model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData),
                    _ => model.OrderByAsc ? (from orderData in result orderby orderData.ModifiedOn ascending select orderData) : (from orderData in result orderby orderData.ModifiedOn descending select orderData),
                };
                objResult.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new ProductCategoryViewModel
                                        {
                                            Id = _security.EncryptData(x.Id),
                                            Name = x.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Medium)) : null,


                                        }).ToListAsync();

                return objResult.Data != null
                        ? CreateResponse(objResult.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok, TotalRecord: objResult.TotalRecord)
                        : CreateResponse<IEnumerable<ProductCategoryViewModel>>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound, TotalRecord: 0);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }

        public async Task<ServiceResponse<IEnumerable<ProductMasterViewModel>>> GetFilterList(ProductFilterModel model)
        {
            ServiceResponse<IEnumerable<ProductMasterViewModel>> objResult = new ServiceResponse<IEnumerable<ProductMasterViewModel>>();
            try
            {
                List<long> Ids = model.Ids != null && model.Ids.Count > 0 ? model.Ids.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> CategoryIds = model.CategoryId != null && model.CategoryId.Count > 0 ? model.CategoryId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> SubCategoryIds = model.SubCategoryId != null && model.SubCategoryId.Count > 0 ? model.SubCategoryId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> SizeIds = model.SizeId != null && model.SizeId.Count > 0 ? model.SizeId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> ViewSectionIds = model.ViewSectionId != null && model.ViewSectionId.Count > 0 ? model.ViewSectionId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> CaptionTagIds = model.CaptionTagId != null && model.CaptionTagId.Count > 0 ? model.CaptionTagId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> OccasionIds = model.OccasionId != null && model.OccasionId.Count > 0 ? model.OccasionId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> FabricIds = model.FabricId != null && model.FabricId.Count > 0 ? model.FabricId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> LengthIds = model.LengthId != null && model.LengthId.Count > 0 ? model.LengthId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> ColorIds = model.ColorId != null && model.ColorId.Count > 0 ? model.ColorId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> PatternIds = model.PatternId != null && model.PatternId.Count > 0 ? model.PatternId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                long Discount = 0;
                if (!string.IsNullOrEmpty(model.DiscountId) && model.DiscountId.Length > 0)
                {
                    TblLookupMaster lk = _db.TblLookupMasters.Where(x => x.Id == long.Parse(_security.DecryptData(model.DiscountId))).FirstOrDefault();
                    if (lk != null)
                    {
                        Discount = long.Parse(lk.Value);
                    }

                }
                IQueryable<TblProductMaster> result = from prd in _db.TblProductMasters.Include(x => x.TblUserWishLists)
                                                      where !prd.IsDelete && prd.IsActive.Value && (string.IsNullOrEmpty(model.Search) || prd.Name.Contains(model.Search) || prd.Category.Name.Contains(model.Search) || prd.SubCategory.Name.Contains(model.Search) || prd.CaptionTag.Name.Contains(model.Search))
                                                      && (string.IsNullOrEmpty(model.Keyword) || model.Keyword.Contains(prd.Keyword) || string.IsNullOrEmpty(model.Keyword) || prd.Name.Contains(model.Keyword) || prd.Category.Name.Contains(model.Keyword) || prd.SubCategory.Name.Contains(model.Keyword) || prd.CaptionTag.Name.Contains(model.Keyword))
                                                        && (model.CategoryId == null || model.CategoryId.Count == 0 || CategoryIds.Contains(prd.CategoryId))
                                                         && (model.SubCategoryId == null || model.SubCategoryId.Count == 0 || SubCategoryIds.Contains(prd.SubCategoryId.Value))
                                                         && (model.SizeId == null || model.SizeId.Count == 0 || prd.TblProductStocks.Any(x => SizeIds.Contains(x.SizeId)))
                                                         && (model.ViewSectionId == null || model.ViewSectionId.Count == 0 || ViewSectionIds.Contains(prd.ViewSectionId.Value))
                                                         && (model.CaptionTagId == null || model.CaptionTagId.Count == 0 || CaptionTagIds.Contains(prd.CaptionTagId.Value))
                                                         && (model.DiscountId == null || Discount == 0 || prd.Discount >= Discount)
                                                         && (model.OccasionId == null || model.OccasionId.Count == 0 || OccasionIds.Contains(prd.OccasionId.Value))
                                                         && (model.FabricId == null || model.FabricId.Count == 0 || FabricIds.Contains(prd.FabricId.Value))
                                                         && (model.LengthId == null || model.LengthId.Count == 0 || LengthIds.Contains(prd.LengthId.Value))
                                                         && (model.ColorId == null || model.ColorId.Count == 0 || ColorIds.Contains(prd.ColorId.Value))
                                                         && (model.PatternId == null || model.PatternId.Count == 0 || PatternIds.Contains(prd.PatternId.Value))
                                                         && (string.IsNullOrEmpty(model.UniqueId) || prd.UniqueId.Contains(model.UniqueId))
                                                         && (Ids == null || Ids.Count == 0 || Ids.Contains(prd.Id))
                                                        && (model.Price == null || model.Price.Count == 0 || (model.Price[0] <= prd.Price && model.Price[1] >= prd.Price))
                                                      select prd;
                result = (object)model.OrderBy switch
                {
                    "Name" => model.OrderByAsc ? (from orderData in result orderby orderData.Name ascending select orderData) : (from orderData in result orderby orderData.Name descending select orderData),
                    "Price" => model.OrderByAsc ? (from orderData in result orderby orderData.SellingPrice ascending select orderData) : (from orderData in result orderby orderData.SellingPrice descending select orderData),
                    "CreatedOn" => model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData),
                    "Discount" => model.OrderByAsc ? (from orderData in result orderby orderData.Discount.Value ascending select orderData) : (from orderData in result orderby orderData.Discount.Value descending select orderData),
                    _ => model.OrderByAsc ? (from orderData in result orderby orderData.ModifiedOn ascending select orderData) : (from orderData in result orderby orderData.ModifiedOn descending select orderData),
                };
                objResult.TotalRecord = result.Count();
                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new ProductMasterViewModel
                                        {
                                            Id = _security.EncryptData(x.Id),
                                            Name = x.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Medium)) : null,
                                            CategoryId = _security.EncryptData(x.CategoryId),
                                            Category = x.Category.Name,
                                            SubCategoryId = x.SubCategoryId.HasValue ? _security.EncryptData(x.SubCategoryId.Value) : null,
                                            SubCategory = x.SubCategory.Name,
                                            CaptionTagId = x.CaptionTagId.HasValue ? _security.EncryptData(x.CaptionTagId.Value) : null,
                                            CaptionTag = x.CaptionTag.Name,
                                            OccasionId = x.OccasionId.HasValue ? _security.EncryptData(x.OccasionId.Value) : null,
                                            Occasion = x.Occasion.Name,
                                            FabricId = x.FabricId.HasValue ? _security.EncryptData(x.FabricId.Value) : null,
                                            Fabric = x.Fabric.Name,
                                            LengthId = x.LengthId.HasValue ? _security.EncryptData(x.LengthId.Value) : null,
                                            Length = x.Length.Name,
                                            ColorId = x.ColorId.HasValue ? _security.EncryptData(x.ColorId.Value) : null,
                                            Color = x.Color.Name,
                                            PatternId = x.PatternId.HasValue ? _security.EncryptData(x.PatternId.Value) : null,
                                            Pattern = x.Pattern.Name,
                                            UniqueId = x.UniqueId,
                                            Desc = x.Desc,
                                            Summary = x.Desc,
                                            Price = x.Price,
                                            SellingPrice = x.SellingPrice ?? x.Price,
                                            Discount = x.Discount ?? 0,
                                            CreatedBy = x.CreatedBy,
                                            CreatedOn = x.CreatedOn,
                                            ModifiedBy = x.ModifiedBy,
                                            ModifiedOn = x.ModifiedOn,
                                            IsActive = x.IsActive.Value,
                                            IsDelete = x.IsDelete,
                                            Keyword = x.Keyword,
                                            MetaTitle = x.MetaTitle,
                                            MetaDesc = x.MetaDesc,
                                            ViewSectionId = x.ViewSectionId.HasValue ? _security.EncryptData(x.ViewSectionId.Value) : null,
                                            ViewSection = x.ViewSection.Name,
                                            IsWhishList = x.TblUserWishLists.Count > 0 && _loginUserDetail != null && x.TblUserWishLists.Any(y => y.ProductId == x.Id && y.UserId == _loginUserDetail.UserId),


                                        }).ToListAsync();

                return result != null
                        ? CreateResponse(objResult.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok, TotalRecord: objResult.TotalRecord)
                        : CreateResponse<IEnumerable<ProductMasterViewModel>>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound, TotalRecord: 0);
            }
            catch (Exception ex)
            {
                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = ex.Message;
                objResult.TotalRecord = 0;
                objResult.StatusCode = (int)ApiStatusCode.InternalServerError;
            }
            return objResult;
        }

        private long? getDiscount(decimal Price, decimal SellingPrice)
        {
            try
            {
                decimal ddValue = (Price - SellingPrice) / Price * 100;
                return (long)Math.Floor(ddValue);
            }
            catch (Exception)
            {

                return 0;
            }


        }

        public async Task<ServiceResponse<object>> IsSKUExist(string SKU, string id = null)
        {
            try
            {
                TblProductMaster result = await _db.TblProductMasters.FirstOrDefaultAsync(x => x.UniqueId.Trim().ToLower() == SKU.Trim().ToLower() && (string.IsNullOrEmpty(id) || x.Id != long.Parse(_security.DecryptData(id))));
                return CreateResponse<object>(result != null, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);

            }
            catch (Exception ex)
            {
                return CreateResponse<object>(null, ResponseMessage.Fail, true, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }
    }
}
