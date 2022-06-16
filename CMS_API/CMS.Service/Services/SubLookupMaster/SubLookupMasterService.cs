using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
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
        public SubLookupMasterService(DB_CMSContext db)
        {
            _db = db;
        }


        public ServiceResponse<IEnumerable<Data.Models.TblSubLookupMaster>> GetList()
        {
            ServiceResponse<IEnumerable<Data.Models.TblSubLookupMaster>> objResult = new ServiceResponse<IEnumerable<Data.Models.TblSubLookupMaster>>();
            try
            {
                var objData = _db.TblLookupTypeMasters.ToList();
                objResult = CreateResponse(objData as IEnumerable<Data.Models.TblSubLookupMaster>, "Success", true);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }
        public ServiceResponse<TblSubLookupMaster> GetById(int id)
        {
            ServiceResponse<TblSubLookupMaster> ObjResponse = new ServiceResponse<TblSubLookupMaster>();
            try
            {

                var detail = _db.TblSubLookupMasters.FirstOrDefault(x => x.Id == id && x.IsActive.Value);
                ObjResponse = CreateResponse(detail, "Success", true);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<TblSubLookupMaster>(null, "Fail", false, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblSubLookupMaster>> Save(SubLookupMasterViewModel model)
        {
            try
            {
                TblSubLookupMaster objRole = new TblSubLookupMaster();
                // objRole.RoleId = model.RoleId;
                objRole.Name = model.Name;
                objRole.SortedOrder = model.SortedOrder;
                objRole.LookUpId = model.LookUpId;
              

                objRole.IsActive = true;
                objRole.CreatedBy = model.CreatedBy;
                var roletype = await _db.TblSubLookupMasters.AddAsync(objRole);
                _db.SaveChanges();
                return CreateResponse(objRole, "Added", true);


            }
            catch (Exception ex)
            {

                return CreateResponse<TblSubLookupMaster>(null, "Fail", false, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblSubLookupMaster>> Edit(int id, SubLookupMasterViewModel model)
        {
            try
            {
                TblSubLookupMaster objRole = new TblSubLookupMaster();

                objRole = _db.TblSubLookupMasters.FirstOrDefault(r => r.Id == id);

                objRole.Name = model.Name;
                objRole.SortedOrder = model.SortedOrder;
                objRole.LookUpId = model.LookUpId;

                objRole.ModifiedBy = model.ModifiedBy;
                var roletype = _db.TblSubLookupMasters.Update(objRole);
                _db.SaveChanges();


                return CreateResponse(objRole, "Updated", true);

            }
            catch (Exception ex)
            {

                return CreateResponse<TblSubLookupMaster>(null, "Fail", false, ex.Message.ToString());

            }

        }


        public async Task<ServiceResponse<TblSubLookupMaster>> Delete(int id)
        {
            try
            {
                TblSubLookupMaster objRole = new TblSubLookupMaster();
                objRole = _db.TblSubLookupMasters.FirstOrDefault(r => r.Id == id);

                var roletype = _db.TblSubLookupMasters.Remove(objRole);
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
