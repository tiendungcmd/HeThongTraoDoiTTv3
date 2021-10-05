namespace DeMo.Models.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class LH_HS
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string MaLopHoc { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string MaHS { get; set; }

        [StringLength(8)]
        public string MaNamHoc { get; set; }

        public virtual HocSinh HocSinh { get; set; }

        public virtual LopHoc LopHoc { get; set; }

        public virtual NamHoc NamHoc { get; set; }
    }
}
