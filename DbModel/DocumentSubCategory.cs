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
    
    public partial class DocumentSubCategory
    {
        public long SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<long> LastUpdate { get; set; }
        public long CategoryNo { get; set; }
        public Nullable<long> ParentNo { get; set; }
    }
}
