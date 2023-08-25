using CMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Service.Services.CustomerAddress
{
    public class CustomerAddressViewModel
    {
  
        public string Id { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string BuildingNumber { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string AddressType { get; set; }
        public bool IsPrimary { get; set; }
      
      
        public string AddressTypeName { get; set; }
        public string State { get; set; }
         }

    public class CustomerAddressPostModel
    {

        public string Id { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string BuildingNumber { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }
        public string AddressType { get; set; }
        public bool IsPrimary { get; set; }

    }
}
