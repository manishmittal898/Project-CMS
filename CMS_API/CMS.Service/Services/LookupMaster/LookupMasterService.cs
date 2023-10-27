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

namespace CMS.Service.Services.LookupMaster
{
    public class LookupMasterService : BaseService, ILookupMasterService
    {
        private readonly DB_CMSContext _db;
        private readonly FileHelper _fileHelper;
        public LookupMasterService(DB_CMSContext db, IHostingEnvironment environment, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
            _fileHelper = new FileHelper(environment);
        }


        public async Task<ServiceResponse<IEnumerable<LookupMasterViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<LookupMasterViewModel>> objResult = new ServiceResponse<IEnumerable<LookupMasterViewModel>>();
            try
            {
                long TypeId = 0;
                if (model.AdvanceSearchModel != null && model.AdvanceSearchModel.Count > 0 && model.AdvanceSearchModel.ContainsKey("typeId"))
                {
                    _ = model.AdvanceSearchModel.TryGetValue("typeId", out object typeId);
                    TypeId = Convert.ToInt64(_security.DecryptData(typeId.ToString()));

                }

                IQueryable<TblLookupMaster> result = from lkType in _db.TblLookupMasters
                                                     where lkType.LookUpType.Value.Equals(TypeId) && !lkType.IsDelete && (string.IsNullOrEmpty(model.Search) || lkType.Name.Contains(model.Search))
                                                     select lkType;
                result = model.OrderBy switch
                {
                    "Name" => model.OrderByAsc ? (from orderData in result orderby orderData.Name ascending select orderData) : (from orderData in result orderby orderData.Name descending select orderData),
                    "CreatedOn" => model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData),
                    _ => model.OrderByAsc ? (from orderData in result orderby orderData.SortedOrder ascending select orderData) : (from orderData in result orderby orderData.SortedOrder descending select orderData),
                };
                objResult.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new LookupMasterViewModel
                                        {
                                            Id = _security.EncryptData(x.Id),
                                            Name = x.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath(ServiceExtension.getSizePath(ImageSize.Medium)) : null,
                                            SortedOrder = x.SortedOrder.Value,
                                            LookUpType = x.LookUpType.HasValue ? _security.EncryptData(x.LookUpType.Value) : null,
                                            LookUpTypeName = x.LookUpTypeNavigation.Name,
                                            IsSubLookup = x.LookUpTypeNavigation.IsSubLookup,
                                            Value = x.Value,
                                            CreatedBy = x.CreatedBy,
                                            CreatedOn = x.CreatedOn,
                                            ModifiedBy = x.ModifiedBy,
                                            ModifiedOn = x.ModifiedOn,
                                            IsActive = x.IsActive.Value,
                                            IsDelete = x.IsDelete

                                        }).ToListAsync();

