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
    
    public partial class tbl_category_tiles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_category_tiles()
        {
            this.tbl_category_associantion = new HashSet<tbl_category_associantion>();
            this.tbl_category_heading = new HashSet<tbl_category_heading>();
        }
    
        public int id_category_tiles { get; set; }
        public string tile_heading { get; set; }
        public Nullable<int> category_theme { get; set; }
        public Nullable<int> id_organization { get; set; }
        public string tile_image { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> updated_date_time { get; set; }
        public Nullable<int> category_order { get; set; }
        public string image_url { get; set; }
        public Nullable<int> id_default { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_category_associantion> tbl_category_associantion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_category_heading> tbl_category_heading { get; set; }
        public virtual tbl_organization tbl_organization { get; set; }
    }
}
