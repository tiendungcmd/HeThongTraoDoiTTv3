namespace DeMo.Models.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        [StringLength(20)]
        public string TenTk { get; set; }

        [StringLength(20)]
        public string MatKhau { get; set; }

        public int? Role { get; set; }

        [StringLength(8)]
        public string MaPH { get; set; }

        [StringLength(8)]
        public string MaGV { get; set; }

        [StringLength(50)]
        public string token { get; set; }
        public virtual GiaoVien GiaoVien { get; set; }

        public virtual PhuHuynh PhuHuynh { get; set; }
    }
}
