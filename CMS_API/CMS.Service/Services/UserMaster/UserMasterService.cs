using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.User
{
    public class UserMasterService : BaseService, IUserMasterService
    {
        DB_CMSContext _db;
        private readonly Security _security;
        public UserMasterService(DB_CMSContext db, IConfiguration _configuration)
        {
            _security = new Security(_configuration);
            _db = db;
        }


        public ServiceResponse<IEnumerable<Data.Models.TblUserMaster>> GetList()
        {
            ServiceResponse<IEnumerable<Data.Models.TblUserMaster>> objResult = new ServiceResponse<IEnumerable<Data.Models.TblUserMaster>>();
            try
            {
                var objData = _db.TblUserMasters.ToList();
                objResult = CreateResponse(objData as IEnumerable<Data.Models.TblUserMaster>, "Success", true);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }
        public ServiceResponse<TblUserMaster> GetById(int id)
        {
            ServiceResponse<TblUserMaster> ObjResponse = new ServiceResponse<TblUserMaster>();
            try
            {

                var detail = _db.TblUserMasters.FirstOrDefault(x => x.UserId == id && !x.IsDeleted && x.IsActive.Value);
                ObjResponse = CreateResponse(detail, "Success", true);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<TblUserMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblUserMaster>> Save(UserViewPostModel model)
        {
            try
            {
                TblUserMaster objUser = new TblUserMaster();
                // objUser.RoleId = model.RoleId;
                objUser.FirstName = model.FirstName ?? null;
                objUser.LastName = model.LastName;
                objUser.Email = model.Email;
                objUser.Dob = model.Dob;
                objUser.Mobile = model.Mobile;
                objUser.Password = _security.EncryptData(model.Password);
                objUser.Address = model.Address;
                objUser.ProfilePhoto = model.ProfilePhoto;
                //  objUser.Role = model.Role;
                objUser.RoleId = model.RoleId;
                objUser.IsDeleted = false;
                objUser.IsActive = true;
                objUser.CreatedBy = model.CreatedBy;
                var roletype = await _db.TblUserMasters.AddAsync(objUser);
                _db.SaveChanges();
                return CreateResponse(objUser, "Added", true);


            }
            catch (Exception ex)
            {

                return CreateResponse<TblUserMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblUserMaster>> Edit(int id, UserViewPostModel model)
        {
            try
            {
                TblUserMaster objRole = new TblUserMaster();

                objRole = _db.TblUserMasters.FirstOrDefault(r => r.UserId == id);

                objRole.FirstName = model.FirstName ?? null;
                objRole.LastName = model.LastName;
                objRole.Email = model.Email;
                objRole.Dob = model.Dob;
                objRole.Mobile = model.Mobile;
                objRole.Password = _security.EncryptData(model.Password);
                objRole.Address = model.Address;
                objRole.ProfilePhoto = model.ProfilePhoto;
                // objRole.Role = model.Role;
                objRole.RoleId = model.RoleId;

                objRole.ModifiedBy = model.ModifiedBy;
                var roletype = _db.TblUserMasters.Update(objRole);
                _db.SaveChanges();


                return CreateResponse(objRole, "Updated", true);

            }
            catch (Exception ex)
            {

                return CreateResponse<TblUserMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }

        }


        public async Task<ServiceResponse<TblUserMaster>> Delete(int id)
        {
            try
            {
                TblUserMaster objRole = new TblUserMaster();
                objRole = _db.TblUserMasters.FirstOrDefault(r => r.UserId == id);

                var roletype = _db.TblUserMasters.Remove(objRole);
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
