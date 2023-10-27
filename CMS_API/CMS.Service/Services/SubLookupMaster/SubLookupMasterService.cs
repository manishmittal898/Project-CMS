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

namespace CMS.Service.Services.SubLookupMaster
{
    public class SubLookupMasterService : BaseService, ISubLookupMasterService
    {
        private readonly DB_CMSContext _db;
        private readonly FileHelper _fileHelper;

        public SubLookupMasterService(DB_CMSContext db, IHostingEnvironment environment, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
            _fileHelper = new FileHelper(environment);
        }

        public async Task<ServiceResponse<IEnumerable<SubLookupMasterViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<SubLookupMasterViewModel>> objResult = new ServiceResponse<IEnumerable<SubLookupMasterViewModel>>();
            try
            {
                long LookupId = 0;
                if (model.AdvanceSearchModel != null && model.AdvanceSearchModel.Count > 0 && model.AdvanceSearchModel.ContainsKey("lookupId"))
                {
                    _ = model.AdvanceSearchModel.TryGetValue("lookupId", out object lookupId);
                    LookupId = Convert.ToInt64(_security.DecryptData(lookupId.ToString()));

                }


                IQueryable<TblSubLookupMaster> result = from lkType in _db.TblSubLookupMasters
                                                        where lkType.LookUpId.Equals(LookupId) && !lkType.IsDeleted && (string.IsNullOrEmpty(model.Search) || lkType.Name.Contains(model.Search))
                                                        select lkType;
                result = model.OrderBy switch
                {
                    "Name" => model.OrderByAsc ? (from orderData in result orderby orderData.Name ascending select orderData) : (from orderData in result orderby orderData.Name descending select orderData),
                    "CreatedOn" => model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData),
                    _ => model.OrderByAsc ? (from orderData in result orderby orderData.SortedOrder ascending select orderData) : (from orderData in result orderby orderData.SortedOrder descending select orderData),
                };
                objResult.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from X in result
                                        select new SubLookupMasterViewModel
                                        {
                                            Id = _security.EncryptData(X.Id),
                                            Name = X.Name,
                                            ImagePath = !string.IsNullOrEmpty(X.ImagePath) ? X.ImagePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Medium)) : null,
                                            SortedOrder = X.SortedOrder,
                                            LookUpId = _security.EncryptData(X.LookUpId),
                                            LookUpName = X.LookUp.Name,
                                            CreatedBy = X.CreatedBy,
                                            CreatedOn = X.CreatedOn,
                                            ModifiedBy = X.ModifiedBy,
                                            ModifiedOn = X.ModifiedOn,
                                            IsActive = X.IsActive,
                                            IsDeleted = X.IsDeleted,

                                        }).ToListAsync();

                return result != null
                    ? CreateResponse(objResult.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok, TotalRecord: objResult.TotalRecord)
                    : CreateResponse<IEnumerable<SubLookupMasterViewModel>>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound, TotalRecord: 0);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }
        public ServiceResponse<SubLookupMasterViewModel> GetById(string id)
        {
            ServiceResponse<SubLookupMasterViewModel> ObjResponse = new ServiceResponse<SubLookupMasterViewModel>();
            try
            {
                SubLookupMasterViewModel detail = _db.TblSubLookupMasters.Where(x => x.Id == long.Parse(_security.DecryptData(id)) && x.IsActive.Value && x.IsDeleted == false).Select(X => new SubLookupMasterViewModel
                {
                    Id = _security.EncryptData(X.Id),
                    Name = X.Name,
                    ImagePath = !string.IsNullOrEmpty(X.ImagePath) ? X.ImagePath.ToAbsolutePath() : null,
                    SortedOrder = X.SortedOrder,
                    LookUpId = _security.EncryptData(X.LookUpId),
                    LookUpName = X.LookUp.Name,
                    CreatedBy = X.CreatedBy,
                    CreatedOn = X.CreatedOn,
                    ModifiedBy = X.ModifiedBy,
                    ModifiedOn = X.ModifiedOn,
                    IsActive = X.IsActive,
                    IsDeleted = X.IsDeleted,

                }).FirstOrDefault();

                ObjResponse = detail != null
                    ? CreateResponse(detail, ResponseMessage.Success, true, (int)ApiStatusCode.Ok)
                    : CreateResponse<SubLookupMasterViewModel>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound);

            }
            catch (Exception ex)
            {
                ObjResponse = CreateResponse<SubLookupMasterViewModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblSubLookupMaster>> Save(SubLookupMasterPostModel model)
        {
            try
            {

                if (!string.IsNullOrEmpty(model.Id))
                {

                    TblSubLookupMaster objData = _db.TblSubLookupMasters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(model.Id)));

                    objData.Name = model.Name;
                    objData.SortedOrder = model.SortedOrder;
                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {

                        objData.ImagePath = !string.IsNullOrEmpty(objData.ImagePath) && model.ImagePath.Contains(objData.ImagePath.Replace("\\", "/")) ? objData.ImagePath : await _fileHelper.Save(model.ImagePath, FilePaths.SubLookup);
                    }
                    else
                    {
                        _ = _fileHelper.Delete(objData.ImagePath);
                        objData.ImagePath = null;
                    }


                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblSubLookupMaster> roletype = _db.TblSubLookupMasters.Update(objData);
                    _ = _db.SaveChanges();
                    return CreateResponse<TblSubLookupMaster>(objData, ResponseMessage.Update, true, (int)ApiStatusCode.Ok);

                }
                else
                {

                    TblSubLookupMaster objData = new TblSubLookupMaster
                    {
                        Name = model.Name,
                        SortedOrder = model.SortedOrder,
                        LookUpId = long.Parse(_security.DecryptData(model.LookUpId)),
                        ImagePath = string.IsNullOrEmpty(model.ImagePath) ? null : await _fileHelper.Save(model.ImagePath, FilePaths.Lookup),
                        IsActive = true,
                        CreatedBy = _loginUserDetail.UserId.Value,
                        ModifiedBy = _loginUserDetail.UserId.Value
                    };
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblSubLookupMaster> roletype = await _db.TblSubLookupMasters.AddAsync(objData);
                    _ = _db.SaveChanges();
                    return CreateResponse<TblSubLookupMaster>(objData, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

                }


            }
            catch (Exception ex)
            {

                return CreateResponse<TblSubLookupMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }


        public async Task<ServiceResponse<TblSubLookupMaster>> Delete(string id)
        {
            try
            {
                TblSubLookupMaster objData = new TblSubLookupMaster();
                objData = _db.TblSubLookupMasters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(id)));
                objData.IsDeleted = !objData.IsDeleted;
                objData.ModifiedBy = _loginUserDetail.UserId.Value;
                objData.ModifiedOn = DateTime.Now;
                //  _db.TblSubLookupMasters.Update(objData);
                _ = await _db.SaveChangesAsync();
                return CreateResponse(objData, ResponseMessage.Delete, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                return CreateResponse<TblSubLookupMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message);
            }


        }

        public async Task<ServiceResponse<TblSubLookupMaster>> ActiveStatusUpdate(string id)
        {
            try
            {
                TblSubLookupMaster objData = new TblSubLookupMaster();
                objData = _db.TblSubLookupMasters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(id)));

                objData.IsActive = !objData.IsActive;
                _ = await _db.SaveChangesAsync();
                return CreateResponse(objData, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception)
            {

                return null;

            }
        }
    }
}
