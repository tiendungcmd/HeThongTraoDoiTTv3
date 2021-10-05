namespace DeMo.Models.model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DADB : DbContext
    {
        public DADB()
            : base("name=DADB")
        {
        }

        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<Diem> Diems { get; set; }
        public virtual DbSet<GiaoVien> GiaoViens { get; set; }
        public virtual DbSet<HocKi> HocKis { get; set; }
        public virtual DbSet<HocSinh> HocSinhs { get; set; }
        public virtual DbSet<LH_HS> LH_HS { get; set; }
        public virtual DbSet<LopHoc> LopHocs { get; set; }
        public virtual DbSet<MonHoc> MonHocs { get; set; }
        public virtual DbSet<NamHoc> NamHocs { get; set; }
        public virtual DbSet<PhuHuynh> PhuHuynhs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<ThongBao> ThongBaos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BinhLuan>()
                .Property(e => e.MaThongBao)
                .IsUnicode(false);

            modelBuilder.Entity<BinhLuan>()
                .Property(e => e.MaPH)
                .IsUnicode(false);

            modelBuilder.Entity<Diem>()
                .Property(e => e.MaHS)
                .IsUnicode(false);

            modelBuilder.Entity<Diem>()
                .Property(e => e.MaGVPT)
                .IsUnicode(false);

            modelBuilder.Entity<Diem>()
                .Property(e => e.MaHK)
                .IsUnicode(false);

            modelBuilder.Entity<Diem>()
                .Property(e => e.MaMH)
                .IsUnicode(false);

            modelBuilder.Entity<GiaoVien>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<GiaoVien>()
                .Property(e => e.MaLop)
                .IsUnicode(false);

            modelBuilder.Entity<GiaoVien>()
                .Property(e => e.MaMH)
                .IsUnicode(false);

            modelBuilder.Entity<GiaoVien>()
                .HasMany(e => e.Diems)
                .WithOptional(e => e.GiaoVien)
                .HasForeignKey(e => e.MaGVPT);

            modelBuilder.Entity<GiaoVien>()
                .HasMany(e => e.LopHocs)
                .WithOptional(e => e.GiaoVien)
                .HasForeignKey(e => e.GVCN);

            modelBuilder.Entity<HocKi>()
                .Property(e => e.MaHocKi)
                .IsUnicode(false);

            modelBuilder.Entity<HocKi>()
                .Property(e => e.MaNH)
                .IsUnicode(false);

            modelBuilder.Entity<HocKi>()
                .HasMany(e => e.Diems)
                .WithOptional(e => e.HocKi)
                .HasForeignKey(e => e.MaHK);

            modelBuilder.Entity<HocSinh>()
                .Property(e => e.MaHS)
                .IsUnicode(false);

            modelBuilder.Entity<HocSinh>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<HocSinh>()
                .HasMany(e => e.LH_HS)
                .WithRequired(e => e.HocSinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LH_HS>()
                .Property(e => e.MaLopHoc)
                .IsUnicode(false);

            modelBuilder.Entity<LH_HS>()
                .Property(e => e.MaHS)
                .IsUnicode(false);

            modelBuilder.Entity<LH_HS>()
                .Property(e => e.MaNamHoc)
                .IsUnicode(false);

            modelBuilder.Entity<LopHoc>()
                .Property(e => e.MaLopHoc)
                .IsUnicode(false);

            modelBuilder.Entity<LopHoc>()
                .Property(e => e.GVCN)
                .IsUnicode(false);

            modelBuilder.Entity<LopHoc>()
                .HasMany(e => e.GiaoViens)
                .WithOptional(e => e.LopHoc)
                .HasForeignKey(e => e.MaLop);

            modelBuilder.Entity<LopHoc>()
                .HasMany(e => e.LH_HS)
                .WithRequired(e => e.LopHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MonHoc>()
                .Property(e => e.MaMH)
                .IsUnicode(false);

            modelBuilder.Entity<NamHoc>()
                .Property(e => e.MaNamHoc)
                .IsUnicode(false);

            modelBuilder.Entity<NamHoc>()
                .HasMany(e => e.HocKis)
                .WithOptional(e => e.NamHoc)
                .HasForeignKey(e => e.MaNH);

            modelBuilder.Entity<PhuHuynh>()
                .Property(e => e.MaPH)
                .IsUnicode(false);

            modelBuilder.Entity<PhuHuynh>()
                .Property(e => e.MaHS)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TenTk)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MaPH)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<ThongBao>()
                .Property(e => e.MaThongBao)
                .IsUnicode(false);

            modelBuilder.Entity<ThongBao>()
                .Property(e => e.MaGV)
                .IsUnicode(false);

            modelBuilder.Entity<ThongBao>()
                .Property(e => e.MaNhan)
                .IsUnicode(false);
        }
    }
}
