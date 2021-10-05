using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeMo.Models
{
    public class ThongBaoViewModel
    {
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

        [StringLength(8)]
        public string MaNhan { get; set; }
        [StringLength(30)]
        public string TenPh { get; set; }
        [StringLength(30)]
        public string TenHS { get; set; }
        [Column(TypeName = "date")]
        public DateTime NgayTB { get; set; }
        [StringLength(30)]
        public string TenGV { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayBL { get; set; }

        [StringLength(500)]
        public string NoiDungBL { get; set; }
        [StringLength(8)]
        public string MaPH { get; set; }
    }
}