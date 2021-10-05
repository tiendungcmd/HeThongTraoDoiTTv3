using DeMo.Models.model;
using System;
using System.Linq;

namespace DeMo.Models
{
    public class LoginDAO
    {
        DADB db = new DADB();
        // KIểm tra đăng nhập
        public bool Login(String tk, String mk)
        {
            //dem so lan xuat hien tu databases
            var rs = db.TaiKhoans.Count(x => x.TenTk == tk && x.MatKhau == mk);
            if (rs > 0)
            {
                return true;
            }
            else
                return false;
        }
        //lay các giá trị quyền truy cập của tài khoản 
        public String Role(String tk)
        {
            //   String roles = db.TaiKhoans.SqlQuery("select Role from TaiKhoan where TenTk='@id' ", new SqlParameter("@id", tk)).ToString();
            var r = from p in db.TaiKhoans where p.TenTk == tk select p;
            String role = "";
            foreach (TaiKhoan taiKhoan in r)
            {
                role = taiKhoan.Role.ToString();
            }
            return role;
        }
        //Lấy mã của tìa khoản MÃ Giáo Viên hoặc Mã Phụ Huynh
        public String Ma(String tk)
        {
            String ma = "";
            var r = from p in db.TaiKhoans where p.TenTk == tk select p;
            int role = int.Parse(Role(tk));
            if (role == 3)
            {
                foreach (TaiKhoan taikhoan in r)
                {
                    ma = taikhoan.MaGV.ToString();
                }
            }
            else if (role == 2)
            {
                foreach (TaiKhoan taikhoan in r)
                {
                    ma = taikhoan.MaPH.ToString();
                }
            }
            return ma;
        }
        //lấy mã học sinh
        public String MaHS(String maph)
        {
            String MaHS = "";
            var ma = (from m in db.PhuHuynhs where m.MaPH == maph select m).ToList();
            foreach(PhuHuynh ph in ma)
            {
                MaHS = ph.MaHS;
            }
               
          
            return MaHS;
        }
    }
}