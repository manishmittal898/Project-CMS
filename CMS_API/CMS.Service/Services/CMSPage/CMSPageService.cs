﻿using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;

namespace CMS.Service.Services.CMSPage
{
    public class CMSPageService : BaseService, ICMSPageService
    {
        private readonly DB_CMSContext _db;
        private readonly FileHelper _fileHelper;
        public CMSPageService(DB_CMSContext db, IHostingEnvironment environment, IConfiguration _configuration) : base(_configuration)
        {
            _db = db;
            _fileHelper = new FileHelper(environment);
        }
        public async Task<ServiceResponse<IEnumerable<CMSPageListViewModel>>> GetList(IndexModel model)
        {

            ServiceResponse<IEnumerable<CMSPageListViewModel>> objResult = new ServiceResponse<IEnumerable<CMSPageListViewModel>>();
            try
            {
                string enumValue = LookupTypeEnum.CMS_Page.GetStringValue();


                IQueryable<TblLookupMaster> result = from lkType in _db.TblLookupMasters
                                                     where lkType.LookUpTypeNavigation.EnumValue.Equals(enumValue) && !lkType.IsDelete && (string.IsNullOrEmpty(model.Search) || lkType.Name.Contains(model.Search))
                                                     select lkType;



                result = model.OrderBy switch
                {
                    "Name" => model.OrderByAsc ? (from orderData in result orderby orderData.Name ascending select orderData) : (from orderData in result orderby orderData.Name descending select orderData),
                    "CreatedOn" => model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData),
                    _ => model.OrderByAsc ? (from orderData in result orderby orderData.SortedOrder ascending select orderData) : (from orderData in result orderby orderData.SortedOrder descending select orderData),
                };
                objResult.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new CMSPageListViewModel
                                        {
                                            PageId = _security.EncryptData(x.Id),
                                            Name = x.Name,
                                            SortedOrder = x.SortedOrder,
                                            IsActive = x.IsActive,
                                            IsDelete = x.IsDelete

                                        }).ToListAsync();

                return result != null
                    ? CreateResponse(objResult.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok, TotalRecord: objResult.TotalRecord)
                    : CreateResponse<IEnumerable<CMSPageListViewModel>>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound, TotalRecord: 0);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;


        }

        public async Task<ServiceResponse<List<CMSPageViewModel>>> GetById(string id)
        {
            ServiceResponse<List<CMSPageViewModel>> ObjResponse = new ServiceResponse<List<CMSPageViewModel>>();
            try
            {
                List<CMSPageViewModel> detail = await _db.TblCmspageContentMasters.Where(x => x.PageId == long.Parse(_security.DecryptData(id)) && x.IsActive.Value && x.IsDeleted == false).Select(X => new CMSPageViewModel
                {
                    Id = _security.EncryptData(X.Id),
                    PageId = _security.EncryptData(X.PageId),
                    Page = X.Page.Name,
                    SortedOrder = X.SortedOrder,
                    Heading = X.Heading,
                    Content = X.Content,
                    IsActive = X.IsActive,
                    IsDeleted = X.IsDeleted,

                }).ToListAsync();

                ObjResponse = detail != null && detail.Count > 0
                    ? CreateResponse(detail, ResponseMessage.Success, true, (int)ApiStatusCode.Ok)
                    : CreateResponse<List<CMSPageViewModel>>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound);

            }
            catch (Exception ex)
            {
                ObjResponse = CreateResponse<List<CMSPageViewModel>>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
            return ObjResponse;
        }


        public async Task<ServiceResponse<string>> Save(CMSPagePostModel model)
        {
            try
            {

                if (!string.IsNullOrEmpty(model.Id))
                {

                    TblCmspageContentMaster objData = _db.TblCmspageContentMasters.FirstOrDefault(r => r.Id == long.Parse(_security.DecryptData(model.Id.ToString())));
                    objData.Content = model.Content;
                    objData.SortedOrder = model.SortedOrder;
                    objData.Heading = model.Heading;
                    objData.ModifiedOn = DateTime.Now;
                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblCmspageContentMaster> dataResult = _db.TblCmspageContentMasters.Update(objData);
                    _ = _db.SaveChanges();

                }
                else
                {
                    TblCmspageContentMaster objData = new TblCmspageContentMaster
                    {
                        PageId = long.Parse(_security.DecryptData(model.PageId.ToString())),
                        Content = model.Content,
                        SortedOrder = model.SortedOrder,
                        Heading = model.Heading,
                        IsDeleted = false,
                        IsActive = true,
                        CreatedBy = _loginUserDetail.UserId.Value,
                        ModifiedBy = _loginUserDetail.UserId.Value
                    };
                    Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblCmspageContentMaster> dataResult = await _db.TblCmspageContentMasters.AddAsync(objData);
                    _ = _db.SaveChanges();

                    model.Id = _security.EncryptData(dataResult.Entity.Id.ToString());

                }

                return CreateResponse<string>(model.Id.ToString(), ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

            }
            catch (Exception ex)
            {

                return CreateResponse<string>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<string>> ActiveStatusUpdate(string id)
        {
            try
            {

                TblCmspageContentMaster objData = await _db.TblCmspageContentMasters.FirstOrDefaultAsync(r => r.Id == long.Parse(_security.DecryptData(id)));
                objData.IsActive = !objData.IsActive;
                objData.ModifiedOn = DateTime.Now;
                objData.ModifiedBy = _loginUserDetail.UserId.Value;
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblCmspageContentMaster> dataResult = _db.TblCmspageContentMasters.Update(objData);
                _ = _db.SaveChanges();
                return CreateResponse(id.ToString(), ResponseMessage.Update, true, (int)ApiStatusCode.Ok);

            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        public async Task<ServiceResponse<string>> Delete(string id)
        {
            try
            {

                TblCmspageContentMaster objData = await _db.TblCmspageContentMasters.FirstOrDefaultAsync(r => r.Id == long.Parse(_security.DecryptData(id)));
                objData.IsDeleted = !objData.IsDeleted;
                objData.ModifiedOn = DateTime.Now;
                objData.ModifiedBy = _loginUserDetail.UserId.Value;
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TblCmspageContentMaster> dataResult = _db.TblCmspageContentMasters.Update(objData);
                _ = _db.SaveChanges();
                return CreateResponse(id.ToString(), ResponseMessage.Update, true, (int)ApiStatusCode.Ok);

            }
            catch (Exception ex)
            {
                return CreateResponse<string>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
    }
}
