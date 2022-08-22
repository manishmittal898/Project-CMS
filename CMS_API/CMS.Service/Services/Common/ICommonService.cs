﻿using CMS.Core.ServiceHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.Common
{
   public interface ICommonService
    {
        Task<ServiceResponse<Dictionary<string, object>>> GetDropDown(string[] key, bool isTransactionData = false);
        Task<ServiceResponse<Dictionary<string, object>>> GetFilterDropDown(FilterDropDownPostModel[] model);

    }
}
