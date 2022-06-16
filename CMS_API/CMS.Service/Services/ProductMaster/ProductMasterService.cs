using CMS.Core.ServiceHelper.Method;
using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
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
        public ProductMasterService(DB_CMSContext db)
        {
            _db = db;
        }


        public ServiceResponse<IEnumerable<Data.Models.TblProductMaster>> GetList()
        {
            ServiceResponse<IEnumerable<Data.Models.TblProductMaster>> objResult = new ServiceResponse<IEnumerable<Data.Models.TblProductMaster>>();
            try
            {
                var objData = _db.TblLookupTypeMasters.ToList();
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

                var detail = _db.TblProductMasters.FirstOrDefault(x => x.Id == id && x.IsActive);
                ObjResponse = CreateResponse(detail, "Success", true);
            }
            catch (Exception ex)
            {

                ObjResponse = CreateResponse<TblProductMaster>(null, "Fail", false, ex.Message.ToString());

            }
            return ObjResponse;
        }

        public async Task<ServiceResponse<TblProductMaster>> Save(ProductMasterViewModel model)
        {
            try
            {
                TblProductMaster objRole = new TblProductMaster();
                // objRole.RoleId = model.RoleId;
                objRole.Name = model.Name;
                objRole.Desc = model.Desc;
                objRole.Price = model.Price;
                objRole.Summary = model.Summary;
                objRole.Caption = model.Caption;
                objRole.IsActive = true;
                objRole.CreatedBy = model.CreatedBy;
                var roletype = await _db.TblProductMasters.AddAsync(objRole);
                _db.SaveChanges();
                return CreateResponse(objRole, "Added", true);


            }
            catch (Exception ex)
            {

                return CreateResponse<TblProductMaster>(null, "Fail", false, ex.Message.ToString());

            }
        }

        public async Task<ServiceResponse<TblProductMaster>> Edit(int id, ProductMasterViewModel model)
        {
            try
            {
                TblProductMaster objRole = new TblProductMaster();

                objRole = _db.TblProductMasters.FirstOrDefault(r => r.Id == id);

                objRole.Name = model.Name;
                objRole.Desc = model.Desc;
                objRole.Price = model.Price;
                objRole.Summary = model.Summary;
                objRole.Caption = model.Caption;

                objRole.ModifiedBy = model.ModifiedBy;
                var roletype = _db.TblProductMasters.Update(objRole);
                _db.SaveChanges();


                return CreateResponse(objRole, "Updated", true);

            }
            catch (Exception ex)
            {

                return CreateResponse<TblProductMaster>(null, "Fail", false, ex.Message.ToString());

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
