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

namespace CMS.Service.Services.SubLookupMaster
{
    public class SubLookupMasterService : BaseService, ISubLookupMasterService
    {
        DB_CMSContext _db;
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
                    model.AdvanceSearchModel.TryGetValue("lookupId", out object lookupId);
                    LookupId = Convert.ToInt64(lookupId.ToString());

                }


                var result = (from lkType in _db.TblSubLookupMasters
                              where lkType.LookUpId.Equals(LookupId) && !lkType.IsDeleted && (string.IsNullOrEmpty(model.Search) || lkType.Name.Contains(model.Search))
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
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.SortedOrder ascending select orderData) : (from orderData in result orderby orderData.SortedOrder descending select orderData);
                        break;
                }
                objResult.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new SubLookupMasterViewModel
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath() : null,
                                            SortedOrder = x.SortedOrder.Value,
                                            LookUpId = x.LookUpId,
                                            LookUpName = x.LookUp.Name,
                                            CreatedBy = x.CreatedBy,
                                            CreatedOn = x.CreatedOn,
                                            ModifiedBy = x.ModifiedBy,
                                            ModifiedOn = x.ModifiedOn,
                                            IsActive = x.IsActive.Value,
                                            IsDeleted = x.IsDeleted

                                        }).ToListAsync();

                if (result != null)
                {

                    return CreateResponse(objResult.Data as IEnumerable<SubLookupMasterViewModel>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<SubLookupMasterViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
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
        public ServiceResponse<SubLookupMasterViewModel> GetById(long id)
        {
            ServiceResponse<SubLookupMasterViewModel> ObjResponse = new ServiceResponse<SubLookupMasterViewModel>();
            try
            {
                var detail = _db.TblSubLookupMasters.Select(X => new SubLookupMasterViewModel
                {
                    Id = X.Id,
                    Name = X.Name,
                    ImagePath = !string.IsNullOrEmpty(X.ImagePath) ? X.ImagePath.ToAbsolutePath() : null,
                    SortedOrder = X.SortedOrder,
                    LookUpId = X.LookUpId,
                    LookUpName = X.LookUp.Name,
                    CreatedBy = X.CreatedBy,
                    CreatedOn = X.CreatedOn,
                    ModifiedBy = X.ModifiedBy,
                    ModifiedOn = X.ModifiedOn,
                    IsActive = X.IsActive,
                    IsDeleted = X.IsDeleted,

                }).FirstOrDefault(x => x.Id == id && x.IsActive.Value && x.IsDeleted == false);

                if (detail != null)
                {
                    ObjResponse = CreateResponse(detail, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);

                }
                else
                {
                    ObjResponse = CreateResponse<SubLookupMasterViewModel>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound);

                }

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

                if (model.Id > 0)
                {

                    TblSubLookupMaster objData = _db.TblSubLookupMasters.FirstOrDefault(r => r.Id == model.Id);

                    objData.Name = model.Name;
                    objData.SortedOrder = model.SortedOrder;
                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {

                        objData.ImagePath = !string.IsNullOrEmpty(objData.ImagePath) && model.ImagePath.Contains(objData.ImagePath.Replace("\\", "/")) ? objData.ImagePath : await _fileHelper.Save(model.ImagePath, FilePaths.SubLookup);
                    }
                    else
                    {
                        _fileHelper.Delete(objData.ImagePath);
                        objData.ImagePath = null;
                    }


                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    var roletype = _db.TblSubLookupMasters.Update(objData);
                    _db.SaveChanges();
                    return CreateResponse<TblSubLookupMaster>(objData, ResponseMessage.Update, true, (int)ApiStatusCode.Ok);

                }
                else
                {

                    TblSubLookupMaster objData = new TblSubLookupMaster();

                    objData.Name = model.Name;
                    objData.SortedOrder = model.SortedOrder;
                    objData.LookUpId = model.LookUpId;
                    objData.ImagePath = string.IsNullOrEmpty(model.ImagePath) ? null : await _fileHelper.Save(model.ImagePath, FilePaths.Lookup);
                    objData.IsActive = true;
                    objData.CreatedBy = _loginUserDetail.UserId.Value;
                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    var roletype = await _db.TblSubLookupMasters.AddAsync(objData);
                    _db.SaveChanges();
                    return CreateResponse<TblSubLookupMaster>(objData, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

                }


            }
            catch (Exception ex)
            {

                return CreateResponse<TblSubLookupMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }


        public async Task<ServiceResponse<TblSubLookupMaster>> Delete(long id)
        {
            try
            {
                TblSubLookupMaster objData = new TblSubLookupMaster();
                objData = _db.TblSubLookupMasters.FirstOrDefault(r => r.Id == id);
                objData.IsDeleted = !objData.IsDeleted;
                objData.ModifiedBy = _loginUserDetail.UserId.Value;
                objData.ModifiedOn = DateTime.Now;
                //  _db.TblSubLookupMasters.Update(objData);
                await _db.SaveChangesAsync();
                return CreateResponse(objData, ResponseMessage.Delete, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                return CreateResponse<TblSubLookupMaster>(null, ResponseMessage.Fail, false, ((int)ApiStatusCode.InternalServerError), ex.Message);
            }


        }

        public async Task<ServiceResponse<TblSubLookupMaster>> ActiveStatusUpdate(long id)
        {
            try
            {
                TblSubLookupMaster objData = new TblSubLookupMaster();
                objData = _db.TblSubLookupMasters.FirstOrDefault(r => r.Id == id);

                objData.IsActive = !objData.IsActive;
                await _db.SaveChangesAsync();
                return CreateResponse(objData as TblSubLookupMaster, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return null;

            }
        }
    }
}
