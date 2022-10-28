using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.ServiceHelper.Model
{
  
    public class ServiceResponse<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }

        public string Message { get; set; }
        public T Data { get; set; }
        public string Exception { get; set; }

        public long? TotalRecord { get; set; }
    }

    public static class ResponseMessage
    {
        public const string Success = "Data retrived successfully...!";
        public const string Found = "Record found...!";
        public const string NotFound = "No record found...!";
        public const string Save = "Record Saved...!";
        public const string Update = "Record Updated...!";
        public const string Delete = "Record Deleted...!";

        public const string InvalidData = "Invalid Data Pass...!";
        public const string UserExist = "User already mapped with mobile or email...!";
        public const string Fail = "Operation Faild...!";
        public const string RestrictedRecord = "Operation Faild due to Restricted Record...!";

        public const string RecordAlreadyExist = "Record already exist, Please try with other !";
        public const string FileUpdated = "File sucessfully uploaded...!";
    }

    public class FilterDropDownPostModel
    {
        public string Key { get; set; }
        public string FileterFromKey { get; set; }
        public long[] Values { get; set; }


    }
}
