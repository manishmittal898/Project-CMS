using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.EntityFrameworkCore;
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
        public CommonService(DB_CMSContext db)
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

                            objData.Add(item, await GetLookupMasters(LookupTypeEnum.Product_Category.GetStringValue(),isTransactionData));
                            break;
                        case DropDownKey.ddlCaptionTag:

                            objData.Add(item, await GetLookupMasters(LookupTypeEnum.Caption_Tag.GetStringValue()));
                            break;
                        case DropDownKey.ddlProductSize:

                            objData.Add(item, await GetLookupMasters(LookupTypeEnum.Product_Size.GetStringValue()));
                            break;

                        case DropDownKey.ddlSubCategory:

                            objData.Add(item, await GetSubLookupMasters(null, LookupTypeEnum.Product_Category.GetStringValue()));
                            break;

                        case DropDownKey.ddlSubLookupGroup:

                            objData.Add(item, GetGroupSubLookupMasters(null, LookupTypeEnum.Product_Category.GetStringValue()));
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
                return await (from type in _db.TblLookupMasters where (string.IsNullOrEmpty(lktype) || type.LookUpTypeNavigation.EnumValue.ToLower() == lktype.ToLower()) && type.IsActive.Value == true && !type.IsDelete && (!isTransactionData || (isTransactionData && type.TblProductMasterCategories.Any(x => x.Category.Id == type.Id))) select type).OrderBy(x => x.SortedOrder)
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
        private object GetGroupSubLookupMasters(long[] lookupId = null, string lktype = null)
        {
            try
            {

                return _db.TblSubLookupMasters.Include(I => I.LookUp).Where(type => (string.IsNullOrEmpty(lktype) || (!string.IsNullOrEmpty(type.LookUp.LookUpTypeNavigation.EnumValue) && type.LookUp.LookUpTypeNavigation.EnumValue.ToLower() == lktype.ToLower())) && (lookupId == null || lookupId.Contains(type.LookUpId)) && type.IsActive.Value == true && !type.IsDeleted)
                    .ToList().GroupBy(x => x.LookUpId)
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


    }
}