                return result != null
                    ? CreateResponse(objResult.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok, TotalRecord: objResult.TotalRecord)
                    : CreateResponse<IEnumerable<LookupMasterViewModel>>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound, TotalRecord: 0);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }
        public ServiceResponse<LookupMasterViewModel> GetById(string id)
        {
            ServiceResponse<LookupMasterViewModel> ObjResponse = new ServiceResponse<LookupMasterViewModel>();
            try
            {
                LookupMasterViewModel detail = _db.TblLookupMasters.Where(x => x.Id == long.Parse(_security.DecryptData(id)) && x.IsActive.Value && x.IsDelete == false).Select(X => new LookupMasterViewModel
                {
                    Id = _security.EncryptData(X.Id),
                    Name = X.Name,
                    ImagePath = !string.IsNullOrEmpty(X.ImagePath) ? X.ImagePath.ToAbsolutePath() : null,
                    SortedOrder = X.SortedOrder,
                    LookUpType = X.LookUpType.HasValue ? _security.EncryptData(X.LookUpType.Value) : null,
                    LookUpTypeName = X.LookUpTypeNavigation.Name,
                    IsSubLookup = X.LookUpTypeNavigation.IsSubLookup,
                    Value = X.Value,
                    CreatedBy = X.CreatedBy,
                    CreatedOn = X.CreatedOn,
                    ModifiedBy = X.ModifiedBy,
                    ModifiedOn = X.ModifiedOn,
                    IsActive = X.IsActive,
                    IsDelete = X.IsDelete,

                }).FirstOrDefault();

                ObjResponse = detail != null
                    ? CreateResponse(detail, ResponseMessage.Success, true, (int)ApiStatusCode.Ok)
                    : CreateResponse<LookupMasterViewModel>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound);

            }
            catch (Exception ex)
            {
                ObjResponse = CreateResponse<LookupMasterViewModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblLookupMaster>> Save(LookupMasterPostModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.Id))
                {

                    TblLookupMaster objData = _db.TblLookupMasters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(model.Id)));
                    objData.Name = model.Name;
                    objData.Value = !string.IsNullOrEmpty(model.Value) ? model.Value.Trim() : null;
                    objData.SortedOrder = model.SortedOrder;
                    objData.LookUpType = model.LookUpType != null ? long.Parse(_security.DecryptData(model.LookUpType)) : null as Nullable<long>;
                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {
                        objData.ImagePath = !string.IsNullOrEmpty(objData.ImagePath) && model.ImagePath.Contains(objData.ImagePath.Replace("\\", "/")) ? objData.ImagePath : await _fileHelper.Save(model.ImagePath, FilePaths.Lookup);
                    }
                    else
                    {
                        _ = _fileHelper.Delete(objData.ImagePath);
                        objData.ImagePath = null;
                    }

                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblLookupMaster> roletype = _db.TblLookupMasters.Update(objData);
                    _ = _db.SaveChanges();
                    return CreateResponse<TblLookupMaster>(objData, ResponseMessage.Update, true, (int)ApiStatusCode.Ok);

                }
                else
                {

                    TblLookupMaster objData = new TblLookupMaster
                    {
                        Name = model.Name,
                        Value = !string.IsNullOrEmpty(model.Value) && model.Value.Trim().Length > 0 ? model.Value.Trim() : null,
                        SortedOrder = model.SortedOrder,
                        LookUpType = model.LookUpType != null ? long.Parse(_security.DecryptData(model.LookUpType.ToString())) : null as Nullable<long>,
                        ImagePath = string.IsNullOrEmpty(model.ImagePath) ? null : await _fileHelper.Save(model.ImagePath, FilePaths.Lookup),

                        IsActive = true,
                        CreatedBy = _loginUserDetail.UserId.Value,
                        ModifiedBy = _loginUserDetail.UserId.Value
                    };
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblLookupMaster> roletype = await _db.TblLookupMasters.AddAsync(objData);
                    _ = _db.SaveChanges();
                    return CreateResponse<TblLookupMaster>(objData, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

                }
            }
            catch (Exception ex)
            {
                return CreateResponse<TblLookupMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }


        public async Task<ServiceResponse<TblLookupMaster>> Delete(string id)
        {
            try
            {
                TblLookupMaster objData = new TblLookupMaster();
                objData = _db.TblLookupMasters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(id)));
                objData.IsDelete = true;
                objData.ModifiedBy = _loginUserDetail.UserId.Value;
                objData.ModifiedOn = DateTime.Now;
                // _db.TblLookupMasters.Update(objData);
                _ = await _db.SaveChangesAsync();
                return CreateResponse(objData, ResponseMessage.Delete, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {
                return CreateResponse<TblLookupMaster>(null, ResponseMessage.Fail, true, (int)ApiStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<ServiceResponse<TblLookupMaster>> ActiveStatusUpdate(string id)
        {
            try
            {
                TblLookupMaster objData = new TblLookupMaster();
                objData = _db.TblLookupMasters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(id)));

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
