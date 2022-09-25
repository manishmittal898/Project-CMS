using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;

namespace CMS.Service.Services.CMSPage
{
    public class CMSPageService : BaseService, ICMSPageService
    {
        DB_CMSContext _db;
        private readonly FileHelper _fileHelper;
        public CMSPageService(DB_CMSContext db, IHostingEnvironment environment)
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


                var result = (from lkType in _db.TblLookupMasters
                              where lkType.LookUpTypeNavigation.EnumValue.Equals(enumValue) && !lkType.IsDelete && (string.IsNullOrEmpty(model.Search) || lkType.Name.Contains(model.Search))
                              select lkType);



                switch (model.OrderBy)
                {
                    case "Name":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.Name ascending select orderData) : (from orderData in result orderby orderData.Name descending select orderData);
                        break;
                    case "CreatedOn":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.CreatedOn descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.SortedOrder ascending select orderData) : (from orderData in result orderby orderData.SortedOrder descending select orderData);
                        break;
                }
                objResult.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                objResult.Data = await (from x in result
                                        select new CMSPageListViewModel
                                        {
                                            PageId = x.Id,
                                            Name = x.Name,
                                            SortedOrder = x.SortedOrder,
                                            IsActive=x.IsActive,
                                            IsDelete=x.IsDelete

                                        }).ToListAsync();

                if (result != null)
                {

                    return CreateResponse(objResult.Data as IEnumerable<CMSPageListViewModel>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<CMSPageListViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
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

        public async Task<ServiceResponse<List<CMSPageViewModel>>> GetById(long id)
        {
            ServiceResponse<List<CMSPageViewModel>> ObjResponse = new ServiceResponse<List<CMSPageViewModel>>();
            try
            {
                var detail = await _db.TblCmspageContentMasters.Where(x => x.PageId == id && x.IsActive.Value && x.IsDeleted == false).Select(X => new CMSPageViewModel
                {
                    Id = X.Id,
                    PageId = X.PageId,
                    Page = X.Page.Name,
                    SortedOrder = X.SortedOrder,
                    Heading = X.Heading,
                    Content = X.Content,
                    IsActive = X.IsActive,
                    IsDeleted = X.IsDeleted,

                }).ToListAsync();

                if (detail != null)
                {
                    ObjResponse = CreateResponse(detail, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);

                }
                else
                {
                    ObjResponse = CreateResponse<List<CMSPageViewModel>>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound);

                }

            }
            catch (Exception ex)
            {
                ObjResponse = CreateResponse<List<CMSPageViewModel>>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
            return ObjResponse;
        }


        public Task<ServiceResponse<TblCmspageContentMaster>> Save(CMSPagePostModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TblCmspageContentMaster>> ActiveStatusUpdate(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TblCmspageContentMaster>> Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}
