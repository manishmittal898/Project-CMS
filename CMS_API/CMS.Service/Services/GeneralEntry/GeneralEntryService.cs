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
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;

namespace CMS.Service.Services.GeneralEntry
{
    public class GeneralEntryService : BaseService, IGeneralEntryService
    {
        DB_CMSContext _db;
        private readonly FileHelper _fileHelper;
        public GeneralEntryService(DB_CMSContext db, IHostingEnvironment environment, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
            _fileHelper = new FileHelper(environment);
        }

        public async Task<ServiceResponse<TblGeneralEntry>> ActiveStatusUpdate(long id)
        {
            try
            {
                TblGeneralEntry objProduct = new TblGeneralEntry();
                objProduct = _db.TblGeneralEntries.FirstOrDefault(r => r.Id == id);

                objProduct.IsActive = !objProduct.IsActive;
                objProduct.ModifiedBy = _loginUserDetail.UserId ?? objProduct.ModifiedBy;
                objProduct.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse(objProduct as TblGeneralEntry, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return null;

            }
        }

        public async Task<ServiceResponse<TblGeneralEntry>> Delete(long id)
        {
            try
            {
                TblGeneralEntry objProduct = new TblGeneralEntry();
                objProduct = _db.TblGeneralEntries.FirstOrDefault(r => r.Id == id);

                objProduct.IsDeleted = (bool)true;
                objProduct.ModifiedBy = _loginUserDetail.UserId ?? objProduct.ModifiedBy;
                objProduct.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse(objProduct as TblGeneralEntry, ResponseMessage.Delete, true, ((int)ApiStatusCode.Ok));

            }
            catch (Exception ex)
            {
                return CreateResponse<TblGeneralEntry>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError), ex.Message.ToString());

            }
        }


