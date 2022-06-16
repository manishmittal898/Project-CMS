using CMS.Core.FixedValue;
using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using CMS.Service.Utility;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.ProductMaster
{
    public class ProductMasterService : BaseService, IProductMasterService
    {
        DB_CMSContext _db;
        private readonly FileHelper _fileHelper;
        public ProductMasterService(DB_CMSContext db, IHostingEnvironment environment)
        {
            _db = db;
            _fileHelper = new FileHelper(environment);
        }


        public ServiceResponse<IEnumerable<Data.Models.TblProductMaster>> GetList()
        {
            ServiceResponse<IEnumerable<Data.Models.TblProductMaster>> objResult = new ServiceResponse<IEnumerable<Data.Models.TblProductMaster>>();
            try
            {
                var objData = _db.TblProductMasters.ToList();
                objResult = CreateResponse(objData as IEnumerable<Data.Models.TblProductMaster>, "Success", true);
            }
            catch (Exception)
            {

                objResult.Data = null;
                objResult.IsSuccess = false;
                objResult.Message = string.Empty;
            }
            return objResult;
        }
        public ServiceResponse<TblProductMaster> GetById(int id)
        {
            ServiceResponse<TblProductMaster> ObjResponse = new ServiceResponse<TblProductMaster>();
            try
            {

                var detail = _db.TblProductMasters.FirstOrDefault(x => x.Id == id && x.IsActive.Value);
                ObjResponse = CreateResponse(detail, "Success", true);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<TblProductMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblProductMaster>> Save(ProductMasterViewModel model)
        {
            try
            {
                TblProductMaster objProduct = new TblProductMaster();
                // objProduct.RoleId = model.RoleId;
                objProduct.Name = model.Name;
                objProduct.CategoryId = model.CategoryId;
                objProduct.SubCategoryId = model.SubCategoryId;
                objProduct.ImagePath = _fileHelper.Save(model.ImagePath, FilePaths.ProductImages_Main);
                objProduct.Desc = model.Desc;
                objProduct.Price = model.Price;
                objProduct.Summary = model.Summary;
                objProduct.Caption = model.Caption;
                objProduct.IsActive = true;
                objProduct.CreatedBy = model.CreatedBy;
                var roletype = await _db.TblProductMasters.AddAsync(objProduct);
                _db.SaveChanges();
                return CreateResponse(objProduct, "Added", true);


            }
            catch (Exception ex)
            {

                return CreateResponse<TblProductMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError , ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblProductMaster>> Edit(int id, ProductMasterViewModel model)
        {
            try
            {
                TblProductMaster objProduct = new TblProductMaster();

                objProduct = _db.TblProductMasters.FirstOrDefault(r => r.Id == id);

                objProduct.Name = model.Name;
                objProduct.CategoryId = model.CategoryId;
                objProduct.SubCategoryId = model.SubCategoryId;
                objProduct.ImagePath = _fileHelper.Save(model.ImagePath, FilePaths.ProductImages_Main);
                objProduct.Desc = model.Desc;
                objProduct.Price = model.Price;
                objProduct.Summary = model.Summary;
                objProduct.Caption = model.Caption;

                objProduct.ModifiedBy = model.ModifiedBy;
                var roletype = _db.TblProductMasters.Update(objProduct);
                _db.SaveChanges();


                return CreateResponse(objProduct, "Updated", true);

            }
            catch (Exception ex)
            {

                return CreateResponse<TblProductMaster>(null, "Fail", false, (int)ApiStatusCode.InternalServerError , ex.Message.ToString());

            }

        }


        public async Task<ServiceResponse<TblProductMaster>> Delete(int id)
        {
            try
            {
                TblProductMaster objRole = new TblProductMaster();
                objRole = _db.TblProductMasters.FirstOrDefault(r => r.Id == id);

                var roletype = _db.TblProductMasters.Remove(objRole);
                _db.SaveChangesAsync();
                return CreateResponse(objRole, "Deleted", true);
            }
            catch (Exception ex)
            {
                return null;

            }


        }

    }
}
