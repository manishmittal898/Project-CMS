using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.ExtensionMethod;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CMS.Core.FixedValue.Enums;

namespace CMS.Service.Services.GeneralEntry
{
    public class GeneralEntryService : BaseService, IGeneralEntryService
    {
        DB_CMSContext _db;
        private readonly FileHelper _fileHelper;
        public GeneralEntryService(DB_CMSContext db, IHostingEnvironment environment)
        {
            _db = db;
            _fileHelper = new FileHelper(environment);
        }

        public async Task<ServiceResponse<TblGeneralEntry>> ActiveStatusUpdate(long id)
        {
            try
            {
                TblGeneralEntry objProduct = new TblGeneralEntry();
                objProduct = _db.TblGeneralEntries.FirstOrDefault(r => r.Id == id);

                objProduct.IsActive = !objProduct.IsActive;
                objProduct.ModifiedBy = _loginUserDetail.UserId ?? objProduct.ModifiedBy;
                objProduct.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse(objProduct as TblGeneralEntry, ResponseMessage.Update, true, ((int)ApiStatusCode.Ok));
            }
            catch (Exception ex)
            {

                return null;

            }
        }

        public async Task<ServiceResponse<TblGeneralEntry>> Delete(long id)
        {
            try
            {
                TblGeneralEntry objProduct = new TblGeneralEntry();
                objProduct = _db.TblGeneralEntries.FirstOrDefault(r => r.Id == id);

                objProduct.IsDeleted = (bool)true;
                objProduct.ModifiedBy = _loginUserDetail.UserId ?? objProduct.ModifiedBy;
                objProduct.ModifiedOn = DateTime.Now;
                await _db.SaveChangesAsync();
                return CreateResponse(objProduct as TblGeneralEntry, ResponseMessage.Delete, true, ((int)ApiStatusCode.Ok));

            }
            catch (Exception ex)
            {
                return CreateResponse<TblGeneralEntry>(null, ResponseMessage.Fail, true, ((int)ApiStatusCode.InternalServerError), ex.Message.ToString());

            }
        }


        public async Task<ServiceResponse<GeneralEntryViewModel>> GetById(long id)
        {
            ServiceResponse<GeneralEntryViewModel> ObjResponse = new ServiceResponse<GeneralEntryViewModel>();
            try
            {
                var result = await (from gen in _db.TblGeneralEntries
                                    join data in _db.TblFileDataMasters.DefaultIfEmpty() on gen.DataId equals data.DataId
                                    into lstGroup
                                    select new GeneralEntryViewModel
                                    {
                                        Id = gen.Id,
                                        Title = gen.Title,
                                        CategoryId = gen.CategoryId,
                                        Category = gen.Category.Name,
                                        Description = gen.Description,
                                        DataId = gen.DataId,
                                        SortedOrder = gen.SortedOrder,
                                        IsActive = gen.IsActive,
                                        IsDeleted = gen.IsDeleted,
                                        Data = lstGroup.Select(x => new GeneralEntryDataViewModel
                                        {
                                            Id = x.Id,
                                            Value = !string.IsNullOrEmpty(x.Value) ? (gen.Category.ContentType != (int)ContentTypeEnum.URL ? x.Value.ToAbsolutePath() : x.Value) : null,
                                            GeneralEntryId = gen.Id
                                        }).ToList()
                                    }).FirstOrDefaultAsync();



                ObjResponse = CreateResponse(result, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<GeneralEntryViewModel>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<IEnumerable<GeneralEntryViewModel>>> GetList(IndexModel model)
        {

            ServiceResponse<IEnumerable<GeneralEntryViewModel>> objResult = new ServiceResponse<IEnumerable<GeneralEntryViewModel>>();
            try
            {

                var result1 = (from gen in _db.TblGeneralEntries.DefaultIfEmpty()
                               join data in _db.TblFileDataMasters.DefaultIfEmpty() on gen.DataId equals data.DataId
                               into lstGroup
                               where !gen.IsDeleted && (string.IsNullOrEmpty(model.Search) || gen.Title.Contains(model.Search))
                               select new { m = gen, dt = lstGroup });

                var result = from b in _db.Set<TblGeneralEntry>()
                             join p in _db.Set<TblFileDataMaster>()
                                 on b.DataId equals p.DataId into grouping
                             from p in grouping.DefaultIfEmpty()
                             select new { m = b, dt = grouping };


                switch (model.OrderBy)
                {
                    case "Name":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.m.Title ascending select orderData) : (from orderData in result orderby orderData.m.Title descending select orderData);
                        break;
                    case "CreatedOn":
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.m.CreatedOn ascending select orderData) : (from orderData in result orderby orderData.m.CreatedOn descending select orderData);
                        break;
                    default:
                        result = model.OrderByAsc ? (from orderData in result orderby orderData.m.SortedOrder ascending select orderData) : (from orderData in result orderby orderData.m.SortedOrder descending select orderData);
                        break;
                }
                objResult.TotalRecord = result.Count();

                result = result.Skip(((model.Page == 0 ? 1 : model.Page) - 1) * (model.PageSize != 0 ? model.PageSize : int.MaxValue)).Take(model.PageSize != 0 ? model.PageSize : int.MaxValue);

                if (result.Count() > 0)
                {

                    objResult.Data = (from x in result
                                      select new GeneralEntryViewModel
                                      {
                                          Id = x.m.Id,
                                          Title = x.m.Title,
                                          CategoryId = x.m.CategoryId,
                                          Category = x.m.Category.Name,
                                          Description = x.m.Description,
                                          DataId = x.m.DataId,
                                          SortedOrder = x.m.SortedOrder,
                                          IsActive = x.m.IsActive,
                                          IsDeleted = x.m.IsDeleted,
                                          Data = x.dt.Select(xt => new GeneralEntryDataViewModel
                                          {
                                              Id = xt.Id,
                                              Value = !string.IsNullOrEmpty(xt.Value) ? (x.m.Category.ContentType != (int)
                                              ContentTypeEnum.URL ? xt.Value.ToAbsolutePath() : xt.Value) : null,
                                              GeneralEntryId = x.m.Id
                                          }).ToList()
                                      }).ToList();
                    return CreateResponse(objResult.Data as IEnumerable<GeneralEntryViewModel>, ResponseMessage.Success, true, ((int)ApiStatusCode.Ok), TotalRecord: objResult.TotalRecord);
                }
                else
                {
                    return CreateResponse<IEnumerable<GeneralEntryViewModel>>(null, ResponseMessage.NotFound, true, ((int)ApiStatusCode.RecordNotFound), TotalRecord: 0);
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

        public Task<ServiceResponse<TblGeneralEntry>> Save(GeneralEntryPostModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<GeneralEntryDataViewModel>>> GetProductFile(string DataId, bool IsFile)
        {
            ServiceResponse<List<GeneralEntryDataViewModel>> objResponse = new ServiceResponse<List<GeneralEntryDataViewModel>>();
            try
            {

                objResponse.Data = await _db.TblFileDataMasters.Where(x => x.DataId == DataId).Select(x => new GeneralEntryDataViewModel
                {
                    Id = x.Id,
                    Value = !string.IsNullOrEmpty(x.Value) ? IsFile ? x.Value.ToAbsolutePath() : x.Value : null

                }).ToListAsync();
                objResponse = CreateResponse(objResponse.Data, ResponseMessage.Success, true, (int)ApiStatusCode.Ok);
            }
            catch (Exception ex)
            {

                objResponse = CreateResponse<List<GeneralEntryDataViewModel>>(null, ResponseMessage.Fail, false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return objResponse;
        }

    }
}
