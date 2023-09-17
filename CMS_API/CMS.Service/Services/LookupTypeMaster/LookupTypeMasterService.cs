using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Service.Services.LookupTypeMaster
{
    public class LookupTypeMasterService : BaseService, ILookupTypeMasterService
    {
        DB_CMSContext _db;
        public LookupTypeMasterService(DB_CMSContext db, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
        }

        public async Task<ServiceResponse<IEnumerable<TblLookupTypeMaster>>> GetListAsync(IndexModel model)
        {
            ServiceResponse<IEnumerable<Data.Models.TblLookupTypeMaster>> objResult = new ServiceResponse<IEnumerable<Data.Models.TblLookupTypeMaster>>();
            try
            {
                var result = (from lkType in _db.TblLookupTypeMasters
                              where !lkType.IsDelete && (string.IsNullOrEmpty(model.Search) || lkType.Name.Contains(model.Search))
                              orderby (model.OrderByAsc && model.OrderBy == "Name" ? lkType.Name : "") ascending
                              orderby (!model.OrderByAsc && model.OrderBy == "Name" ? lkType.Name : "") descending
                              select lkType);
                objResult.TotalRecord = result.Count();

                objResult.Data = await result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue).ToListAsync();

                if (result != null)
                {

                    return CreateResponse(objResult.Data as IEnumerable<Data.Models.TblLookupTypeMaster>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<TblLookupTypeMaster>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
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
        public ServiceResponse<TblLookupTypeMaster> GetById(string id)
        {
            ServiceResponse<TblLookupTypeMaster> ObjResponse = new ServiceResponse<TblLookupTypeMaster>();
            try
            {

                var result = _db.TblLookupTypeMasters.FirstOrDefault(x => x.Id == long.Parse(_security.DecryptData(id)) && x.IsActive.Value);
                if (result != null)
                {

                    return CreateResponse(result, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<TblLookupTypeMaster>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {
                ObjResponse = CreateResponse<TblLookupTypeMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblLookupTypeMaster>> Save(LookupTypeMasterViewModel model)
        {
            try
            {
                TblLookupTypeMaster objData = new TblLookupTypeMaster();
                objData.Name = model.Name;
                objData.SortOrder = model.SortOrder;
                objData.IsActive = true;
                objData.CreatedBy = _loginUserDetail.UserId.Value;
                var roletype = await _db.TblLookupTypeMasters.AddAsync(objData);
                _db.SaveChanges();
                return CreateResponse(objData, "Added", true);

            }
            catch (Exception ex)
            {

                return CreateResponse<TblLookupTypeMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblLookupTypeMaster>> Edit(string id, LookupTypeMasterViewModel model)
        {
            try
            {
                TblLookupTypeMaster objData = new TblLookupTypeMaster();

                objData = _db.TblLookupTypeMasters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(id)));
                objData.Name = model.Name;
                objData.SortOrder = model.SortOrder;
                objData.ModifiedBy = _loginUserDetail.UserId.Value;
                var roletype = _db.TblLookupTypeMasters.Update(objData);
                _db.SaveChanges();
                return CreateResponse(objData, "Updated", true);

            }
            catch (Exception ex)
            {
                return CreateResponse<TblLookupTypeMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }

        }

        public async Task<ServiceResponse<TblLookupTypeMaster>> Delete(string id)
        {
            try
            {
                TblLookupTypeMaster objData = new TblLookupTypeMaster();
                objData = _db.TblLookupTypeMasters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(id)));

                var roletype = _db.TblLookupTypeMasters.Remove(objData);
                await _db.SaveChangesAsync();
                return CreateResponse(objData, "Deleted", true);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
