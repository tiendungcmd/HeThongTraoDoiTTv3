namespace DeMo.Models.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ThongBao")]
    public partial class ThongBao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ThongBao()
        {
            BinhLuans = new HashSet<BinhLuan>();
        }

        [Key]
        [StringLength(8)]
        public string MaThongBao { get; set; }

        [StringLength(8)]
        public string MaGV { get; set; }

        [StringLength(30)]
        public string TieuDe { get; set; }

        [StringLength(500)]
        public string NoiDung { get; set; }

        [StringLength(20)]
        public string LinkFile { get; set; }

        [Required]
        [StringLength(8)]
        public string MaNhan { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayTB { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        public virtual GiaoVien GiaoVien { get; set; }
    }
}
