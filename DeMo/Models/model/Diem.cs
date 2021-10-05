namespace DeMo.Models.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Diem")]
    public partial class Diem
    {
        [Key]
        public int MaDiem { get; set; }

        public int? DiemTx1 { get; set; }

        public int? DiemTx2 { get; set; }

        public int? DiemTx3 { get; set; }

        public int? DiemTx4 { get; set; }

        public int? DiemGK { get; set; }

        public int? DiemCK { get; set; }

        [StringLength(8)]
        public string MaHS { get; set; }

        [StringLength(8)]
        public string MaGVPT { get; set; }

        [StringLength(8)]
        public string MaHK { get; set; }

        [StringLength(8)]
        public string MaMH { get; set; }

        public virtual GiaoVien GiaoVien { get; set; }

        public virtual HocKi HocKi { get; set; }

        public virtual HocSinh HocSinh { get; set; }

        public virtual MonHoc MonHoc { get; set; }
    }
}
