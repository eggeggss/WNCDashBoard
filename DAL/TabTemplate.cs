//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TabTemplate
    {
        public int id_tab { get; set; }
        public Nullable<int> id_group { get; set; }
        public string tab_name { get; set; }
        public Nullable<System.DateTime> dt_create { get; set; }
        public Nullable<System.DateTime> dt_update { get; set; }
        public Nullable<int> stat_void { get; set; }
        public string htmltemplate { get; set; }
    }
}
