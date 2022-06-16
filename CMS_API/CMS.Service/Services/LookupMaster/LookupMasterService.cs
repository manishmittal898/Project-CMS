using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.LookupMaster
{
    public class LookupMasterService : BaseService, ILookupMasterService
    {
        DB_CMSContext _db;
        public LookupMasterService(DB_CMSContext db)
        {
            _db = db;
        }


        public ServiceResponse<IEnumerable<Data.Models.TblLookupMaster>> GetList()
        {
            ServiceResponse<IEnumerable<Data.Models.TblLookupMaster>> objResult = new ServiceResponse<IEnumerable<Data.Models.TblLookupMaster>>();
            try
            {
                var objData = _db.TblLookupMasters.ToList();
                objResult = CreateResponse(objData as IEnumerable<Data.Models.TblLookupMaster>, "Success", true);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }
        public ServiceResponse<TblLookupMaster> GetById(int id)
        {
            ServiceResponse<TblLookupMaster> ObjResponse = new ServiceResponse<TblLookupMaster>();
            try
            {

                var detail = _db.TblLookupMasters.FirstOrDefault(x => x.Id == id && x.IsActive.Value);
                ObjResponse = CreateResponse(detail, "Success", true);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<TblLookupMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblLookupMaster>> Save(LookupMasterViewModel model)
        {
            try
            {
                TblLookupMaster objRole = new TblLookupMaster();
                // objRole.RoleId = model.RoleId;
                objRole.Name = model.Name;
                objRole.SortedOrder = model.SortedOrder;
                objRole.LookUpType = model.LookUpType;

                objRole.IsActive = true;
                objRole.CreatedBy = model.CreatedBy;
                var roletype = await _db.TblLookupMasters.AddAsync(objRole);
                _db.SaveChanges();
                return CreateResponse(objRole, "Added", true);


            }
            catch (Exception ex)
            {

                return CreateResponse<TblLookupMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError , ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblLookupMaster>> Edit(int id, LookupMasterViewModel model)
        {
            try
            {
                TblLookupMaster objRole = new TblLookupMaster();

                objRole = _db.TblLookupMasters.FirstOrDefault(r => r.Id == id);

                objRole.Name = model.Name;
                objRole.SortedOrder = model.SortedOrder;
                objRole.LookUpType = model.LookUpType;

                objRole.ModifiedBy = model.ModifiedBy;
                var roletype = _db.TblLookupMasters.Update(objRole);
                _db.SaveChanges();


                return CreateResponse(objRole, "Updated", true);

            }
            catch (Exception ex)
            {

                return CreateResponse<TblLookupMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }

        }


        public async Task<ServiceResponse<TblLookupMaster>> Delete(int id)
        {
            try
            {
                TblLookupMaster objRole = new TblLookupMaster();
                objRole = _db.TblLookupMasters.FirstOrDefault(r => r.Id == id);

                var roletype = _db.TblLookupMasters.Remove(objRole);
                _db.SaveChangesAsync();
                return CreateResponse(objRole, "Deleted", true);
            }
            catch (Exception ex)
            {

                return null;

            }


        }

    }
}
