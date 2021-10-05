using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeMo.Models.models
{
    public class HocSinhViewModel
    {
        [StringLength(8)]
        public string MaHS { get; set; }

        [StringLength(30)]
        public string TenHS { get; set; }

        [StringLength(3)]
        public string GioiTinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgaySinh { get; set; }

        [StringLength(50)]
        public string DiaChi { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        public int? SDT { get; set; }
        [StringLength(10)]
        public string TenLop { get; set; }
        [StringLength(8)]
        public string MaGV { get; set; }

        [StringLength(30)]
        public string TenGV { get; set; }

        [StringLength(8)]
        public string MaLop { get; set; }
        [StringLength(8)]
        public string MaPH { get; set; }

        [StringLength(30)]
        public string TenPh { get; set; }
    }
}