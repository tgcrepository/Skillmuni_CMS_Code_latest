//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace m2ostnext
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_ce_evaluation_progdist_mapping
    {
        public int id_ce_evaluation_progdist_mapping { get; set; }
        public string ce_evaluation_token { get; set; }
        public Nullable<int> id_ce_career_evaluation_master { get; set; }
        public Nullable<int> id_brief_question { get; set; }
        public Nullable<int> id_user { get; set; }
        public Nullable<int> question_link_type { get; set; }
        public Nullable<System.DateTime> date_time_stamp { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
    }
}
