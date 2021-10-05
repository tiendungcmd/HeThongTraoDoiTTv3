namespace DeMo.Models.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [Key]
        public int MaBL { get; set; }

        [StringLength(8)]
        public string MaThongBao { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayBL { get; set; }

        [StringLength(8)]
        public string MaPH { get; set; }

        [StringLength(500)]
        public string NoiDungBL { get; set; }

        public virtual PhuHuynh PhuHuynh { get; set; }

        public virtual ThongBao ThongBao { get; set; }
    }
}
