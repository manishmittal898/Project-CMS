using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Service.Services.RoleType
{
    public class RoleTypeService : BaseService, IRoleTypeService
    {
        private readonly DB_CMSContext _db;
        public RoleTypeService(DB_CMSContext db, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
        }


        public ServiceResponse<IEnumerable<Data.Models.TblRoleType>> GetList()
        {
            ServiceResponse<IEnumerable<Data.Models.TblRoleType>> objResult = new ServiceResponse<IEnumerable<Data.Models.TblRoleType>>();
            try
            {
                List<TblRoleType> objData = _db.TblRoleTypes.ToList();
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

                TblRoleType detail = _db.TblRoleTypes.FirstOrDefault(x => x.RoleId == id && !x.IsDeleted && x.IsActive.Value);
                ObjResponse = CreateResponse(detail, "Success", true);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<TblRoleType>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblRoleType>> Save(RoleTypePostModel model)
        {
            try
            {
                TblRoleType objRole = new TblRoleType
                {
                    // objRole.RoleId = model.RoleId;
                    ParentRoleId = model.ParentRoleId ?? null,
                    RoleName = model.RoleName,
                    RoleLevel = model.RoleLevel,
                    IsDeleted = false,
                    IsActive = true,
                    CreatedBy = model.CreatedBy
                };
                _ = await _db.TblRoleTypes.AddAsync(objRole);
                _ = _db.SaveChanges();
                return CreateResponse(objRole, "Added", true);


            }
            catch (Exception ex)
            {

                return CreateResponse<TblRoleType>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

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
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblRoleType> roletype = _db.TblRoleTypes.Update(objRole);
                _ = await _db.SaveChangesAsync();


                return CreateResponse(objRole, "Updated", true);

            }
            catch (Exception ex)
            {

                return CreateResponse<TblRoleType>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }

        }


        public async Task<ServiceResponse<TblRoleType>> Delete(int id)
        {
            try
            {
                TblRoleType objRole = new TblRoleType();
                objRole = _db.TblRoleTypes.FirstOrDefault(r => r.RoleId == id);
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblRoleType> roletype = _db.TblRoleTypes.Remove(objRole);
                _ = await _db.SaveChangesAsync();
                return CreateResponse(objRole, "Deleted", true);
            }
            catch (Exception)
            {
                return null;

            }


        }
    }
}
