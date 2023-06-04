using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Services.LookupMaster;
using CMS.Service.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;

namespace CMS.Service.Services.CustomerAddress
{
    public class CustomerAddressService : BaseService, ICustomerAddressService
    {
        DB_CMSContext _db;
        public CustomerAddressService(DB_CMSContext db)
        {
            _db = db;
        }
        public async Task<ServiceResponse<IEnumerable<CustomerAddressViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<CustomerAddressViewModel>> ObjResponse = new ServiceResponse<IEnumerable<CustomerAddressViewModel>>();
            try
            {


                long userId = _loginUserDetail.RoleId.Value == (int)RoleEnum.Customer ? _loginUserDetail.UserId.Value : 0;
                if (model.AdvanceSearchModel.Count > 0 && model.AdvanceSearchModel.ContainsKey("userId") && userId == 0)
                {
                    model.AdvanceSearchModel.TryGetValue("userId", out object CustomerId);
                    userId = Convert.ToInt64(CustomerId.ToString());

                }


                var result = (from addrs in _db.TblUserAddressMasters
                              where (userId == 0 || addrs.UserId == userId) && !addrs.IsDelete && (string.IsNullOrEmpty(model.Search) || addrs.Address.Contains(model.Search))
                              select addrs);
                switch (model.OrderBy)
                {
                    case "Address":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Address ascending select orderData) : (from orderData in result orderby orderData.Address descending select orderData);
                        break;
                    case "CreatedOn":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.PinCode ascending select orderData) : (from orderData in result orderby orderData.PinCode descending select orderData);
                        break;
                }
                ObjResponse.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);
                if (result != null)
                {

                    ObjResponse.Data = await (from x in result
                                              select new CustomerAddressViewModel()
                                              {

                                                  Address = x.Address,
                                                  AddressType = x.AddressType,
                                                  AddressTypeName = x.AddressTypeNavigation.Name,
                                                  BuildingNumber = x.BuildingNumber,
                                                  City = x.City,
                                                  FullName = x.FullName,
                                                  IsPrimary = x.IsPrimary,
                                                  Id = x.Id,
                                                  Landmark = x.Landmark,
                                                  Mobile = x.Mobile,
                                                  PinCode = x.PinCode,
                                                  State = x.State.Name,
                                                  StateId = x.StateId,
                                                  UserId = x.UserId,

                                              }).ToListAsync();

                    return CreateResponse(ObjResponse.Data, ResponseMessage.NotFound, true, ((int)ApiStatusCode.Ok), TotalRecord: ObjResponse.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<CustomerAddressViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
                }
            }

            catch (Exception ex)
            {

                return CreateResponse<IEnumerable<CustomerAddressViewModel>>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }

        }

        public ServiceResponse<CustomerAddressViewModel> GetById(long id)
        {
            ServiceResponse<CustomerAddressViewModel> ObjResponse = new ServiceResponse<CustomerAddressViewModel>();
            try
            {

                var result = _db.TblUserAddressMasters.FirstOrDefault(x => x.Id == id && x.IsActive.Value && x.IsDelete == false);
                if (result != null)
                {
                    ObjResponse.Data = new CustomerAddressViewModel()
                    {

                        Address = result.Address,
                        AddressType = result.AddressType,
                        AddressTypeName = result.AddressTypeNavigation.Name,
                        BuildingNumber = result.BuildingNumber,
                        City = result.City,
                        FullName = result.FullName,
                        IsPrimary = result.IsPrimary,
                        Id = result.Id,
                        Landmark = result.Landmark,
                        Mobile = result.Mobile,
                        PinCode = result.PinCode,
                        State = result.State.Name,
                        StateId = result.StateId,
                        UserId = result.UserId,

                    };
                    return CreateResponse(ObjResponse.Data, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
                }
                else
                {
                    return CreateResponse<CustomerAddressViewModel>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound));
                }

            }
            catch (Exception ex)
            {

                return CreateResponse<CustomerAddressViewModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblUserAddressMaster>> Save(CustomerAddressPostModel model)
        {
            try
            {

                if (model.Id > 0)
                {

                    TblUserAddressMaster objData = _db.TblUserAddressMasters.FirstOrDefault(r => r.Id == model.Id);



                    objData.Address = model.Address;
                    objData.AddressType = model.AddressType;
                    objData.BuildingNumber = model.BuildingNumber;
                    objData.City = model.City;
                    objData.FullName = model.FullName;
                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    objData.ModifiedOn = DateTime.Now;
                    objData.Landmark = model.Landmark;
                    objData.Mobile = model.Mobile;
                    objData.PinCode = model.PinCode;
                    objData.StateId = model.StateId;

                    var data = _db.TblUserAddressMasters.Update(objData);
                    await _db.SaveChangesAsync();
                    return CreateResponse(objData, ResponseMessage.Update, true, (int)ApiStatusCode.Ok);

                }
                else
                {

                    TblUserAddressMaster objData = new TblUserAddressMaster()
                    {

                        Address = model.Address,
                        AddressType = model.AddressType,
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
                        StateId = model.StateId,
                        UserId = _loginUserDetail.RoleId.Value == (int)RoleEnum.Customer ? _loginUserDetail.UserId.Value : model.UserId

                    };
                    await _db.SaveChangesAsync();
                    return CreateResponse(objData, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

                }


            }
            catch (Exception ex)
            {

                return CreateResponse<TblUserAddressMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblUserAddressMaster>> PrimaryStatusUpdate(long id)
        {
            try
            {
                TblUserAddressMaster objAddress = new TblUserAddressMaster();
                objAddress = _db.TblUserAddressMasters.FirstOrDefault(r => r.Id == id);
                objAddress.IsPrimary = !objAddress.IsPrimary;


                _db.TblUserAddressMasters.Update(objAddress);
                await _db.SaveChangesAsync();
                if (objAddress.IsPrimary)
                {
                    var address = _db.TblUserAddressMasters.Where(r => r.IsPrimary && r.Id != id).ToList();
                    foreach (var item in address)
                    {
                        item.IsPrimary = false;
                    }
                    _db.UpdateRange(address);
                }

                await _db.SaveChangesAsync();
                return CreateResponse(objAddress, ResponseMessage.Update, true);
            }
            catch (Exception ex)
            {

                return CreateResponse<TblUserAddressMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblUserAddressMaster>> ActiveStatusUpdate(long id)
        {
            try
            {
                TblUserAddressMaster objAddress = new TblUserAddressMaster();
                objAddress = _db.TblUserAddressMasters.FirstOrDefault(r => r.Id == id);
                objAddress.IsActive = !objAddress.IsActive;
                var roletype = _db.TblUserAddressMasters.Update(objAddress);
                await _db.SaveChangesAsync();
                return CreateResponse(objAddress, ResponseMessage.Update, true);
            }
            catch (Exception ex)
            {

                return CreateResponse<TblUserAddressMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblUserAddressMaster>> Delete(long id)
        {
            try
            {
                TblUserAddressMaster objAddress = new TblUserAddressMaster();
                objAddress = _db.TblUserAddressMasters.FirstOrDefault(r => r.Id == id);
                objAddress.IsDelete = !objAddress.IsDelete;
                var roletype = _db.TblUserAddressMasters.Update(objAddress);
                await _db.SaveChangesAsync();
                return CreateResponse(objAddress, ResponseMessage.Delete, true);
            }
            catch (Exception ex)
            {
                return CreateResponse<TblUserAddressMaster>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }
    }
}
