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
    
    public partial class tbl_user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_user()
        {
            this.tbl_assessment_general = new HashSet<tbl_assessment_general>();
            this.tbl_feedback_data = new HashSet<tbl_feedback_data>();
            this.tbl_offline_expiry = new HashSet<tbl_offline_expiry>();
            this.tbl_subscriptions = new HashSet<tbl_subscriptions>();
            this.tbl_survey_data = new HashSet<tbl_survey_data>();
            this.tbl_user_data = new HashSet<tbl_user_data>();
        }
    
        public int ID_USER { get; set; }
        public int ID_CODE { get; set; }
        public Nullable<int> ID_ORGANIZATION { get; set; }
        public int ID_ROLE { get; set; }
        public string USERID { get; set; }
        public string PASSWORD { get; set; }
        public string FBSOCIALID { get; set; }
        public string GPSOCIALID { get; set; }
        public string STATUS { get; set; }
        public System.DateTime UPDATEDTIME { get; set; }
        public Nullable<System.DateTime> EXPIRY_DATE { get; set; }
        public string EMPLOYEEID { get; set; }
        public string user_department { get; set; }
        public string user_designation { get; set; }
        public string user_function { get; set; }
        public string user_grade { get; set; }
        public string user_status { get; set; }
        public Nullable<int> reporting_manager { get; set; }
        public Nullable<int> is_reporting { get; set; }
        public string ref_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_assessment_general> tbl_assessment_general { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_feedback_data> tbl_feedback_data { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_offline_expiry> tbl_offline_expiry { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_subscriptions> tbl_subscriptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_survey_data> tbl_survey_data { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_user_data> tbl_user_data { get; set; }
    }
}
