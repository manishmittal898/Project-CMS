﻿namespace CMS.Core.ServiceHelper.Model
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
        public const string Success = "Data retrived successfully.";
        public const string Found = "Record found.";
        public const string NotFound = "No record found.";
        public const string Save = "Record saved sucessfully.";
        public const string Update = "Record update sucessfully.";
        public const string Delete = "Record delete sucessfully.";
        public const string UpdateDenied = "Record can't be update. Please try another record";
        public const string DeleteDenied = "Record can't be deleted. Please try another record";

        public const string InvalidData = "Invalid data pass.";
        public const string UserExist = "User already mapped with mobile or email.";
        public const string Fail = "Operation Faild.";
        public const string RestrictedRecord = "Operation Faild due to Restricted Record.";
        public const string OTPMissMatch = "OTP not matched.";
        public const string OTPSent = "OTP Sent Successfully.";
        public const string OTPVerificatoinSuccess = "OTP Verification Successfully.";
        public const string OTPMissMatched = "Wrong OTP submitted.";


        public const string RecordAlreadyExist = "Record already exist, Please try with other !";
        public const string FileUpdated = "File sucessfully uploaded.";
        public const string Logout = "Logout sucessfully.";

    }

    public class FilterDropDownPostModel
    {
        public string Key { get; set; }
        public string FileterFromKey { get; set; }
        public string[] Values { get; set; }


    }
}
