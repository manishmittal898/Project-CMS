﻿using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;

namespace CMS.Service.Services.UserMaster
{
    public class UserMasterService : BaseService, IUserMasterService
    {
        private readonly DB_CMSContext _db;
        private readonly FileHelper _fileHelper;


        public UserMasterService(DB_CMSContext db, IConfiguration _configuration, IHostingEnvironment environment) : base(_configuration)
        {
            _db = db;
            _fileHelper = new FileHelper(environment);
        }

        public async Task<ServiceResponse<IEnumerable<UserMasterViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<UserMasterViewModel>> objResult = new ServiceResponse<IEnumerable<UserMasterViewModel>>();
            try
            {
                int roleId = 0;
                if (model.AdvanceSearchModel != null && model.AdvanceSearchModel.Count > 0 && model.AdvanceSearchModel.ContainsKey("roleId"))
                {
                    _ = model.AdvanceSearchModel.TryGetValue("roleId", out object role);
                    roleId = Convert.ToInt32(role.ToString());

                }

                IQueryable<TblUserMaster> result = from user in _db.TblUserMasters
                                                   where !user.IsDeleted && (string.IsNullOrEmpty(model.Search) || user.FirstName.Contains(model.Search) || user.LastName.Contains(model.Search) || user.Email.Contains(model.Search) || user.Mobile.Contains(model.Search)) && ((roleId == 0 && user.RoleId != (int)RoleEnum.Customer) || user.RoleId == roleId)
                                                   select user;
                result = model.OrderBy switch
                {
                    "Name" => model.OrderByAsc ? (from orderData in result orderby orderData.FirstName ascending select orderData) : (from orderData in result orderby orderData.FirstName descending select orderData),
                    "CreatedOn" => model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData),
                    _ => model.OrderByAsc ? (from orderData in result orderby orderData.ModifiedOn ascending select orderData) : (from orderData in result orderby orderData.ModifiedOn descending select orderData),
                };
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
                                            GenderId = x.GenderId.HasValue ? _security.EncryptData(x.GenderId.Value) : null,
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

