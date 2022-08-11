using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.LookupTypeMaster
{
    public class LookupTypeMasterService : BaseService, ILookupTypeMasterService
    {
        DB_CMSContext _db;
        public LookupTypeMasterService(DB_CMSContext db)
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
        public ServiceResponse<TblLookupTypeMaster> GetById(int id)
        {
            ServiceResponse<TblLookupTypeMaster> ObjResponse = new ServiceResponse<TblLookupTypeMaster>();
            try
            {

                var result = _db.TblLookupTypeMasters.FirstOrDefault(x => x.Id == id && x.IsActive.Value);
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
                TblLookupTypeMaster objRole = new TblLookupTypeMaster();

                objRole.Name = model.Name;

                objRole.IsActive = true;
                objRole.CreatedBy = _loginUserDetail.UserId.Value;
                var roletype = await _db.TblLookupTypeMasters.AddAsync(objRole);
                _db.SaveChanges();
                return CreateResponse(objRole, "Added", true);


            }
            catch (Exception ex)
            {

                return CreateResponse<TblLookupTypeMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblLookupTypeMaster>> Edit(int id, LookupTypeMasterViewModel model)
        {
            try
            {
                TblLookupTypeMaster objRole = new TblLookupTypeMaster();

                objRole = _db.TblLookupTypeMasters.FirstOrDefault(r => r.Id == id);

                objRole.Name = model.Name;
                objRole.ModifiedBy = _loginUserDetail.UserId.Value;
                var roletype = _db.TblLookupTypeMasters.Update(objRole);
                _db.SaveChanges();


                return CreateResponse(objRole, "Updated", true);

            }
            catch (Exception ex)
            {

                return CreateResponse<TblLookupTypeMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }

        }

        public async Task<ServiceResponse<TblLookupTypeMaster>> Delete(long id)
        {
            try
            {
                TblLookupTypeMaster objRole = new TblLookupTypeMaster();
                objRole = _db.TblLookupTypeMasters.FirstOrDefault(r => r.Id == id);

                var roletype = _db.TblLookupTypeMasters.Remove(objRole);
                await _db.SaveChangesAsync();
                return CreateResponse(objRole, "Deleted", true);
            }
            catch (Exception ex)
            {

                return null;

            }


        }



    }
}
