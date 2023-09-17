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
using static CMS.Core.FixedValue.Enums;

namespace CMS.Service.Services.GeneralEntry
{
    public class GECategoryService : BaseService, IGECategoryService
    {
        DB_CMSContext _db;
        private readonly FileHelper _fileHelper;
        public GECategoryService(DB_CMSContext db, IHostingEnvironment environment, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
            _fileHelper = new FileHelper(environment);
        }


        public async Task<ServiceResponse<IEnumerable<GeneralEntryCategoryViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<GeneralEntryCategoryViewModel>> objResult = new ServiceResponse<IEnumerable<GeneralEntryCategoryViewModel>>();
            try
            {

                var result = (from data in _db.TblGecategoryMaters
                              where !data.IsDelete && (string.IsNullOrEmpty(model.Search) || data.Name.Contains(model.Search))
                              select data);
                switch (model.OrderBy)
                {
                    case "Name":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Name ascending select orderData) : (from orderData in result orderby orderData.Name descending select orderData);
                        break;
                    case "CreatedOn":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.SortedOrder ascending select orderData) : (from orderData in result orderby orderData.SortedOrder descending select orderData);
                        break;
                }
                objResult.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                if (objResult.TotalRecord > 0)
                {
                    objResult.Data = result.ToList().Select(x => new GeneralEntryCategoryViewModel
                    {
                        Id = _security.EncryptData(x.Id),
                        Name = x.Name,
                        ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Medium)) : null,
                        SortedOrder = x.SortedOrder.Value,
                        EnumValue = x.EnumValue,
                        IsShowDataInMain = x.IsShowDataInMain,
                        IsShowUrl = x.IsShowUrl,
                        ContentType = _security.EncryptData(x.ContentType),
                        IsShowThumbnail = x.IsShowThumbnail,
                        IsShowInMain = x.IsShowInMain,
                        IsSingleEntry = x.IsSingleEntry,
                        IsSystemEntry = x.IsSystemEntry,
                        CreatedBy = x.CreatedBy,
                        CreatedOn = x.CreatedOn,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedOn = x.ModifiedOn,
                        IsActive = x.IsActive.Value,
                        IsDelete = x.IsDelete,
                        ContentTypeText = Enum.GetValues(typeof(ContentTypeEnum)).Cast<ContentTypeEnum>().Where(en => x.ContentType == (int)en).Select(r => r.GetStringValue()).FirstOrDefault()
                    });

                    return CreateResponse(objResult.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<GeneralEntryCategoryViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
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
        public async Task<ServiceResponse<GeneralEntryCategoryViewModel>> GetById(string id)
        {
            ServiceResponse<GeneralEntryCategoryViewModel> ObjResponse = new ServiceResponse<GeneralEntryCategoryViewModel>();
            try
            {
                TblGecategoryMater detail = await _db.TblGecategoryMaters.Where(x => x.Id == long.Parse(_security.DecryptData(id)) && x.IsActive.Value && x.IsDelete == false).FirstOrDefaultAsync();
                if (detail != null)
                {

                    ObjResponse.Data = new GeneralEntryCategoryViewModel
                    {
                        Id = id,
                        Name = detail.Name,
                        ImagePath = !string.IsNullOrEmpty(detail.ImagePath) ? detail.ImagePath.ToAbsolutePath() : null,
                        SortedOrder = detail.SortedOrder.Value,
                        EnumValue = detail.EnumValue,
                        ContentType = _security.EncryptData(detail.ContentType),
                        IsShowUrl = detail.IsShowUrl,
                        IsShowDataInMain = detail.IsShowDataInMain,
                        IsShowInMain = detail.IsShowInMain,
                        IsSingleEntry = detail.IsSingleEntry,
                        IsSystemEntry = detail.IsSystemEntry,
                        IsShowThumbnail = detail.IsShowThumbnail,
                        CreatedBy = detail.CreatedBy,
                        CreatedOn = detail.CreatedOn,
                        ModifiedBy = detail.ModifiedBy,
                        ModifiedOn = detail.ModifiedOn,
                        IsActive = detail.IsActive.Value,
                        IsDelete = detail.IsDelete,
                        ContentTypeText = Enum.GetValues(typeof(ContentTypeEnum)).Cast<ContentTypeEnum>().Where(en => detail.ContentType == (int)en).Select(r => r.GetStringValue()).FirstOrDefault()

                    };
                    ObjResponse = CreateResponse(ObjResponse.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);

                }
                else
                {
                    ObjResponse = CreateResponse<GeneralEntryCategoryViewModel>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound);

                }

            }
            catch (Exception ex)
            {
                ObjResponse = CreateResponse<GeneralEntryCategoryViewModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblGecategoryMater>> Save(GeneralEntryCategoryPostModel model)
        {
            try
            {

                if (!string.IsNullOrEmpty(model.Id))
                {

                    TblGecategoryMater objData = _db.TblGecategoryMaters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(model.Id)));



                    objData.Name = model.Name;
                    objData.SortedOrder = model.SortedOrder;
                    objData.ContentType = int.Parse(_security.DecryptData(model.ContentType));
                    objData.IsSingleEntry = objData.IsSingleEntry ? objData.IsSingleEntry : model.IsSingleEntry;
                    objData.IsShowInMain = objData.IsShowInMain ? objData.IsShowInMain : model.IsShowInMain;
                    objData.IsShowThumbnail = objData.IsShowThumbnail ? objData.IsShowThumbnail : model.IsShowThumbnail;
                    objData.IsShowDataInMain = objData.IsShowDataInMain ? objData.IsShowDataInMain : model.IsShowDataInMain;
                    objData.IsShowUrl = objData.IsShowUrl ? objData.IsShowUrl : model.IsShowUrl;

                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    objData.ModifiedOn = DateTime.Now;


                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {

                        objData.ImagePath = !string.IsNullOrEmpty(objData.ImagePath) && model.ImagePath.Contains(objData.ImagePath.Replace("\\", "/")) ? objData.ImagePath : await _fileHelper.Save(model.ImagePath, FilePaths.GeneralEntryCategory);
                    }
                    else if (!string.IsNullOrEmpty(objData.ImagePath))
                    {
                        _fileHelper.Delete(objData.ImagePath);
                        objData.ImagePath = null;

                    }
                    var roletype = _db.TblGecategoryMaters.Update(objData);
                    _db.SaveChanges();
                    return CreateResponse(objData, ResponseMessage.Update, true, (int)ApiStatusCode.Ok);

                }
                else
                {

                    TblGecategoryMater objData = new TblGecategoryMater();

                    objData.Name = model.Name;
                    objData.SortedOrder = model.SortedOrder;
                    objData.ContentType = int.Parse(_security.DecryptData(model.ContentType));
                    objData.EnumValue = model.Name.Replace(' ', '_');
                    objData.IsSingleEntry = model.IsSingleEntry;
                    objData.IsShowInMain = model.IsShowInMain;
                    objData.IsShowDataInMain = model.IsShowDataInMain;
                    objData.IsShowThumbnail = model.IsShowThumbnail;
                    objData.IsSystemEntry = false;
                    objData.IsShowUrl = model.IsShowUrl;

                    objData.ImagePath = string.IsNullOrEmpty(model.ImagePath) ? null : await _fileHelper.Save(model.ImagePath, FilePaths.GeneralEntryCategory);

                    objData.IsActive = true;
                    objData.CreatedBy = _loginUserDetail.UserId.Value;
                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    var roletype = await _db.TblGecategoryMaters.AddAsync(objData);
                    _db.SaveChanges();
                    return CreateResponse(objData, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

                }


            }
            catch (Exception ex)
            {

                return CreateResponse<TblGecategoryMater>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }


        public async Task<ServiceResponse<TblGecategoryMater>> Delete(string id)
        {
            try
            {
                TblGecategoryMater objData = _db.TblGecategoryMaters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(id)));

                if (objData.IsSystemEntry)
                {
                    return CreateResponse(objData, ResponseMessage.RestrictedRecord, true, (int)ApiStatusCode.Ok);
                }
                else
                {

                    objData.IsDelete = true;
                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    objData.ModifiedOn = DateTime.Now;
                    await _db.SaveChangesAsync();
                    return CreateResponse(objData, ResponseMessage.Delete, true, (int)ApiStatusCode.Ok);
                }
            }
            catch (Exception)
            {

                return CreateResponse<TblGecategoryMater>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError));
            }


        }

        public async Task<ServiceResponse<TblGecategoryMater>> ActiveStatusUpdate(string id)
        {
            try
            {
                TblGecategoryMater objData = _db.TblGecategoryMaters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(id)));
                if (objData.IsSystemEntry)
                {
                    return CreateResponse(objData, ResponseMessage.RestrictedRecord, true, (int)ApiStatusCode.Ok);
                }
                else
                {
                    objData.IsActive = !objData.IsActive;
                    await _db.SaveChangesAsync();
                    return CreateResponse(objData as TblGecategoryMater, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));

                }

            }
            catch (Exception)
            {

                return null;

            }
        }

        public async Task<ServiceResponse<TblGecategoryMater>> FlagStatusUpdate(string id, string columnName)
        {
            try
            {
                TblGecategoryMater objData = new TblGecategoryMater();
                objData = _db.TblGecategoryMaters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(id)));

                switch (columnName)
                {
                    case "IsShowDataInMain":
                        objData.IsShowDataInMain = !objData.IsShowDataInMain;


                        break;
                    case "IsShowInMain":
                        objData.IsShowInMain = !objData.IsShowInMain;


                        break;
                    case "IsSingleEntry":
                        objData.IsSingleEntry = !objData.IsSingleEntry;

                        break;
                    case "IsShowThumbnail":
                        objData.IsShowThumbnail = !objData.IsShowThumbnail;
                        break;
                    case "IsShowUrl":
                        objData.IsShowUrl = !objData.IsShowUrl;
                        break;
                    default:
                        break;
                }

                await _db.SaveChangesAsync();
                return CreateResponse(objData as TblGecategoryMater, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception)
            {

                return null;

            }
        }
    }
}
