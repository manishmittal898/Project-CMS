using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;
using static CMS.Service.Services.Common.CommonModel;

namespace CMS.Service.Services.Common
{
    public class CommonService : BaseService, ICommonService
    {
        private readonly DB_CMSContext _db;
        public CommonService(DB_CMSContext db , IConfiguration _configuration) : base(_configuration)
        {

            _db = db;
        }

        public async Task<ServiceResponse<Dictionary<string, object>>> GetDropDown(string[] key, bool isTransactionData = false)
        {
            Dictionary<string, object> objData = new Dictionary<string, object>();
            try
            {
                foreach (var item in key)
                {

                    switch (item.ToLower())
                    {


                        case DropDownKey.ddlUserRole:

                            objData.Add(item, await GetUserRole());
                            break;


                        case DropDownKey.ddlLookupTypeMasters:

                            objData.Add(item, await GetLookupTypeMasters());
                            break;
                        case DropDownKey.ddlCategory:

                            objData.Add(item, await GetLookupMasters(LookupTypeEnum.Product_Category.GetStringValue(), isTransactionData));
                            break;
                        case DropDownKey.ddlCaptionTag:

                            objData.Add(item, await GetLookupMasters(LookupTypeEnum.Caption_Tag.GetStringValue()));
                            break;
                        case DropDownKey.ddlProductSize:

                            objData.Add(item, await GetLookupMasters(LookupTypeEnum.Product_Size.GetStringValue()));
                            break;

                        case DropDownKey.ddlGender:

                            objData.Add(item, await GetLookupMasters(LookupTypeEnum.GENDER.GetStringValue()));
                            break;

                        case DropDownKey.ddlProductViewSection:

                            objData.Add(item, await GetLookupMasters(LookupTypeEnum.Product_View_Section.GetStringValue(), isTransactionData));
                            break;
                        case DropDownKey.ddlAddressType:

                            objData.Add(item, await GetLookupMasters(LookupTypeEnum.Address_Type.GetStringValue()));
                            break;

                        case DropDownKey.ddlProductPrice:

                            objData.Add(item, await GetProductMaxPrice());
                            break;


                        case DropDownKey.ddlSubCategory:

                            objData.Add(item, await GetSubLookupMasters(null, LookupTypeEnum.Product_Category.GetStringValue()));
                            break;

                        case DropDownKey.ddlSubLookupGroup:

                            objData.Add(item, GetGroupSubLookupMasters(null, LookupTypeEnum.Product_Category.GetStringValue(), isTransactionData));
                            break;


                        case DropDownKey.ddlLookupGroup:

                            objData.Add(item, GetGroupLookupMasters(null, LookupTypeEnum.Product_Category.GetStringValue(), isTransactionData));
                            break;

                        case DropDownKey.ddlCMSPage:

                            objData.Add(item, await GetLookupMasters(LookupTypeEnum.CMS_Page.GetStringValue(), true));
                            break;


                        case DropDownKey.ddlState:

                            objData.Add(item, await GetLookupMasters(LookupTypeEnum.State.GetStringValue(), isTransactionData));
                            break;


                        case DropDownKey.ddlContentType:

                            objData.Add(item, GetContentTypeEnum());
                            break;

                        case DropDownKey.ddlGeneralEntryCategory:
                            objData.Add(item, await GetGeneralEntryCategory(isTransactionData));


                            break;






                        default:
                            break;
                    }
                }

                return CreateResponse(objData, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return CreateResponse<Dictionary<string, object>>(null, ResponseMessage.Success, true, ((int)ApiStatusCode.ServerException), ex.Message.ToString());

            }

        }

        public async Task<ServiceResponse<Dictionary<string, object>>> GetFilterDropDown(FilterDropDownPostModel[] model)
        {
            Dictionary<string, object> objData = new Dictionary<string, object>();
            try
            {
                foreach (var item in model)
                {

                    switch (item.Key.ToLower())
                    {

                        case DropDownKey.ddlSubLookup:
                            if (item.FileterFromKey.ToLower() == DropDownKey.ddlLookup.ToLower().ToString())
                            {
                                objData.Add(item.Key, await GetSubLookupMasters(item.Values));
                            }
                            break;

                        case DropDownKey.ddlSubLookupGroup:
                            if (item.FileterFromKey.ToLower() == DropDownKey.ddlLookup.ToLower().ToString())
                            {
                                objData.Add(item.Key, GetGroupSubLookupMasters(item.Values));
                            }
                            break;


                    }

                }

                return CreateResponse(objData, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return CreateResponse<Dictionary<string, object>>(null, ResponseMessage.Success, true, ((int)ApiStatusCode.ServerException), ex.Message.ToString());

            }

        }

        private async Task<object> GetUserRole(bool? isParent = false)
        {
            try
            {
                return await (from role in _db.TblRoleTypes where role.IsActive == true && !role.IsDeleted select role)
                     .Select(r => new { Text = r.RoleName, Value = r.RoleId })
                     .ToListAsync();
            }
            catch
            {

                return null;
            }
        }

        private async Task<object> GetLookupMasters(string lktype = null, bool isTransactionData = false)
        {
            try
            {
                var data = (from type in _db.TblLookupMasters where (string.IsNullOrEmpty(lktype) || type.LookUpTypeNavigation.EnumValue.ToLower() == lktype.ToLower()) && type.IsActive.Value == true && !type.IsDelete select type);
                if (isTransactionData)
                {

                    if (lktype == LookupTypeEnum.Product_Category.GetStringValue())
                    {
                        data = data.Where(x => x.TblProductMasterCategories.Any(y => y.Category.Id == x.Id));
                    }
                    else if (lktype == LookupTypeEnum.CMS_Page.GetStringValue())
                    {
                        data = data.Where(x => x.TblCmspageContentMasters.Any(y => y.PageId == x.Id && y.IsDeleted == false && y.IsActive == true));

                    }else if(lktype == LookupTypeEnum.Product_View_Section.GetStringValue())
                    {
                        data = data.Where(x => x.TblProductMasterViewSections.Any(y => y.ViewSectionId == x.Id && y.IsDelete==false && y.IsActive.Value == true));

                    }

                }

                return await data.OrderBy(x => x.SortedOrder)
                    .Select(r => new { Text = r.Name, Value = r.Id })
                    .ToListAsync();

            }
            catch
            {

                return null;
            }
        }


        private async Task<object> GetSubLookupMasters(long[] lookupId = null, string lktype = null)
        {
            try
            {
                return await (from type in _db.TblSubLookupMasters where (string.IsNullOrEmpty(lktype) || type.LookUp.LookUpTypeNavigation.EnumValue.ToLower() == lktype.ToLower()) && (lookupId.Length == 0 || lookupId.Contains(type.LookUpId)) && type.IsActive.Value == true && !type.IsDeleted select type).OrderBy(x => x.SortedOrder).Select(r => new { Text = r.Name, Value = r.Id, CategoryId = r.LookUpId, Category = r.LookUp.Name })
                     .ToListAsync();

            }
            catch
            {

                return null;
            }
        }

        private object GetGroupLookupMasters(long[] lookupId = null, string lktype = null, bool isTransactionData = false)
        {
            try
            {
                string enumValue = LookupTypeEnum.Product_Category.GetStringValue().ToLower();

                var data = _db.TblLookupMasters.Include(I => I.TblSubLookupMasters).ThenInclude(x => x.TblProductMasters).Include(x => x.TblProductMasterCategories).Where(type => (string.IsNullOrEmpty(lktype) || (!string.IsNullOrEmpty(type.LookUpTypeNavigation.EnumValue) && type.IsActive == true && type.IsDelete == false && type.LookUpTypeNavigation.EnumValue.ToLower() == lktype.ToLower())) && (lookupId == null || lookupId.Contains(type.Id)) &&
                  (!isTransactionData || (type.LookUpTypeNavigation.EnumValue.ToLower() == enumValue ? (isTransactionData && type.TblProductMasterCategories.Count(x => x.CategoryId == type.Id && x.IsDelete == false && x.IsActive.Value == true) > 0) : true))

                ).ToList();

                return data.GroupBy(x => x.Id)
                      .Select(y => new
                      {
                          CategoryId = y.Key,
                          Category = y.FirstOrDefault().Name,
                          Data = y.FirstOrDefault().TblSubLookupMasters.Where(x => x.IsActive == true && !x.IsDeleted && (!isTransactionData || (x.TblProductMasters.Any(yz => !yz.IsDelete && yz.IsActive == true
                          && x.Id == yz.SubCategoryId))))
                          .Select(x => new { Text = x.Name, Value = x.Id }).ToList()
                      }).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }





        }


        private object GetGroupSubLookupMasters(long[] lookupId = null, string lktype = null, bool isTransactionData = false)
        {
            try
            {
                string enumValue = LookupTypeEnum.Product_Category.GetStringValue().ToLower();

                var data = _db.TblSubLookupMasters.Include(I => I.LookUp).Include(x => x.TblProductMasters).Where(type => (string.IsNullOrEmpty(lktype) || (!string.IsNullOrEmpty(type.LookUp.LookUpTypeNavigation.EnumValue) && type.LookUp.IsActive == true && type.LookUp.IsDelete == false && type.LookUp.LookUpTypeNavigation.EnumValue.ToLower() == lktype.ToLower())) && (lookupId == null || lookupId.Contains(type.LookUpId)) && type.IsActive.Value == true && !type.IsDeleted &&
                 (!isTransactionData || (type.LookUp.LookUpTypeNavigation.EnumValue.ToLower() == enumValue ? (isTransactionData && type.TblProductMasters.Count(x => x.SubCategoryId == type.Id && x.CategoryId == type.LookUpId && x.IsDelete == false && x.IsActive.Value == true) > 0) : true))

                ).ToList();





                return data.GroupBy(x => x.LookUpId)
                      .Select(y => new
                      {
                          CategoryId = y.Key,
                          Category = y.FirstOrDefault().LookUp.Name,
                          Data = y.Select(x => new { Text = x.Name, Value = x.Id }).ToList()
                      }).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }





        }
        private async Task<object> GetLookupTypeMasters()
        {
            try
            {
                return await (from type in _db.TblLookupTypeMasters where type.IsActive == true && !type.IsDelete select type).OrderBy(x => x.Name)
                     .Select(r => new { Text = r.Name, Value = r.Id })
                     .ToListAsync();
            }
            catch
            {

                return null;
            }
        }

        private async Task<object> GetProductMaxPrice()
        {
            try
            {
                return await (from type in _db.TblProductMasters where type.IsActive == true && !type.IsDelete select type).OrderByDescending(x => x.Price)
                     .Select(r => new { Text = r.Price, Value = r.Price }).FirstAsync();

            }
            catch
            {

                return null;
            }
        }

        private object GetContentTypeEnum()
        {
            return Enum.GetValues(typeof(ContentTypeEnum)).Cast<ContentTypeEnum>().Select(r => new { Value = r, Text = r.GetStringValue() }).ToList();
        }

        private async Task<object> GetGeneralEntryCategory(bool isTransactionData = false)
        {
            try
            {
                return await (from type in _db.TblGecategoryMaters
                              where type.IsActive == true && !type.IsDelete
                              && (isTransactionData ? type.TblGeneralEntries.Any(g => g.CategoryId == type.Id) : true)
                              select type).OrderBy(x => x.Name)
                     .Select(r => new { Text = r.Name, Value = r.Id, ContentType = r.ContentType, IsShowThumbnail = r.IsShowThumbnail, IsShowUrl = r.IsShowUrl })
                     .ToListAsync();
            }
            catch
            {

                return null;
            }
        }


    }
}