                return result != null
                    ? CreateResponse(objResult.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok, TotalRecord: objResult.TotalRecord)
                    : CreateResponse<IEnumerable<UserMasterViewModel>>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound, TotalRecord: 0);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }

        public async Task<ServiceResponse<UserMasterViewModel>> GetById(long? id = null)
        {
            ServiceResponse<UserMasterViewModel> ObjResponse = new ServiceResponse<UserMasterViewModel>();
            try
            {
                if (_loginUserDetail != null && _loginUserDetail.RoleId.Value == (int)RoleEnum.Customer)
                {
                    id = _loginUserDetail.UserId ?? id;

                }
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
                               GenderId = x.GenderId.HasValue ? _security.EncryptData(x.GenderId.Value) : null,
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
                    TblUserMaster user = await _db.TblUserMasters.Where(x => (x.Mobile == model.Mobile || x.Email == model.Email) && x.UserId != model.UserId && !x.IsDeleted).FirstOrDefaultAsync();

                    if (user == null)
                    {
                        TblUserMaster objUser = _db.TblUserMasters.FirstOrDefault(r => r.UserId == model.UserId);
                        objUser.FirstName = model.FirstName ?? null;
                        objUser.LastName = model.LastName??null;
                        objUser.Email = model.Email;
                        objUser.Dob = model.Dob;
                        objUser.Mobile = model.Mobile;
                        // objUser.Password = _security.EncryptData(model.Password);
                        if (model.ProfilePhoto != null)
                        {
                            objUser.ProfilePhoto = await _fileHelper.Save(model.ProfilePhoto, FilePaths.UserProfile);
                        }
                        objUser.GenderId = !string.IsNullOrEmpty(model.GenderId) ? long.Parse(_security.DecryptData(model.GenderId)) : null as long?;

                        objUser.ModifiedBy = model.ModifiedBy;
                        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblUserMaster> roletype = _db.TblUserMasters.Update(objUser);
                        _ = _db.SaveChanges();
                        objUser.ProfilePhoto = !string.IsNullOrEmpty(objUser.ProfilePhoto) ? objUser.ProfilePhoto.ToAbsolutePath() : null;
                        return CreateResponse(objUser, ResponseMessage.Update.Replace("Record", "User"), true);
                    }
                    else
                    {
                        return CreateResponse<TblUserMaster>(null, ResponseMessage.UserExist, true, (int)ApiStatusCode.AlreadyExist);

                    }

                }
                else
                {

                    TblUserMaster user = await _db.TblUserMasters.Where(x => ((model.Mobile !=null && x.Mobile == model.Mobile )|| x.Email == model.Email) && !x.IsDeleted).FirstOrDefaultAsync();

                    if (user == null)
                    {

                        TblUserMaster objUser = new TblUserMaster
                        {
                            FirstName = model.FirstName ?? null,
                            LastName = model.LastName,
                            Email = model.Email,
                            Dob = model.Dob,
                            GenderId = !string.IsNullOrEmpty(model.GenderId) ? long.Parse(_security.DecryptData(model.GenderId)) : null as long?,
                            Mobile = model.Mobile,
                            Password = _security.EncryptData(model.Password),
                            ProfilePhoto = model.ProfilePhoto,
                            RoleId = model.RoleId,
                            IsDeleted = false,
                            IsActive = true,
                            CreatedBy = model.CreatedBy ?? 1
                        };
                        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblUserMaster> roletype = await _db.TblUserMasters.AddAsync(objUser);
                        _ = _db.SaveChanges();
                        return CreateResponse(objUser, ResponseMessage.Save.Replace("Record", "User"), true);
                    }
                    else
                    {
                        return CreateResponse<TblUserMaster>(null, ResponseMessage.UserExist, true, (int)ApiStatusCode.AlreadyExist);
                    }
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<TblUserMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblUserMaster>> Delete(int id)
        {
            try
            {
                TblUserMaster objRole = new TblUserMaster();
                objRole = _db.TblUserMasters.FirstOrDefault(r => r.UserId == id);

                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblUserMaster> roletype = _db.TblUserMasters.Remove(objRole);
                _ = await _db.SaveChangesAsync();
                return CreateResponse(objRole, "Deleted", true);
            }
            catch (Exception)
            {
                return null;

            }


        }

        public async Task<ServiceResponse<TblUserMaster>> UpdateProfilePic(UserProfilePostModel model)
        {
            try
            {
                TblUserMaster objUser = new TblUserMaster();
                objUser = _db.TblUserMasters.FirstOrDefault(r => r.UserId == _loginUserDetail.UserId);

                objUser.ProfilePhoto = await _fileHelper.Save(model.File, FilePaths.UserProfile);
                _ = await _db.SaveChangesAsync();
                objUser.ProfilePhoto = !string.IsNullOrEmpty(objUser.ProfilePhoto) ? objUser.ProfilePhoto.ToAbsolutePath() : null;

                return CreateResponse(objUser, ResponseMessage.Save, true);
            }
            catch (Exception)
            {
                return null;

            }


        }

        public async Task<ServiceResponse<TblUserMaster>> updateProfileDetail(UserDetailPostModel model)
        {
            TblUserMaster user = await _db.TblUserMasters.Where(x => (x.Mobile == model.Mobile || x.Email == model.Email) && x.UserId != _loginUserDetail.UserId && !x.IsDeleted).FirstOrDefaultAsync();

            if (user == null)
            {
                TblUserMaster objUser = _db.TblUserMasters.FirstOrDefault(r => r.UserId == _loginUserDetail.UserId);
                objUser.FirstName = model.FirstName ?? null;
                objUser.LastName = model.LastName;
                objUser.Email = model.Email;
                objUser.Dob = model.Dob;
                objUser.Mobile = model.Mobile;
                if (model.ProfilePhoto != null)
                {
                    objUser.ProfilePhoto = await _fileHelper.Save(model.ProfilePhoto, FilePaths.UserProfile);
                }
                objUser.GenderId = !string.IsNullOrEmpty(model.GenderId) ? long.Parse(_security.DecryptData(model.GenderId)) : null as long?;

                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblUserMaster> roletype = _db.TblUserMasters.Update(objUser);
                _ = _db.SaveChanges();
                objUser.ProfilePhoto = !string.IsNullOrEmpty(objUser.ProfilePhoto) ? objUser.ProfilePhoto.ToAbsolutePath() : null;
                return CreateResponse(objUser, ResponseMessage.Update.Replace("Record", "User"), true);
            }
            else
            {
                return CreateResponse<TblUserMaster>(null, ResponseMessage.UserExist, true, (int)ApiStatusCode.AlreadyExist);

            }

        }
    }
}
