//------------------------------------------------------------------------------
// <auto-generated>
//    這個程式碼是由範本產生。
//
//    對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//    如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DbModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        public long UserId { get; set; }
        public string UserEmail { get; set; }
        public string CompanyNo { get; set; }
        public string UserPwd { get; set; }
        public bool IsActive { get; set; }
        public string Creater { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public string ExtensinNo { get; set; }
        public string PhoneNo { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public string Status { get; set; }
    }
}
