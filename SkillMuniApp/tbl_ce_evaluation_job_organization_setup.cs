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
    
    public partial class tbl_ce_evaluation_job_organization_setup
    {
        public int id_ce_evaluation_job_organization_setup { get; set; }
        public Nullable<int> id_job_organization { get; set; }
        public Nullable<int> id_ce_evaluation_jobrole { get; set; }
        public Nullable<int> job_setup_type { get; set; }
        public Nullable<int> organization_benchmark_jobpoint { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
    }
}
