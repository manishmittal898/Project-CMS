using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
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
        public UserMasterService(DB_CMSContext db)
        {
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

                ObjResponse = CreateResponse<TblUserMaster>(null, "Fail", false, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblUserMaster>> Save(UserViewPostModel model)
        {
            try
            {
                TblUserMaster objRole = new TblUserMaster();
                // objRole.RoleId = model.RoleId;
                objRole.FirstName = model.FirstName ?? null;
                objRole.LastName = model.LastName;
                objRole.Email = model.Email;
                objRole.Dob = model.Dob;
                objRole.Mobile = model.Mobile;
                objRole.Password = encryptpass(model.Password);
                objRole.Address = model.Address;
                objRole.ProfilePhoto = model.ProfilePhoto;
                //  objRole.Role = model.Role;
                objRole.RoleId = model.RoleId;
                objRole.IsDeleted = false;
                objRole.IsActive = true;
                objRole.CreatedBy = model.CreatedBy;
                var roletype = await _db.TblUserMasters.AddAsync(objRole);
                _db.SaveChanges();
                return CreateResponse(objRole, "Added", true);


            }
            catch (Exception ex)
            {

                return CreateResponse<TblUserMaster>(null, "Fail", false, ex.Message.ToString());

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
                objRole.Password = encryptpass(model.Password);
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

                return CreateResponse<TblUserMaster>(null, "Fail", false, ex.Message.ToString());

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

        public string encryptpass(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }

    }
}
