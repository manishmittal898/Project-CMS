﻿using CMS.Core.ServiceHelper.Model;
using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service.Services.CMSPage
{
    public interface ICMSPageService
    {
        Task<ServiceResponse<IEnumerable<CMSPageListViewModel>>> GetList(IndexModel model);
        Task<ServiceResponse<List<CMSPageViewModel>>> GetById(long id);
        Task<ServiceResponse<string>> Save(CMSPagePostModel model);
        Task<ServiceResponse<TblCmspageContentMaster>> ActiveStatusUpdate(long id);

        Task<ServiceResponse<TblCmspageContentMaster>> Delete(long id);
    }
}