        public async Task<ServiceResponse<GeneralEntryViewModel>> GetById(long id, bool isEdit = false)
        {
            ServiceResponse<GeneralEntryViewModel> ObjResponse = new ServiceResponse<GeneralEntryViewModel>();
            try
            {
                var result = await (from gen in _db.TblGeneralEntries
                                    where gen.Id == id

                                    select new GeneralEntryViewModel
                                    {
                                        Id = gen.Id,
                                        Title = gen.Title,
                                        CategoryId = gen.CategoryId,
                                        Category = gen.Category.Name,
                                        Description = gen.Description,
                                        DataId = gen.DataId,
                                        SortedOrder = gen.SortedOrder,
                                        ImagePath = !string.IsNullOrEmpty(gen.ImagePath) && (gen.Category.IsShowThumbnail || isEdit) ? gen.ImagePath.ToAbsolutePath() : null,
                                        Url = !string.IsNullOrEmpty(gen.Url) && (gen.Category.IsShowUrl || isEdit) ? gen.Url : null,
                                        Keyword = gen.Keyword,
                                        IsActive = gen.IsActive,
                                        IsDeleted = gen.IsDeleted

                                    }).FirstOrDefaultAsync();

                result.Data = await (from data in _db.TblFileDataMasters
                                     join mData in _db.TblGeneralEntries.DefaultIfEmpty() on data.DataId equals mData.DataId
                                     where data.DataId == result.DataId
                                     select new GeneralEntryDataViewModel
                                     {
                                         Id = data.Id,
                                         Value = !string.IsNullOrEmpty(data.Value) ? data.Value.ToAbsolutePath() : null,
                                         GeneralEntryId = result.Id
                                     }).ToListAsync();



                ObjResponse = CreateResponse(result, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<GeneralEntryViewModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<List<GeneralEntryViewModel>>> GetList(IndexModel model)
        {

            ServiceResponse<List<GeneralEntryViewModel>> objResult = new ServiceResponse<List<GeneralEntryViewModel>>();
            try
            {

                var result = from b in _db.Set<TblGeneralEntry>()
                             where b.IsDeleted == false
                             select new { m = b };

                switch (model.OrderBy)
                {
                    case "Name":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.m.Title ascending select orderData) : (from orderData in result orderby orderData.m.Title descending select orderData);
                        break;
                    case "CreatedOn":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.m.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.m.CreatedOn descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.m.SortedOrder ascending select orderData) : (from orderData in result orderby orderData.m.SortedOrder descending select orderData);
                        break;
                }
                objResult.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                if (result.Count() > 0)
                {


                    objResult.Data = (from x in result
                                      select new GeneralEntryViewModel
                                      {
                                          Id = x.m.Id,
                                          Title = x.m.Title,
                                          CategoryId = x.m.CategoryId,
                                          Category = x.m.Category.Name,
                                          Description = x.m.Description,
                                          Keyword = x.m.Keyword,
                                          DataId = x.m.DataId,
                                          SortedOrder = x.m.SortedOrder,
                                          ImagePath = !string.IsNullOrEmpty(x.m.ImagePath) && x.m.Category.IsShowThumbnail ? x.m.ImagePath.ToAbsolutePath() : null,
                                          Url = !string.IsNullOrEmpty(x.m.Url) && x.m.Category.IsShowUrl ? x.m.Url : null,
                                          IsActive = x.m.IsActive,
                                          IsDeleted = x.m.IsDeleted
                                      }).ToList();
                    return CreateResponse(objResult.Data as List<GeneralEntryViewModel>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<List<GeneralEntryViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
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

        public async Task<ServiceResponse<object>> Save(GeneralEntryPostModel model)
        {
            try
            {
                TblGeneralEntry objGeneralEntry = new TblGeneralEntry();
                List<TblFileDataMaster> dataItems = new List<TblFileDataMaster>();

                if (model.Id > 0)
                {
                    objGeneralEntry = await _db.TblGeneralEntries.Include(x => x.Category).FirstOrDefaultAsync(r => r.Id == model.Id);

                    var currentCat = model.CategoryId == objGeneralEntry.CategoryId ? objGeneralEntry.Category : await _db.TblGecategoryMaters.FirstOrDefaultAsync(r => r.Id == model.CategoryId);

                    objGeneralEntry.Id = model.Id;
                    objGeneralEntry.Title = model.Title;
                    objGeneralEntry.CategoryId = model.CategoryId;
                    objGeneralEntry.Description = model.Description;
                    objGeneralEntry.SortedOrder = model.SortedOrder;
                    objGeneralEntry.Keyword = model.Keyword;
                    objGeneralEntry.ModifiedBy = _loginUserDetail.UserId.Value;
                    objGeneralEntry.ModifiedOn = DateTime.Now;
                    objGeneralEntry.Url = !string.IsNullOrEmpty(model.Url) && currentCat.IsShowUrl ? model.Url : objGeneralEntry.Url;

                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {

                        objGeneralEntry.ImagePath = !string.IsNullOrEmpty(objGeneralEntry.ImagePath) && model.ImagePath.Contains(objGeneralEntry.ImagePath.Replace("\\", "/")) ? objGeneralEntry.ImagePath : _fileHelper.Save(model.ImagePath, FilePaths.GeneralEntry);
                    }
                    else
                    {
                        _fileHelper.Delete(objGeneralEntry.ImagePath);
                        objGeneralEntry.ImagePath = null;
                    }

                    _db.TblGeneralEntries.Update(objGeneralEntry);
                    await _db.SaveChangesAsync();
                    if (model.Data != null && model.Data.Count > 0)
                    {
                        string[] existingFilePaths = _db.TblFileDataMasters.Where(x => x.DataId == objGeneralEntry.DataId).Select(x => x.Value).ToArray();
                        model.Data = model.Data.FindAll(x => !existingFilePaths.Contains(x.Replace("\\", "/")));

                        dataItems = model.Data.Select(x => new TblFileDataMaster
                        {
                            Value = _fileHelper.Save(x, FilePaths.GeneralEntry),
                            DataId = objGeneralEntry.DataId,
                            ModifiedOn = DateTime.Now,
                            ModifiedBy = _loginUserDetail.UserId.Value,
                        }).ToList();
                        await _db.TblFileDataMasters.AddRangeAsync(dataItems);
                        await _db.SaveChangesAsync();
                    }

                    return CreateResponse((object)objGeneralEntry.Id, ResponseMessage.Update, true, (int)ApiStatusCode.Ok);
                }
                else
                {

                    objGeneralEntry.Title = model.Title;
                    objGeneralEntry.CategoryId = model.CategoryId;
                    objGeneralEntry.Description = model.Description;
                    objGeneralEntry.DataId = Guid.NewGuid().ToString();
                    objGeneralEntry.SortedOrder = model.SortedOrder;
                    objGeneralEntry.ImagePath = !string.IsNullOrEmpty(model.ImagePath) ? _fileHelper.Save(model.ImagePath, FilePaths.GeneralEntry) : null;
                    objGeneralEntry.Keyword = model.Keyword;
                    objGeneralEntry.IsActive = true;
                    objGeneralEntry.IsDeleted = false;
                    objGeneralEntry.ModifiedBy = _loginUserDetail.UserId.Value;
                    objGeneralEntry.ModifiedOn = DateTime.Now;
                    objGeneralEntry.CreatedBy = _loginUserDetail.UserId.Value;
                    objGeneralEntry.ModifiedBy = _loginUserDetail.UserId.Value;
                    objGeneralEntry.Url = model.Url;

                    var product = await _db.TblGeneralEntries.AddAsync(objGeneralEntry);
                    await _db.SaveChangesAsync();

                    if (model.Data != null && model.Data.Count > 0)
                    {
                        objGeneralEntry = await _db.TblGeneralEntries.Include(x => x.Category).FirstOrDefaultAsync(r => r.Id == product.Entity.Id);

                        dataItems = model.Data.Select(x => new TblFileDataMaster
                        {
                            Value = _fileHelper.Save(x, FilePaths.GeneralEntry),
                            DataId = objGeneralEntry.DataId,
                            CreatedBy = _loginUserDetail.UserId.Value,
                            ModifiedBy = _loginUserDetail.UserId.Value,

                        }).ToList();
                        await _db.TblFileDataMasters.AddRangeAsync(dataItems);
                        await _db.SaveChangesAsync();

                    }
                    return CreateResponse((object)objGeneralEntry.Id, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

                }

            }
            catch (Exception ex)
            {

                return CreateResponse<object>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<List<GeneralEntryDataViewModel>>> GetProductFile(string DataId, bool IsFile)
        {
            ServiceResponse<List<GeneralEntryDataViewModel>> objResponse = new ServiceResponse<List<GeneralEntryDataViewModel>>();
            try
            {

                objResponse.Data = await _db.TblFileDataMasters.Where(x => x.DataId == DataId).Select(x => new GeneralEntryDataViewModel
                {
                    Id = x.Id,
                    Value = !string.IsNullOrEmpty(x.Value) ? IsFile ? x.Value.ToAbsolutePath() : x.Value : null

                }).ToListAsync();
                objResponse = CreateResponse(objResponse.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                objResponse = CreateResponse<List<GeneralEntryDataViewModel>>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return objResponse;
        }

        public async Task<ServiceResponse<List<GeneralEntryViewModel>>> GetDataList(GeneralEntryFilterModel model)
        {
            ServiceResponse<List<GeneralEntryViewModel>> objResult = new ServiceResponse<List<GeneralEntryViewModel>>();

            try
            {
                var result = (from g in _db.TblGeneralEntries.Include(x => x.Category)
                              where g.IsDeleted == false && g.IsActive == true &&
                             (string.IsNullOrEmpty(model.Title) ? true : g.Title.Contains(model.Title)) &&
                             (model.CategoryId == g.CategoryId || (model.CategoryId.HasValue && model.CategoryId == 0))
                              select g);







                switch (model.OrderBy)
                {
                    case "Name":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Title ascending select orderData) : (from orderData in result orderby orderData.Title descending select orderData);
                        break;
                    case "CreatedOn":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.SortedOrder ascending select orderData) : (from orderData in result orderby orderData.SortedOrder descending select orderData);
                        break;
                }

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);
                objResult.TotalRecord = result.Count();

                if (result.Count() > 0)
                {
                    var imgData = await (from d in _db.TblFileDataMasters where result.Any(y => y.DataId == d.DataId) select d).ToListAsync();

                    objResult.Data = await result.Select(x => new GeneralEntryViewModel
                    {


                        Id = x.Id,
                        Title = x.Title,
                        CategoryId = x.CategoryId,
                        Category = x.Category.Name,
                        Description = x.Description,
                        Keyword = x.Keyword,
                        DataId = x.DataId,
                        SortedOrder = x.SortedOrder,
                        ImagePath = !string.IsNullOrEmpty(x.ImagePath) && x.Category.IsShowThumbnail ? x.ImagePath.ToAbsolutePath() : null,
                        Url = !string.IsNullOrEmpty(x.Url) && x.Category.IsShowUrl ? x.Url : null,
                        IsActive = x.IsActive,
                        IsDeleted = x.IsDeleted,
                        Data = imgData.Count>0 ? imgData.Where(y => y.DataId == x.DataId).Select(r => new GeneralEntryDataViewModel
                        {
                            GeneralEntryId = x.Id,
                            Id = r.Id,
                            Value = !string.IsNullOrEmpty(r.Value) ? r.Value.ToAbsolutePath() : null
                        }
                    ).ToList() : null
                    }).ToListAsync();

                    return CreateResponse(objResult.Data as List<GeneralEntryViewModel>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<List<GeneralEntryViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }
            }
            catch (Exception ex)
            {

                return CreateResponse<List<GeneralEntryViewModel>>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message);


            }

        }
        public async Task<object> DeleteGeneralEntryItems(long id)
        {
            try
            {
                TblFileDataMaster dataItem = _db.TblFileDataMasters.FirstOrDefault(r => r.Id == id);

                if (dataItem != null)
                {

                    TblGeneralEntry mainData = _db.TblGeneralEntries.FirstOrDefault(r => r.Id == id);
                    if (mainData != null)
                    {
                        _fileHelper.Delete(dataItem.Value);


                    }
                    dataItem.ModifiedBy = _loginUserDetail.UserId ?? dataItem.ModifiedBy;
                    dataItem.ModifiedOn = DateTime.Now;
                    _db.TblFileDataMasters.Remove(dataItem);

                    await _db.SaveChangesAsync();
                    return CreateResponse(dataItem, ResponseMessage.Delete, true, ((int)ApiStatusCode.Ok));
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
    }
}
