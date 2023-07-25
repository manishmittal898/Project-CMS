using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Services.ProductMaster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;

namespace CMS.Service.Services.User
{
    public class UserMasterService : BaseService, IUserMasterService
    {
        DB_CMSContext _db;
        private readonly Security _security;
        public UserMasterService(DB_CMSContext db, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
        }



        public async Task<ServiceResponse<IEnumerable<UserMasterViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<UserMasterViewModel>> objResult = new ServiceResponse<IEnumerable<UserMasterViewModel>>();
            try
            {
                int roleId = 0;
                if (model.AdvanceSearchModel != null && model.AdvanceSearchModel.Count > 0 && model.AdvanceSearchModel.ContainsKey("roleId"))
                {
                    model.AdvanceSearchModel.TryGetValue("roleId", out object role);
                    roleId = Convert.ToInt32(role.ToString());

                }

                var result = (from user in _db.TblUserMasters
                              where !user.IsDeleted && (string.IsNullOrEmpty(model.Search) || user.FirstName.Contains(model.Search) || user.LastName.Contains(model.Search) || user.Email.Contains(model.Search) || user.Mobile.Contains(model.Search)) && ((roleId == 0 && user.RoleId != (int)RoleEnum.Customer) || user.RoleId == roleId)
                              select user);
                switch (model.OrderBy)
                {
                    case "Name":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.FirstName ascending select orderData) : (from orderData in result orderby orderData.FirstName descending select orderData);
                        break;
                    case "CreatedOn":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.ModifiedOn ascending select orderData) : (from orderData in result orderby orderData.ModifiedOn descending select orderData);
                        break;
                }
                objResult.TotalRecord = result.Count();
                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new UserMasterViewModel
                                        {
                                            UserId = x.UserId,
                                            FirstName = x.FirstName,
                                            LastName = x.LastName,
                                            Email = x.Email,
                                            Mobile = x.Mobile,
                                            Dob = x.Dob,
                                            Address = x.Address,
                                            GenderId = x.GenderId ?? null,
                                            Gender = x.Gender.Name ?? null,
                                            RoleId = x.RoleId,
                                            Role = x.Role.RoleName,
                                            ProfilePhoto = !string.IsNullOrEmpty(x.ProfilePhoto) ? x.ProfilePhoto.ToAbsolutePath() : null,
                                            CreatedBy = x.CreatedBy,
                                            CreatedOn = x.CreatedOn,
                                            ModifiedBy = x.ModifiedBy,
                                            ModifiedOn = x.ModifiedOn,
                                            IsActive = x.IsActive.Value,
                                            IsDeleted = x.IsDeleted,


                                        }).ToListAsync();

                if (result != null)
                {

                    return CreateResponse(objResult.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<UserMasterViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
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

        public async Task<ServiceResponse<UserMasterViewModel>> GetById(long id)
        {
            ServiceResponse<UserMasterViewModel> ObjResponse = new ServiceResponse<UserMasterViewModel>();
            try
            {
                ObjResponse.Data = await _db.TblUserMasters.Include(r => r.Role).Include(add => add.TblUserAddressMasterUsers).ThenInclude(st => st.State).ThenInclude(add => add.TblUserAddressMasterAddressTypeNavigations).Where(
                           x => x.UserId == id && x.IsDeleted == false && x.IsActive.Value == true).Select(x => new UserMasterViewModel
                           {

                               UserId = x.UserId,
                               FirstName = x.FirstName ?? "N/A",
                               LastName = x.LastName ?? "N/A",
                               Email = x.Email ?? "N/A",
                               Mobile = x.Mobile ?? "N/A",
                               Dob = x.Dob ?? null,
                               Address = x.Address ?? "N/A",
                               GenderId = x.GenderId ?? null,
                               Gender = x.Gender.Name ?? null,
                               RoleId = x.RoleId,
                               Role = x.Role.RoleName,
                               ProfilePhoto = !string.IsNullOrEmpty(x.ProfilePhoto) ? x.ProfilePhoto.ToAbsolutePath() : null,
                               CreatedBy = x.CreatedBy,
                               CreatedOn = x.CreatedOn,
                               ModifiedBy = x.ModifiedBy,
                               ModifiedOn = x.ModifiedOn,
                               IsActive = x.IsActive.Value,
                               IsDeleted = x.IsDeleted,
                               CustomerAddresses = x.TblUserAddressMasterUsers.Count > 0 ? x.TblUserAddressMasterUsers.Where(xs => !x.IsDeleted).Select(ad => new UserAddressMasterViewModel
                               {
                                   Id = ad.Id,
                                   UserId = ad.UserId,
                                   FullName = ad.FullName,
                                   Mobile = ad.Mobile,
                                   BuildingNumber = ad.BuildingNumber,
                                   Address = ad.Address,
                                   PinCode = ad.PinCode,
                                   Landmark = ad.Landmark,
                                   City = ad.City,
                                   StateId = ad.StateId,
                                   State = ad.State.Name,
                                   AddressType = ad.AddressType,
                                   AddressTypeName = ad.AddressTypeNavigation.Name,
                                   IsPrimary = ad.IsPrimary,
                                   IsActive = ad.IsActive,

                               }).ToList() : null
                           }).FirstOrDefaultAsync();

                ObjResponse = CreateResponse(ObjResponse.Data, "Success", true);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<UserMasterViewModel>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblUserMaster>> Save(UserMasterPostModel model)
        {
            try
            {
                if (model.UserId > 0)
                {
                    var user = await _db.TblUserMasters.Where(x => (x.Mobile == model.Mobile || x.Email == model.Email) && x.UserId != model.UserId && !x.IsDeleted).FirstOrDefaultAsync();

                    if (user == null)
                    {
                        TblUserMaster objUser = _db.TblUserMasters.FirstOrDefault(r => r.UserId == model.UserId);
                        objUser.FirstName = model.FirstName ?? null;
                        objUser.LastName = model.LastName;
                        objUser.Email = model.Email;
                        objUser.Dob = model.Dob;
                        objUser.Mobile = model.Mobile;
                        // objUser.Password = _security.EncryptData(model.Password);
                        objUser.GenderId = model.GenderId;
                        objUser.ProfilePhoto = model.ProfilePhoto;
                        objUser.ModifiedBy = model.ModifiedBy;
                        var roletype = _db.TblUserMasters.Update(objUser);
                        _db.SaveChanges();
                        return CreateResponse(objUser, "User update Succefully...", true);
                    }
                    else
                    {
                        return CreateResponse<TblUserMaster>(null, "User already exist", true, ((int)ApiStatusCode.AlreadyExist));

                    }

                }
                else
                {

                    var user = await _db.TblUserMasters.Where(x => (x.Mobile == model.Mobile || x.Email == model.Email) && !x.IsDeleted).FirstOrDefaultAsync();

                    if (user == null)
                    {

                        TblUserMaster objUser = new TblUserMaster();
                        objUser.FirstName = model.FirstName ?? null;
                        objUser.LastName = model.LastName;
                        objUser.Email = model.Email;
                        objUser.Dob = model.Dob;
                        objUser.GenderId = model.GenderId;
                        objUser.Mobile = model.Mobile;
                        objUser.Password = _security.EncryptData(model.Password);
                        objUser.ProfilePhoto = model.ProfilePhoto;
                        objUser.RoleId = model.RoleId;
                        objUser.IsDeleted = false;
                        objUser.IsActive = true;
                        objUser.CreatedBy = model.CreatedBy;
                        var roletype = await _db.TblUserMasters.AddAsync(objUser);
                        _db.SaveChanges();
                        return CreateResponse(objUser, "User Added Succefully...", true);
                    }
                    else
                    {
                        return CreateResponse<TblUserMaster>(null, "User already exist", true, ((int)ApiStatusCode.AlreadyExist));
                    }
                }

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
