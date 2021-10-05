namespace DeMo.Models.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HocKi")]
    public partial class HocKi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HocKi()
        {
            Diems = new HashSet<Diem>();
        }

        [Key]
        [StringLength(8)]
        public string MaHocKi { get; set; }

        [StringLength(20)]
        public string TenHocKi { get; set; }

        [StringLength(8)]
        public string MaNH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diem> Diems { get; set; }

        public virtual NamHoc NamHoc { get; set; }
    }
}
