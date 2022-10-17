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
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.GeneralEntry
{
    public class GECategoryService : BaseService, IGECategoryService
    {
        DB_CMSContext _db;
        private readonly FileHelper _fileHelper;
        public GECategoryService(DB_CMSContext db, IHostingEnvironment environment)
        {
            _db = db;
            _fileHelper = new FileHelper(environment);
        }


        public async Task<ServiceResponse<IEnumerable<GeneralEntryCategoryViewModel>>> GetList(IndexModel model)
        {
            ServiceResponse<IEnumerable<GeneralEntryCategoryViewModel>> objResult = new ServiceResponse<IEnumerable<GeneralEntryCategoryViewModel>>();
            try
            {


                var result = (from data in _db.TblGecategoryMaters
                              where !data.IsDelete && (string.IsNullOrEmpty(model.Search) || data.Name.Contains(model.Search))
                              select data);
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
                                        select new GeneralEntryCategoryViewModel
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath() : null,
                                            SortedOrder = x.SortedOrder.Value,
                                            EnumValue = x.EnumValue,
                                            IsShowDataInMain = x.IsShowDataInMain,
                                            IsShowInMain = x.IsShowInMain,
                                            IsSingleEntry = x.IsSingleEntry,
                                            CreatedBy = x.CreatedBy,
                                            CreatedOn = x.CreatedOn,
                                            ModifiedBy = x.ModifiedBy,
                                            ModifiedOn = x.ModifiedOn,
                                            IsActive = x.IsActive.Value,
                                            IsDelete = x.IsDelete

                                        }).ToListAsync();

                if (result != null)
                {

                    return CreateResponse(objResult.Data as IEnumerable<GeneralEntryCategoryViewModel>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<GeneralEntryCategoryViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
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
        public async Task<ServiceResponse<GeneralEntryCategoryViewModel>> GetById(long id)
        {
            ServiceResponse<GeneralEntryCategoryViewModel> ObjResponse = new ServiceResponse<GeneralEntryCategoryViewModel>();
            try
            {
                var detail = await _db.TblGecategoryMaters.Select(x => new GeneralEntryCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImagePath = !string.IsNullOrEmpty(x.ImagePath) ? x.ImagePath.ToAbsolutePath() : null,
                    SortedOrder = x.SortedOrder.Value,
                    EnumValue = x.EnumValue,
                    IsShowDataInMain = x.IsShowDataInMain,
                    IsShowInMain = x.IsShowInMain,
                    IsSingleEntry = x.IsSingleEntry,
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    ModifiedBy = x.ModifiedBy,
                    ModifiedOn = x.ModifiedOn,
                    IsActive = x.IsActive.Value,
                    IsDelete = x.IsDelete

                }).FirstOrDefaultAsync(x => x.Id == id && x.IsActive.Value && x.IsDelete == false);

                if (detail != null)
                {
                    ObjResponse = CreateResponse(detail, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);

                }
                else
                {
                    ObjResponse = CreateResponse<GeneralEntryCategoryViewModel>(null, ResponseMessage.NotFound, true, (int)ApiStatusCode.RecordNotFound);

                }

            }
            catch (Exception ex)
            {
                ObjResponse = CreateResponse<GeneralEntryCategoryViewModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());
            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblGecategoryMater>> Save(GeneralEntryCategoryPostModel model)
        {
            try
            {

                if (model.Id > 0)
                {

                    TblGecategoryMater objData = _db.TblGecategoryMaters.FirstOrDefault(r => r.Id == model.Id);

                    objData.Name = model.Name;
                    objData.SortedOrder = model.SortedOrder;
                    //  objData.EnumValue = model.EnumValue;
                    objData.IsSingleEntry = model.IsSingleEntry;
                    objData.IsShowInMain = model.IsShowInMain;
                    objData.IsShowDataInMain = model.IsShowDataInMain;

                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {

                        objData.ImagePath = !string.IsNullOrEmpty(objData.ImagePath) && model.ImagePath.Contains(objData.ImagePath.Replace("\\", "/")) ? objData.ImagePath : _fileHelper.Save(model.ImagePath, FilePaths.Lookup);
                    }
                    else
                    {
                        _fileHelper.Delete(objData.ImagePath);
                        objData.ImagePath = null;

                    }


                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    var roletype = _db.TblGecategoryMaters.Update(objData);
                    _db.SaveChanges();
                    return CreateResponse(objData, ResponseMessage.Update, true, (int)ApiStatusCode.Ok);

                }
                else
                {

                    TblGecategoryMater objData = new TblGecategoryMater();

                    objData.Name = model.Name;
                    objData.SortedOrder = model.SortedOrder;
                    objData.EnumValue = model.EnumValue;
                    objData.IsSingleEntry = model.IsSingleEntry;
                    objData.IsShowInMain = model.IsShowInMain;
                    objData.IsShowDataInMain = model.IsShowDataInMain;
                    objData.ImagePath = string.IsNullOrEmpty(model.ImagePath) ? null : _fileHelper.Save(model.ImagePath, FilePaths.Lookup);

                    objData.IsActive = true;
                    objData.CreatedBy = _loginUserDetail.UserId.Value;
                    objData.ModifiedBy = _loginUserDetail.UserId.Value;
                    var roletype = await _db.TblGecategoryMaters.AddAsync(objData);
                    _db.SaveChanges();
                    return CreateResponse(objData, ResponseMessage.Save, true, (int)ApiStatusCode.Ok);

                }


            }
            catch (Exception ex)
            {

                return CreateResponse<TblGecategoryMater>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
        }


        public async Task<ServiceResponse<TblGecategoryMater>> Delete(long id)
        {
            try
            {
                TblGecategoryMater objData = new TblGecategoryMater();
                objData = _db.TblGecategoryMaters.FirstOrDefault(r => r.Id == id);
                objData.IsDelete = true;
                objData.ModifiedBy = _loginUserDetail.UserId.Value;
                objData.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse(objData, ResponseMessage.Delete, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                return CreateResponse<TblGecategoryMater>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError));
            }


        }

        public async Task<ServiceResponse<TblGecategoryMater>> ActiveStatusUpdate(long id)
        {
            try
            {
                TblGecategoryMater objData = new TblGecategoryMater();
                objData = _db.TblGecategoryMaters.FirstOrDefault(r => r.Id == id);

                objData.IsActive = !objData.IsActive;
                await _db.SaveChangesAsync();
                return CreateResponse(objData as TblGecategoryMater, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return null;

            }
        }

        public async Task<ServiceResponse<TblGecategoryMater>> FlagStatusUpdate(long id, string columnName)
        {
            try
            {
                TblGecategoryMater objData = new TblGecategoryMater();
                objData = _db.TblGecategoryMaters.FirstOrDefault(r => r.Id == id);

                switch (columnName)
                {
                    case "IsShowDataInMain":
                        objData.IsShowDataInMain = !objData.IsShowDataInMain;


                        break;
                    case "IsShowInMain":
                        objData.IsShowInMain = !objData.IsShowInMain;


                        break;
                    case "IsSingleEntry":
                        objData.IsSingleEntry = !objData.IsSingleEntry;


                        break;
                    default:
                        break;
                }
              
                await _db.SaveChangesAsync();
                return CreateResponse(objData as TblGecategoryMater, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return null;

            }
        }
    }
}
