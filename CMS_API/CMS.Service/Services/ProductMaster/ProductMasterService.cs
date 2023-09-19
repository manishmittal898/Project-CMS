using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
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
        DB_CMSContext _db;
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


                var result = (from pdct in _db.TblProductMasters
                              join prd in _db.VwProductMasters.DefaultIfEmpty() on pdct.Id equals prd.Id
                              where !pdct.IsDelete && (string.IsNullOrEmpty(model.Search) || pdct.Name.Contains(model.Search) || pdct.Category.Name.Contains(model.Search) || pdct.SubCategory.Name.Contains(model.Search) || pdct.CaptionTag.Name.Contains(model.Search))
                              select new { pdct = pdct, selling = prd.SellingPrice });
                switch (model.OrderBy)
                {
                    case "Name":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.pdct.Name ascending select orderData) : (from orderData in result orderby orderData.pdct.Name descending select orderData);
                        break;

                    case "CreatedOn":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.pdct.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.pdct.CreatedOn descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.pdct.ModifiedOn ascending select orderData) : (from orderData in result orderby orderData.pdct.ModifiedOn descending select orderData);
                        break;
                }
                objResult.TotalRecord = result.Count();
                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new ProductMasterViewModel
                                        {
                                            Id = _security.EncryptData(x.pdct.Id),
                                            Name = x.pdct.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.pdct.ImagePath) ? x.pdct.ImagePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Medium)) : null,
                                            CategoryId = _security.EncryptData(x.pdct.CategoryId),
                                            Category = x.pdct.Category.Name,
                                            SubCategoryId = x.pdct.SubCategoryId.HasValue ? _security.EncryptData(x.pdct.SubCategoryId.Value) : null,
                                            SubCategory = x.pdct.SubCategory.Name,
                                            CaptionTagId = x.pdct.CaptionTagId.HasValue ? _security.EncryptData(x.pdct.CaptionTagId.Value) : null,
                                            CaptionTag = x.pdct.CaptionTag.Name,
                                            ViewSectionId = x.pdct.ViewSectionId.HasValue ? _security.EncryptData(x.pdct.ViewSectionId.Value) : null,
                                            ViewSection = x.pdct.ViewSection.Name,
                                            DiscountId = x.pdct.DiscountId.HasValue ? _security.EncryptData(x.pdct.DiscountId.Value) : null,
                                            Discount = x.pdct.Discount.Name,
                                            OccasionId = x.pdct.OccasionId.HasValue ? _security.EncryptData(x.pdct.OccasionId.Value) : null,
                                            Occasion = x.pdct.Occasion.Name,
                                            FabricId = x.pdct.FabricId.HasValue ? _security.EncryptData(x.pdct.FabricId.Value) : null,
                                            Fabric = x.pdct.Fabric.Name,
                                            LengthId = x.pdct.LengthId.HasValue ? _security.EncryptData(x.pdct.LengthId.Value) : null,
                                            Length = x.pdct.Length.Name,
                                            ColorId = x.pdct.ColorId.HasValue ? _security.EncryptData(x.pdct.ColorId.Value) : null,
                                            Color = x.pdct.Color.Name,
                                            PatternId = x.pdct.PatternId.HasValue ? _security.EncryptData(x.pdct.PatternId.Value) : null,
                                            Pattern = x.pdct.Pattern.Name,
                                            UniqueId = x.pdct.UniqueId,
                                            Desc = x.pdct.Desc,
                                            Summary = x.pdct.Desc,
                                            Price = x.pdct.Price,
                                            SellingPrice = x.selling,
                                            MetaTitle = x.pdct.MetaTitle,
                                            MetaDesc = x.pdct.MetaDesc,
                                            CreatedBy = x.pdct.CreatedBy,
                                            CreatedOn = x.pdct.CreatedOn,
                                            ModifiedBy = x.pdct.ModifiedBy,
                                            ModifiedOn = x.pdct.ModifiedOn,
                                            IsActive = x.pdct.IsActive.Value,
                                            IsDelete = x.pdct.IsDelete,
                                            Keyword = x.pdct.Keyword,
                                            IsWhishList = (_loginUserDetail != null && x.pdct.TblUserWishLists.Count(x => x.UserId == _loginUserDetail.UserId && x.ProductId == x.Id) > 0) ? true : false,

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
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }
        public ServiceResponse<ProductMasterViewModel> GetById(string id)
        {
            ServiceResponse<ProductMasterViewModel> ObjResponse = new ServiceResponse<ProductMasterViewModel>();
            try
            {

                var detail = _db.TblProductMasters.Include(x => x.TblProductStocks).Where(x => x.Id == long.Parse(_security.DecryptData(id)) && !x.IsDelete).Select(x => new ProductMasterViewModel
                {
                    Id = _security.EncryptData(x.Id.ToString()),
                    Name = x.Name,
                    ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath() : null,
                    CategoryId = _security.EncryptData(x.CategoryId),
                    Category = x.Category.Name,
                    SubCategoryId = x.SubCategoryId.HasValue ? _security.EncryptData(x.SubCategoryId.Value) : null,
                    SubCategory = x.SubCategory.Name,
                    CaptionTagId = x.CaptionTagId.HasValue ? _security.EncryptData(x.CaptionTagId.Value) : null,
                    CaptionTag = x.CaptionTag.Name,
                    ViewSectionId = x.ViewSectionId.HasValue ? _security.EncryptData(x.ViewSectionId.Value) : null,
                    ViewSection = x.ViewSection.Name,
                    DiscountId = x.DiscountId.HasValue ? _security.EncryptData(x.DiscountId.Value) : null,
                    Discount = x.Discount.Name,
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
                    SellingPrice = x.DiscountId.HasValue ? Math.Floor(x.Price.Value - (x.Price.Value * decimal.Parse(x.Discount.Value)) / 100) : x.Price,
                    MetaTitle = x.MetaTitle,
                    MetaDesc = x.MetaDesc,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedOn = x.ModifiedOn,
                    IsActive = x.IsActive.Value,
                    IsDelete = x.IsDelete,
                    Keyword = x.Keyword,
                    IsWhishList = x.TblUserWishLists.Count > 0 && _loginUserDetail != null ? x.TblUserWishLists.Any(y => y.ProductId == x.Id && y.UserId == _loginUserDetail.UserId) : false,

                    Stocks = x.TblProductStocks.Count > 0 ? x.TblProductStocks.OrderBy(x => x.Size.SortedOrder).Select(st => new ProductStockModel
                    {
                        Id = _security.EncryptData(st.Id),
                        ProductId = _security.EncryptData(x.Id),
                        SizeId = _security.EncryptData(st.SizeId),
                        Size = st.Size.Name,
                        UnitPrice = st.UnitPrice,
                        SellingPrice = st.Product.DiscountId.HasValue ? Math.Floor(st.UnitPrice.Value - (st.UnitPrice.Value * decimal.Parse(st.Product.Discount.Value)) / 100) : st.UnitPrice,
                        Quantity = st.Quantity

                    }).ToList() : null
                }).FirstOrDefault();
                if (detail != null)
                {
                    var productFiles = GetProductFile(id).Result;
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

        public async Task<ServiceResponse<TblProductMaster>> Save(ProductMasterPostModel model)
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
                    objProduct.DiscountId = !string.IsNullOrEmpty(model.DiscountId) ? long.Parse(_security.DecryptData(model.DiscountId)) : null as Nullable<long>;
                    objProduct.OccasionId = !string.IsNullOrEmpty(model.OccasionId) ? long.Parse(_security.DecryptData(model.OccasionId)) : null as Nullable<long>;
                    objProduct.FabricId = !string.IsNullOrEmpty(model.FabricId) ? long.Parse(_security.DecryptData(model.FabricId)) : null as Nullable<long>;
                    objProduct.LengthId = !string.IsNullOrEmpty(model.LengthId) ? long.Parse(_security.DecryptData(model.LengthId)) : null as Nullable<long>;
                    objProduct.ColorId = !string.IsNullOrEmpty(model.ColorId) ? long.Parse(_security.DecryptData(model.ColorId)) : null as Nullable<long>;
                    objProduct.PatternId = !string.IsNullOrEmpty(model.PatternId) ? long.Parse(_security.DecryptData(model.PatternId)) : null as Nullable<long>;
                    objProduct.UniqueId = model.UniqueId;

                    objProduct.Desc = model.Desc;
                    objProduct.Price = model.Price;
                    objProduct.Summary = model.Summary;

                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {

                        objProduct.ImagePath = !string.IsNullOrEmpty(objProduct.ImagePath) && model.ImagePath.Contains(objProduct.ImagePath.Replace("\\", "/")) ? objProduct.ImagePath : await _fileHelper.Save(model.ImagePath, FilePaths.ProductImages_Main, isThumbnail: true);
                    }
                    else
                    {
                        _fileHelper.Delete(objProduct.ImagePath);
                        objProduct.ImagePath = null;
                    }
                    objProduct.ModifiedBy = _loginUserDetail.UserId.Value;
                    objProduct.Keyword = !string.IsNullOrEmpty(model.Keyword) ? model.Keyword : model.Name;
                    objProduct.MetaTitle = model.MetaTitle;
                    objProduct.MetaDesc = model.MetaDesc;
                    objProduct.ModifiedOn = DateTime.Now;

                    var product = _db.TblProductMasters.Update(objProduct);

                    if (model.Stocks != null && model.Stocks.Count > 0)
                    {
                        var exIds = model.Stocks.Where(x => !string.IsNullOrEmpty(x.Id)).Select(x => long.Parse(_security.DecryptData(x.Id))).ToArray();

                        var deleteStock = objProduct.TblProductStocks.Where(x => !exIds.Contains(x.Id)).ToList();
                        if (deleteStock.Count > 0)
                        {
                            _db.TblProductStocks.RemoveRange(deleteStock);
                            _db.SaveChanges();
                        }


                        List<TblProductStock> prStocks = new List<TblProductStock>();
                        model.Stocks.Where(wh => !string.IsNullOrEmpty(wh.Id) && long.Parse(_security.DecryptData(wh.Id)) > 0).ToList().ForEach(xs =>
                        {
                            TblProductStock prStock = objProduct.TblProductStocks.Where(p => p.Id == long.Parse(_security.DecryptData(xs.Id))).FirstOrDefault();
                            if (prStock != null)
                            {
                                prStock.SizeId = long.Parse(_security.DecryptData(xs.SizeId));
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


                        productStock = model.Stocks.Where(x => x.Id == default || long.Parse(_security.DecryptData(x.Id)) == 0).Select(x => new TblProductStock
                        {

                            ProductId = long.Parse(_security.DecryptData(model.Id)),
                            SizeId = long.Parse(_security.DecryptData(x.SizeId)),
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
                        _db.SaveChanges();
                    }
                    objProduct.TblProductStocks = null;

                    return CreateResponse(objProduct, ResponseMessage.Update, true, (int)ApiStatusCode.Ok);
                }
                else
                {


                    objProduct.Name = model.Name;
                    objProduct.CategoryId = long.Parse(_security.DecryptData(model.CategoryId));
                    objProduct.SubCategoryId = !string.IsNullOrEmpty(model.SubCategoryId) ? long.Parse(_security.DecryptData(model.SubCategoryId)) : null as Nullable<long>;
                    objProduct.ViewSectionId = !string.IsNullOrEmpty(model.ViewSectionId) ? long.Parse(_security.DecryptData(model.ViewSectionId)) : null as Nullable<long>;
                    objProduct.CaptionTagId = !string.IsNullOrEmpty(model.CaptionTagId) ? long.Parse(_security.DecryptData(model.CaptionTagId)) : null as Nullable<long>;
                    objProduct.DiscountId = !string.IsNullOrEmpty(model.DiscountId) ? long.Parse(_security.DecryptData(model.DiscountId)) : null as Nullable<long>;
                    objProduct.OccasionId = !string.IsNullOrEmpty(model.OccasionId) ? long.Parse(_security.DecryptData(model.OccasionId)) : null as Nullable<long>;
                    objProduct.FabricId = !string.IsNullOrEmpty(model.FabricId) ? long.Parse(_security.DecryptData(model.FabricId)) : null as Nullable<long>;
                    objProduct.LengthId = !string.IsNullOrEmpty(model.LengthId) ? long.Parse(_security.DecryptData(model.LengthId)) : null as Nullable<long>;
                    objProduct.ColorId = !string.IsNullOrEmpty(model.ColorId) ? long.Parse(_security.DecryptData(model.ColorId)) : null as Nullable<long>;
                    objProduct.PatternId = !string.IsNullOrEmpty(model.PatternId) ? long.Parse(_security.DecryptData(model.PatternId)) : null as Nullable<long>;
                    objProduct.UniqueId = model.UniqueId;
                    objProduct.ImagePath = !string.IsNullOrEmpty(model.ImagePath) ? await _fileHelper.Save(model.ImagePath, FilePaths.ProductImages_Main, isThumbnail: true) : null;
                    objProduct.Desc = model.Desc;
                    objProduct.Price = model.Price;
                    objProduct.Summary = model.Summary;
                    objProduct.IsActive = true;
                    objProduct.Keyword = !string.IsNullOrEmpty(model.Keyword) ? model.Keyword : model.Name;
                    objProduct.CreatedBy = _loginUserDetail.UserId.Value;
                    objProduct.ModifiedBy = _loginUserDetail.UserId.Value;
                    objProduct.MetaTitle = model.MetaTitle;
                    objProduct.MetaDesc = model.MetaDesc;
                    var product = await _db.TblProductMasters.AddAsync(objProduct);
                    _db.SaveChanges();

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
                        _db.SaveChanges();

                    }

                    if (model.Stocks != null && model.Stocks.Count > 0)
                    {
                        productStock = model.Stocks.Select(x => new TblProductStock
                        {

                            ProductId = product.Entity.Id,
                            SizeId = long.Parse(_security.DecryptData(x.SizeId)),
                            UnitPrice = x.UnitPrice,
                            Quantity = x.Quantity

                        }).ToList();
                        await _db.TblProductStocks.AddRangeAsync(productStock);
                        _db.SaveChanges();

                    }
                    objProduct.TblProductStocks = null;
                    return CreateResponse(objProduct, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

                }

            }
            catch (Exception ex)
            {

                return CreateResponse<TblProductMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

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
                await _db.SaveChangesAsync();
                return CreateResponse(objProduct as TblProductMaster, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return CreateResponse<TblProductMaster>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError), ex.Message.ToString());

            }
        }
        public async Task<ServiceResponse<TblProductMaster>> Delete(string id)
        {
            try
            {
                TblProductMaster objProduct = new TblProductMaster();
                objProduct = await _db.TblProductMasters.FirstOrDefaultAsync(r => r.Id == long.Parse(_security.DecryptData(id)));

                objProduct.IsDelete = (bool)true;
                objProduct.ModifiedBy = _loginUserDetail.UserId ?? objProduct.ModifiedBy;
                objProduct.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse(objProduct as TblProductMaster, ResponseMessage.Delete, true, ((int)ApiStatusCode.Ok));

            }
            catch (Exception ex)
            {
                return CreateResponse<TblProductMaster>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError), ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<object>> DeleteProductFile(string id)
        {
            try
            {
                List<TblProductImage> objProductFile = _db.TblProductImages.Where(r => r.Id == long.Parse(_security.DecryptData(id)) || string.IsNullOrEmpty(r.FilePath)).ToList();

                objProductFile.ForEach(x =>
                {
                    _fileHelper.Delete(x.FilePath);
                });
                if (objProductFile != null)
                {

                    _db.TblProductImages.RemoveRange(objProductFile);

                    //add for thumbnail
                    await _db.SaveChangesAsync();
                    return CreateResponse(true as object, ResponseMessage.Delete, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse(null as object, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));

                }


            }
            catch (Exception ex)
            {
                return CreateResponse(null as object, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError), ex.Message.ToString());

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


                var result = (from lkType in _db.TblProductMasters
                              where !lkType.IsDelete && lkType.IsActive.Value && !lkType.Category.IsDelete && lkType.Category.IsActive == true && (string.IsNullOrEmpty(model.Search) || lkType.Name.Contains(model.Search) || lkType.Category.Name.Contains(model.Search) || lkType.SubCategory.Name.Contains(model.Search) || lkType.CaptionTag.Name.Contains(model.Search))
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
                objResult.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new ProductCategoryViewModel
                                        {
                                            Id = _security.EncryptData(x.Id),
                                            Name = x.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Medium)) : null,


                                        }).ToListAsync();

                if (objResult.Data != null)
                {

                    return CreateResponse(objResult.Data as IEnumerable<ProductCategoryViewModel>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<ProductCategoryViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }
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
                List<long> DiscountIds = model.DiscountId != null && model.DiscountId.Count > 0 ? model.DiscountId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> OccasionIds = model.OccasionId != null && model.OccasionId.Count > 0 ? model.OccasionId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> FabricIds = model.FabricId != null && model.FabricId.Count > 0 ? model.FabricId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> LengthIds = model.LengthId != null && model.LengthId.Count > 0 ? model.LengthId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> ColorIds = model.ColorId != null && model.ColorId.Count > 0 ? model.ColorId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;
                List<long> PatternIds = model.PatternId != null && model.PatternId.Count > 0 ? model.PatternId.Select(p => long.Parse(_security.DecryptData(p))).ToList() : null;


                var result = (from prd in _db.TblProductMasters.Include(x => x.TblUserWishLists)
                              join pt in _db.VwProductMasters.DefaultIfEmpty() on prd.Id equals pt.Id

                              where !prd.IsDelete && (string.IsNullOrEmpty(model.Search) || prd.Name.Contains(model.Search) || prd.Category.Name.Contains(model.Search) || prd.SubCategory.Name.Contains(model.Search) || prd.CaptionTag.Name.Contains(model.Search))
                              && (string.IsNullOrEmpty(model.Keyword) || model.Keyword.Contains(prd.Keyword) || string.IsNullOrEmpty(model.Keyword) || prd.Name.Contains(model.Keyword) || prd.Category.Name.Contains(model.Keyword) || prd.SubCategory.Name.Contains(model.Keyword) || prd.CaptionTag.Name.Contains(model.Keyword))
                                && (model.CategoryId == null || model.CategoryId.Count == 0 || CategoryIds.Contains(prd.CategoryId))
                                 && (model.SubCategoryId == null || model.SubCategoryId.Count == 0 || SubCategoryIds.Contains(prd.SubCategoryId.Value))
                                 && (model.SizeId == null || model.SizeId.Count == 0 || prd.TblProductStocks.Any(x => SizeIds.Contains(x.SizeId)))
                                 && (model.ViewSectionId == null || model.ViewSectionId.Count == 0 || ViewSectionIds.Contains(prd.ViewSectionId.Value))
                                 && (model.CaptionTagId == null || model.CaptionTagId.Count == 0 || CaptionTagIds.Contains(prd.CaptionTagId.Value))
                                 && (model.DiscountId == null || model.DiscountId.Count == 0 || DiscountIds.Contains(prd.DiscountId.Value))
                                 && (model.OccasionId == null || model.OccasionId.Count == 0 || OccasionIds.Contains(prd.OccasionId.Value))
                                 && (model.FabricId == null || model.FabricId.Count == 0 || FabricIds.Contains(prd.FabricId.Value))
                                 && (model.LengthId == null || model.LengthId.Count == 0 || LengthIds.Contains(prd.LengthId.Value))
                                 && (model.ColorId == null || model.ColorId.Count == 0 || ColorIds.Contains(prd.ColorId.Value))
                                 && (model.PatternId == null || model.PatternId.Count == 0 || PatternIds.Contains(prd.PatternId.Value))
                                 && (string.IsNullOrEmpty(model.UniqueId) || prd.UniqueId.Contains(model.UniqueId))
                                 && (Ids == null || Ids.Count == 0 || Ids.Contains(prd.Id))
                                && (model.Price == null || model.Price.Count == 0 || (model.Price[0] <= prd.Price && model.Price[1] >= prd.Price))
                              select new { prd = prd, selling = pt.SellingPrice });
                switch (model.OrderBy)
                {
                    case "Name":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.prd.Name ascending select orderData) : (from orderData in result orderby orderData.prd.Name descending select orderData);
                        break;
                    case "Price":
                        result = model.OrderByAsc ? (from orderData in result orderby (orderData.selling) ascending select orderData) : (from orderData in result orderby orderData.selling descending select orderData);
                        break;
                    case "CreatedOn":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.prd.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.prd.CreatedOn descending select orderData);
                        break;
                    case "Discount":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.prd.Discount.Value ascending select orderData) : (from orderData in result orderby orderData.prd.Discount.Value descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.prd.ModifiedOn ascending select orderData) : (from orderData in result orderby orderData.prd.ModifiedOn descending select orderData);
                        break;
                }
                objResult.TotalRecord = result.Count();
                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new ProductMasterViewModel
                                        {
                                            Id = _security.EncryptData(x.prd.Id),
                                            Name = x.prd.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.prd.ImagePath) ? x.prd.ImagePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Medium)) : null,
                                            CategoryId = _security.EncryptData(x.prd.CategoryId),
                                            Category = x.prd.Category.Name,
                                            SubCategoryId = x.prd.SubCategoryId.HasValue ? _security.EncryptData(x.prd.SubCategoryId.Value) : null,
                                            SubCategory = x.prd.SubCategory.Name,
                                            CaptionTagId = x.prd.CaptionTagId.HasValue ? _security.EncryptData(x.prd.CaptionTagId.Value) : null,
                                            CaptionTag = x.prd.CaptionTag.Name,
                                            DiscountId = x.prd.DiscountId.HasValue ? _security.EncryptData(x.prd.DiscountId.Value) : null,
                                            Discount = x.prd.Discount.Name,
                                            OccasionId = x.prd.OccasionId.HasValue ? _security.EncryptData(x.prd.OccasionId.Value) : null,
                                            Occasion = x.prd.Occasion.Name,
                                            FabricId = x.prd.FabricId.HasValue ? _security.EncryptData(x.prd.FabricId.Value) : null,
                                            Fabric = x.prd.Fabric.Name,
                                            LengthId = x.prd.LengthId.HasValue ? _security.EncryptData(x.prd.LengthId.Value) : null,
                                            Length = x.prd.Length.Name,
                                            ColorId = x.prd.ColorId.HasValue ? _security.EncryptData(x.prd.ColorId.Value) : null,
                                            Color = x.prd.Color.Name,
                                            PatternId = x.prd.PatternId.HasValue ? _security.EncryptData(x.prd.PatternId.Value) : null,
                                            Pattern = x.prd.Pattern.Name,
                                            UniqueId = x.prd.UniqueId,
                                            Desc = x.prd.Desc,
                                            Summary = x.prd.Desc,
                                            Price = x.prd.Price,
                                            SellingPrice = x.selling,
                                            CreatedBy = x.prd.CreatedBy,
                                            CreatedOn = x.prd.CreatedOn,
                                            ModifiedBy = x.prd.ModifiedBy,
                                            ModifiedOn = x.prd.ModifiedOn,
                                            IsActive = x.prd.IsActive.Value,
                                            IsDelete = x.prd.IsDelete,
                                            Keyword = x.prd.Keyword,
                                            MetaTitle = x.prd.MetaTitle,
                                            MetaDesc = x.prd.MetaDesc,
                                            ViewSectionId = x.prd.ViewSectionId.HasValue ? _security.EncryptData(x.prd.ViewSectionId.Value) : null,
                                            ViewSection = x.prd.ViewSection.Name,
                                            IsWhishList = x.prd.TblUserWishLists.Count > 0 && _loginUserDetail != null ? x.prd.TblUserWishLists.Any(y => y.ProductId == x.prd.Id && y.UserId == _loginUserDetail.UserId) : false
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
                objResult.Message = ex.Message;
                objResult.TotalRecord = 0;
                objResult.StatusCode = ((int)ApiStatusCode.InternalServerError);
            }
            return objResult;
        }

    }
}
