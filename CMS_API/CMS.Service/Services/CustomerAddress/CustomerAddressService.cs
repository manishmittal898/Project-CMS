﻿using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;

namespace CMS.Service.Services.CustomerAddress
{
    public class CustomerAddressService : BaseService, ICustomerAddressService
    {
        private readonly DB_CMSContext _db;

        public CustomerAddressService(DB_CMSContext db, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;

        }
        public async Task<ServiceResponse<IEnumerable<CustomerAddressViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<CustomerAddressViewModel>> ObjResponse = new ServiceResponse<IEnumerable<CustomerAddressViewModel>>();
            try
            {
                long userId = _loginUserDetail.RoleId.Value == (int)RoleEnum.Customer ? _loginUserDetail.UserId.Value : 0;
                if (model.AdvanceSearchModel != null && model.AdvanceSearchModel.Count > 0 && model.AdvanceSearchModel.ContainsKey("userId") && userId == 0)
                {
                    _ = model.AdvanceSearchModel.TryGetValue("userId", out object CustomerId);
                    userId = Convert.ToInt64(CustomerId.ToString());

                }
                IQueryable<TblUserAddressMaster> result = from addrs in _db.TblUserAddressMasters
                                                          where (userId == 0 || addrs.UserId == userId) && !addrs.IsDelete && (string.IsNullOrEmpty(model.Search) || addrs.Address.Contains(model.Search))
                                                          select addrs;
                result = model.OrderBy switch
                {
                    "Address" => model.OrderByAsc ? (from orderData in result orderby orderData.Address ascending select orderData) : (from orderData in result orderby orderData.Address descending select orderData),
                    "CreatedOn" => model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData),
                    _ => model.OrderByAsc ? (from orderData in result orderby orderData.PinCode ascending select orderData) : (from orderData in result orderby orderData.PinCode descending select orderData),
                };
                ObjResponse.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);
                if (result != null)
                {

                    ObjResponse.Data = await (from x in result
                                              select new CustomerAddressViewModel()
                                              {
                                                  Address = x.Address,
                                                  AddressType = x.AddressType.HasValue ? _security.EncryptData(x.AddressType.Value) : null,
                                                  AddressTypeName = x.AddressTypeNavigation.Name,
                                                  BuildingNumber = x.BuildingNumber,
                                                  City = x.City,
                                                  FullName = x.FullName,
                                                  IsPrimary = x.IsPrimary,
                                                  Id = _security.EncryptData(x.Id),
                                                  Landmark = x.Landmark,
                                                  Mobile = x.Mobile,
                                                  PinCode = x.PinCode,
                                                  State = x.State.Name,
                                                  StateId = x.StateId.HasValue ? _security.EncryptData(x.StateId.Value) : null,
                                                  UserId = x.UserId,
                                              }).ToListAsync();

                    return CreateResponse(ObjResponse.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok, TotalRecord: ObjResponse.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<CustomerAddressViewModel>>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound, TotalRecord: 0);
                }
            }

            catch (Exception ex)
            {
                return CreateResponse<IEnumerable<CustomerAddressViewModel>>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        public ServiceResponse<CustomerAddressViewModel> GetById(string id)
        {
            ServiceResponse<CustomerAddressViewModel> ObjResponse = new ServiceResponse<CustomerAddressViewModel>();
            try
            {

                TblUserAddressMaster result = _db.TblUserAddressMasters.FirstOrDefault(x => x.Id == _security.DecryptData(id).ToLongValue() && x.IsActive.Value && x.IsDelete == false);
                if (result != null)
                {
                    ObjResponse.Data = new CustomerAddressViewModel()
                    {
                        Address = result.Address,
                        AddressType = result.AddressType.HasValue ? _security.EncryptData(result.AddressType.Value) : null,
                        AddressTypeName = result.AddressTypeNavigation.Name,
                        BuildingNumber = result.BuildingNumber,
                        City = result.City,
                        FullName = result.FullName,
                        IsPrimary = result.IsPrimary,
                        Id = _security.EncryptData(result.Id),
                        Landmark = result.Landmark,
                        Mobile = result.Mobile,
                        PinCode = result.PinCode,
                        State = result.State.Name,
                        StateId = result.StateId.HasValue ? _security.EncryptData(result.StateId.Value) : null,
                        UserId = result.UserId,
                    };
                    return CreateResponse(ObjResponse.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
                }
                else
                {
                    return CreateResponse<CustomerAddressViewModel>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound);
                }

            }
            catch (Exception ex)
            {
                return CreateResponse<CustomerAddressViewModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        public async Task<ServiceResponse<CustomerAddressPostModel>> Save(CustomerAddressPostModel model)
        {
            try
            {
                TblUserAddressMaster existingData = _db.TblUserAddressMasters.FirstOrDefault(r => !r.IsDelete && r.IsActive.Value && r.IsPrimary && r.UserId == (_loginUserDetail.RoleId.Value == (int)RoleEnum.Customer ? _loginUserDetail.UserId.Value : model.UserId));

                if (existingData == null)
                {
                    model.IsPrimary = true;
                }
                TblUserAddressMaster objData;
                bool isAdd = true;
                if (!string.IsNullOrEmpty(model.Id))
                {
                    objData = _db.TblUserAddressMasters.FirstOrDefault(r => r.Id == _security.DecryptData(model.Id).ToLongValue());
                    objData.Address = model.Address;
                    objData.AddressType = !string.IsNullOrEmpty(model.AddressType) ? long.Parse(_security.DecryptData(model.AddressType)) : null as Nullable<long>;
                    objData.BuildingNumber = model.BuildingNumber;
                    objData.City = model.City;
                    objData.FullName = model.FullName;
                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    objData.ModifiedOn = DateTime.Now;
                    objData.Landmark = model.Landmark;
                    objData.Mobile = model.Mobile;
                    objData.PinCode = model.PinCode;
                    objData.StateId = !string.IsNullOrEmpty(model.StateId) ? long.Parse(_security.DecryptData(model.StateId)) : null as Nullable<long>; ;
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblUserAddressMaster> data = _db.TblUserAddressMasters.Update(objData);
                    _ = await _db.SaveChangesAsync();
                    isAdd = false;
                }
                else
                {
                    objData = new TblUserAddressMaster()
                    {
                        Address = model.Address,
                        AddressType = !string.IsNullOrEmpty(model.AddressType) ? long.Parse(_security.DecryptData(model.AddressType)) : null as Nullable<long>,
                        BuildingNumber = model.BuildingNumber,
                        City = model.City,
                        FullName = model.FullName,
                        IsPrimary = model.IsPrimary,
                        IsActive = true,
                        IsDelete = false,
                        CreatedBy = _loginUserDetail.UserId.Value,
                        ModifiedBy = _loginUserDetail.UserId.Value,
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                        Landmark = model.Landmark,
                        Mobile = model.Mobile,
                        PinCode = model.PinCode,
                        StateId = !string.IsNullOrEmpty(model.StateId) ? long.Parse(_security.DecryptData(model.StateId)) : null as Nullable<long>,
                        UserId = _loginUserDetail.RoleId.Value == (int)RoleEnum.Customer ? _loginUserDetail.UserId.Value : model.UserId

                    };
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblUserAddressMaster> resultData = await _db.TblUserAddressMasters.AddAsync(objData);
                    _ = await _db.SaveChangesAsync();
                    objData = resultData.Entity;
                    model.Id = _security.EncryptData(objData.Id);
                }

                //if set primary then remove primary for old record
                if (model.IsPrimary)
                {
                    List<TblUserAddressMaster> address = _db.TblUserAddressMasters.Where(r => r.IsPrimary && r.Id != objData.Id).ToList();
                    foreach (TblUserAddressMaster item in address)
                    {
                        item.IsPrimary = false;
                    }
                    _db.UpdateRange(address);
                    _ = await _db.SaveChangesAsync();
                }
                return CreateResponse(model, isAdd ? ResponseMessage.Save : ResponseMessage.Update, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {
                return CreateResponse<CustomerAddressPostModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        public async Task<ServiceResponse<TblUserAddressMaster>> PrimaryStatusUpdate(string id)
        {
            try
            {
                TblUserAddressMaster objAddress = new TblUserAddressMaster();
                objAddress = _db.TblUserAddressMasters.FirstOrDefault(r => r.Id == _security.DecryptData(id).ToLongValue());
                objAddress.IsPrimary = !objAddress.IsPrimary;


                _ = _db.TblUserAddressMasters.Update(objAddress);
                _ = await _db.SaveChangesAsync();
                if (objAddress.IsPrimary)
                {
                    List<TblUserAddressMaster> address = _db.TblUserAddressMasters.Where(r => r.IsPrimary && r.Id != _security.DecryptData(id).ToLongValue()).ToList();
                    foreach (TblUserAddressMaster item in address)
                    {
                        item.IsPrimary = false;
                    }
                    _db.UpdateRange(address);
                }

                _ = await _db.SaveChangesAsync();
                return CreateResponse(objAddress, ResponseMessage.Update, true);
            }
            catch (Exception ex)
            {
                return CreateResponse<TblUserAddressMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        public async Task<ServiceResponse<TblUserAddressMaster>> ActiveStatusUpdate(string id)
        {
            try
            {
                TblUserAddressMaster objAddress = new TblUserAddressMaster();
                objAddress = _db.TblUserAddressMasters.FirstOrDefault(r => r.Id == _security.DecryptData(id).ToLongValue());
                objAddress.IsActive = !objAddress.IsActive;
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblUserAddressMaster> roletype = _db.TblUserAddressMasters.Update(objAddress);
                _ = await _db.SaveChangesAsync();
                return CreateResponse(objAddress, ResponseMessage.Update, true);
            }
            catch (Exception ex)
            {
                return CreateResponse<TblUserAddressMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        public async Task<ServiceResponse<TblUserAddressMaster>> Delete(string id)
        {
            try
            {
                TblUserAddressMaster objAddress = new TblUserAddressMaster();
                objAddress = _db.TblUserAddressMasters.FirstOrDefault(r => r.Id == _security.DecryptData(id).ToLongValue());
                if (objAddress != null && !objAddress.IsPrimary)
                {
                    objAddress.IsDelete = !objAddress.IsDelete;
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblUserAddressMaster> roletype = _db.TblUserAddressMasters.Update(objAddress);
                    _ = await _db.SaveChangesAsync();
                    return CreateResponse(objAddress, ResponseMessage.Delete, true);
                }
                else
                {
                    return objAddress != null && objAddress.IsPrimary
                        ? CreateResponse(objAddress, ResponseMessage.DeleteDenied, false)
                        : CreateResponse<TblUserAddressMaster>(null, ResponseMessage.NotFound, false);
                }
            }
            catch (Exception ex)
            {
                return CreateResponse<TblUserAddressMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
    }
}
