using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.RoleType
{
    public class RoleTypeService : BaseService, IRoleTypeService
    {
        DB_CMSContext _db;
        public RoleTypeService(DB_CMSContext db)
        {
            _db = db;
        }


        public ServiceResponse<IEnumerable<Data.Models.TblRoleType>> GetList()
        {
            ServiceResponse<IEnumerable<Data.Models.TblRoleType>> objResult = new ServiceResponse<IEnumerable<Data.Models.TblRoleType>>();
            try
            {
                var objData = _db.TblRoleTypes.ToList();
                objResult = CreateResponse(objData as IEnumerable<Data.Models.TblRoleType>, "Success", true);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }
        public ServiceResponse<TblRoleType> GetById(int id)
        {
            ServiceResponse<TblRoleType> ObjResponse = new ServiceResponse<TblRoleType>();
            try
            {

                var detail = _db.TblRoleTypes.FirstOrDefault(x => x.RoleId == id && !x.IsDeleted && x.IsActive.Value);
                ObjResponse = CreateResponse(detail, "Success", true);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<TblRoleType>(null, "Fail", false, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblRoleType>> Save(RoleTypePostModel model)
        {
            try
            {
                TblRoleType objRole = new TblRoleType();
                // objRole.RoleId = model.RoleId;
                objRole.ParentRoleId = model.ParentRoleId ?? null;
                objRole.RoleName = model.RoleName;
                objRole.RoleLevel = model.RoleLevel;
                objRole.IsDeleted = false;
                objRole.IsActive = true;
                objRole.CreatedBy = model.CreatedBy;
                 await _db.TblRoleTypes.AddAsync(objRole);
                _db.SaveChanges();
                return CreateResponse(objRole, "Added", true);


            }
            catch (Exception ex)
            {

                return CreateResponse<TblRoleType>(null, "Fail", false, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblRoleType>> Edit(int id, RoleTypePostModel model)
        {
            try
            {
                TblRoleType objRole = new TblRoleType();

                objRole = _db.TblRoleTypes.FirstOrDefault(r => r.RoleId == id);

                objRole.RoleName = model.RoleName;
                objRole.RoleLevel = model.RoleLevel;

                objRole.ModifiedBy = model.ModifiedBy;
                var roletype =  _db.TblRoleTypes.Update(objRole);
              await  _db.SaveChangesAsync();


                return CreateResponse(objRole, "Updated", true);

            }
            catch (Exception ex)
            {

                return CreateResponse<TblRoleType>(null, "Fail", false, ex.Message.ToString());

            }

        }


        public async Task<ServiceResponse<TblRoleType>> Delete(int id)
        {
            try
            {
                TblRoleType objRole = new TblRoleType();
                objRole = _db.TblRoleTypes.FirstOrDefault(r => r.RoleId == id);
            
                            var roletype =  _db.TblRoleTypes.Remove(objRole);
                _db.SaveChangesAsync();
                return CreateResponse(objRole, "Deleted", true);
            }
            catch(Exception ex)
            {
                return null;

            }

           
        }
    }
}
