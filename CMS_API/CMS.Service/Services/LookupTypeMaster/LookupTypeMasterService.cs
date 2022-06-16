using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
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


        public ServiceResponse<IEnumerable<Data.Models.TblLookupTypeMaster>> GetList()
        {
            ServiceResponse<IEnumerable<Data.Models.TblLookupTypeMaster>> objResult = new ServiceResponse<IEnumerable<Data.Models.TblLookupTypeMaster>>();
            try
            {
                var objData = _db.TblLookupTypeMasters.ToList();
                objResult = CreateResponse(objData as IEnumerable<Data.Models.TblLookupTypeMaster>, "Success", true);
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

                var detail = _db.TblLookupTypeMasters.FirstOrDefault(x => x.Id == id && x.IsActive.Value);
                ObjResponse = CreateResponse(detail, "Success", true);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<TblLookupTypeMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblLookupTypeMaster>> Save(LookupTypeMasterViewModel model)
        {
            try
            {
                TblLookupTypeMaster objRole = new TblLookupTypeMaster();
                // objRole.RoleId = model.RoleId;
                objRole.Name = model.Name;
             

                objRole.IsActive = true;
                objRole.CreatedBy = model.CreatedBy;
                var roletype = await _db.TblLookupTypeMasters.AddAsync(objRole);
                _db.SaveChanges();
                return CreateResponse(objRole, "Added", true);


            }
            catch (Exception ex)
            {

                return CreateResponse<TblLookupTypeMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError , ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblLookupTypeMaster>> Edit(int id, LookupTypeMasterViewModel model)
        {
            try
            {
                TblLookupTypeMaster objRole = new TblLookupTypeMaster();

                objRole = _db.TblLookupTypeMasters.FirstOrDefault(r => r.Id == id);

                objRole.Name = model.Name;          
                objRole.ModifiedBy = model.ModifiedBy;
                var roletype = _db.TblLookupTypeMasters.Update(objRole);
                _db.SaveChanges();


                return CreateResponse(objRole, "Updated", true);

            }
            catch (Exception ex)
            {

                return CreateResponse<TblLookupTypeMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError , ex.Message.ToString());

            }

        }


        public async Task<ServiceResponse<TblLookupTypeMaster>> Delete(int id)
        {
            try
            {
                TblLookupTypeMaster objRole = new TblLookupTypeMaster();
                objRole = _db.TblLookupTypeMasters.FirstOrDefault(r => r.Id == id);

                var roletype = _db.TblLookupTypeMasters.Remove(objRole);
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
