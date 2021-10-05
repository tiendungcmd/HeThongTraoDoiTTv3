using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeMo.Models
{
    public class DiemGV
    {
        [StringLength(8)]
        public int MaDiem { get; set; }

        public int? DiemTx1 { get; set; }

        public int? DiemTx2 { get; set; }

        public int? DiemTx3 { get; set; }

        public int? DiemTx4 { get; set; }

        public int? DiemGK { get; set; }

        public int? DiemCK { get; set; }

        [StringLength(8)]
        public string MaHS { get; set; }
        public string TenHS { get; internal set; }
        [StringLength(20)]
        public string TenMH { get; set; }
        [StringLength(30)]
        public string TenGV { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }
        public int? Nhom { get; set; }
        public int? DiemTBN { get; set; }
        [StringLength(8)]
        public string MaHK { get; set; }
        [StringLength(15)]
        public string TenNH { get; set; }
    }
}