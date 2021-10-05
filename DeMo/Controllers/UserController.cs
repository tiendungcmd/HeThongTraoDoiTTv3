using DeMo.Models;
using DeMo.Models.model;
using DeMo.Models.models;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;


namespace DeMo.Controllers
{
    public class UserController : Controller
    {
        DADB db = new DADB();
        // GET: User
        public ActionResult Index()
        {

            var ma = Session["MaPH"];
            var mshs = Session["MaHS"];
            var tt = from i in db.HocSinhs
                     join lh in db.LH_HS on i.MaHS equals lh.MaHS
                     join lop in db.LopHocs on lh.MaLopHoc equals lop.MaLopHoc
                     join ph in db.PhuHuynhs on i.MaHS equals ph.MaHS
                     where ph.MaPH == ma
                     select new HocSinhViewModel()
                     {
                         TenHS = i.TenHS,
                         TenLop = lop.TenLop,
                         GioiTinh = i.GioiTinh,
                         NgaySinh = i.NgaySinh,
                         DiaChi = i.DiaChi,
                         Email = i.Email

                     };
            return View(tt);
        }
        //lay diem hoc ki 1
        public ActionResult DiemHK1(string id)
        {

            var di = from diem in db.Diems
                     join hs in db.HocSinhs on diem.MaHS equals hs.MaHS
                     join gvpt in db.GiaoViens on diem.MaGVPT equals gvpt.MaGV
                     join m in db.MonHocs on gvpt.MaMH equals m.MaMH
                     join hk in db.HocKis on diem.MaHK equals hk.MaHocKi
                     join nh in db.NamHocs on hk.MaNH equals nh.MaNamHoc
                     where diem.MaHS == id && diem.MaHK == "HK1"
                     orderby m.Nhom descending
                     select new DiemGV()
                     {
                         MaDiem = diem.MaDiem,
                         DiemCK = diem.DiemCK,
                         DiemTx1 = diem.DiemTx1,
                         DiemTx2 = diem.DiemTx2,
                         DiemGK = diem.DiemGK,
                         DiemTx3 = diem.DiemTx3,
                         DiemTx4 = diem.DiemTx4,
                         TenHS = hs.TenHS,
                         TenGV = gvpt.TenGV,
                         NgaySinh = gvpt.NgaySinh,
                         TenMH = m.TenMH,
                         Nhom = m.Nhom,
                         TenNH = nh.TenNH
                     };
            foreach (DiemGV hs in di)
            {
                ViewData["TenHS"] = hs.TenHS;
                ViewData["TenNH"] = hs.TenNH;
            }

            return View(di);
        }
        //lay ket qua diem hoc ki 2
        public ActionResult DiemHK2(string id)
        {
            var di = from diem in db.Diems
                     join hs in db.HocSinhs on diem.MaHS equals hs.MaHS
                     join gvpt in db.GiaoViens on diem.MaGVPT equals gvpt.MaGV
                     join m in db.MonHocs on gvpt.MaMH equals m.MaMH
                     join hk in db.HocKis on diem.MaHK equals hk.MaHocKi
                     join nh in db.NamHocs on hk.MaNH equals nh.MaNamHoc
                     where diem.MaHS == id && diem.MaHK == "HK2"
                     orderby m.Nhom descending
                     select new DiemGV()
                     {
                         MaDiem = diem.MaDiem,
                         DiemCK = diem.DiemCK,
                         DiemTx1 = diem.DiemTx1,
                         DiemTx2 = diem.DiemTx2,
                         DiemGK = diem.DiemGK,
                         DiemTx3 = diem.DiemTx3,
                         DiemTx4 = diem.DiemTx4,
                         TenHS = hs.TenHS,
                         TenGV = gvpt.TenGV,
                         NgaySinh = gvpt.NgaySinh,
                         TenMH = m.TenMH,
                         Nhom = m.Nhom,
                         TenNH = nh.TenNH
                     };
            foreach (DiemGV hs in di)
            {
                ViewData["TenHS"] = hs.TenHS;
                ViewData["TenNH"] = hs.TenNH;
            }

            return View(di);
        }
        public ActionResult CuoiKi(string id)
        {
            var di = from diem in db.Diems
                     join hs in db.HocSinhs on diem.MaHS equals hs.MaHS
                     join gvpt in db.GiaoViens on diem.MaGVPT equals gvpt.MaGV
                     join m in db.MonHocs on gvpt.MaMH equals m.MaMH
                     join hk in db.HocKis on diem.MaHK equals hk.MaHocKi
                     join nh in db.NamHocs on hk.MaNH equals nh.MaNamHoc
                     where diem.MaHS == id && diem.MaHK == "HK1"
                     orderby m.Nhom descending
                     select new DiemGV()
                     {
                         MaDiem = diem.MaDiem,
                         DiemCK = diem.DiemCK,
                         DiemTx1 = diem.DiemTx1,
                         DiemTx2 = diem.DiemTx2,
                         DiemGK = diem.DiemGK,
                         DiemTx3 = diem.DiemTx3,
                         DiemTx4 = diem.DiemTx4,
                         TenHS = hs.TenHS,
                         TenGV = gvpt.TenGV,
                         NgaySinh = gvpt.NgaySinh,
                         TenMH = m.TenMH,
                         Nhom = m.Nhom,
                         MaHK = diem.MaHK,
                         TenNH = nh.TenNH
                     };
            foreach (DiemGV hs in di)
            {
                ViewData["TenHS"] = hs.TenHS;
                ViewData["TenNH"] = hs.TenNH;
            }
            return View(di);
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Login");

        }
        // lay thong baso
        public ActionResult ThongBao()
        {
            var ma = "1";
            var maph = Session["MaPH"];
            var maHS = Session["MaHS"];
            var tb = from t in db.ThongBaos
                     where (t.MaNhan == ma || t.MaNhan == maHS)
                     orderby t.NgayTB descending
                     select t;
            return View(tb);
        }
        public ActionResult DocThongBao(String id)
        {
            ViewData["idTB"] = id;

            return View();


        }
        public ActionResult ThemBL()
        {
            //lấy ngày hên tại
            DateTime now = new DateTime();
            BinhLuan bl = new BinhLuan();
            //gán dữ liệu từ form vào đối tượng
            bl.MaPH = Session["MaPH"].ToString();
            bl.MaThongBao = Request.Form["MaThongBao"];
            bl.MaPH = Request.Form["MaPH"];
            bl.NgayBL = now;
            bl.NoiDungBL = Request.Form["NoiDungBL"];
            //thêm đối tượng vào data base
            db.BinhLuans.Add(bl);
            db.SaveChanges();
            return RedirectToAction("ThongBao");
        }
        [HttpGet]
        public JsonResult ChiTiet(string tenMH)
        {
            try
            {
                var gv = from giaovien in db.GiaoViens
                         join mh in db.MonHocs on giaovien.MaMH equals mh.MaMH
                         where mh.TenMH == tenMH
                         select new
                         {
                             TenGV = giaovien.TenGV,
                             SDT = giaovien.SDT,
                             GioiTinh = giaovien.GioiTinh,
                             DiaChi = giaovien.DiaChi,
                             TenMh = mh.TenMH
                         };
                return Json(new { code = 200, gv = gv }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "loi roi"  }, JsonRequestBehavior.AllowGet);
            }

        }

        //lấy id thong báo
        public ActionResult DetailThongBao(string id)
        {

            ViewData["idTB"] = id;

            return View();
        }
        //lấy danh sách thông báo
        public JsonResult dsTB(int page = 1, int pageSize = 3)
        {
            var ma = "1";
            var maHS = Session["MaHS"];
            try
            {
                var dsTB = (from i in db.ThongBaos
                            where (i.MaNhan == ma || i.MaNhan == maHS)
                            orderby i.NgayTB descending
                            select new
                            {
                                MaThongBao = i.MaThongBao,
                                MaGV = i.MaGV,
                                TieuDe = i.TieuDe,
                                NoiDung = i.NoiDung,
                                MaNhan = i.MaNhan,
                                LinkFile = i.LinkFile,
                                NgayTB = i.NgayTB.ToString()
                            }).ToList();
                int totalRow = dsTB.Count();
                var ds = dsTB.Skip((page - 1) * pageSize).Take(pageSize);
                return Json(new { code = 200, dsTb = ds, total = totalRow }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Loi"  }, JsonRequestBehavior.AllowGet);
            }
        }
        //đọc binh luan
        [HttpGet]
        public JsonResult detailBL(string id)
        {
            try
            {
                var dsBL = from bl in db.BinhLuans
                           join ph in db.PhuHuynhs on bl.MaPH equals ph.MaPH
                           into t
                           from rt in t.DefaultIfEmpty()
                           where bl.MaThongBao == id
                           orderby bl.NgayBL
                           select new
                           {
                               maBL = bl.MaBL,
                               ngayBl = bl.NgayBL,
                               maPH = bl.MaPH,
                               noiDung = bl.NoiDungBL,
                               maThongBao = bl.MaThongBao,
                               tenPH = rt.TenPh
                           };
                return Json(new { code = 200, dsBl = dsBL }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Tải bình luận lỗi"  }, JsonRequestBehavior.AllowGet);
            }
        }
        //chi tiet thong bao
        [HttpGet]
        public JsonResult detailTB(string id)
        {
            try
            {
                var tb = (from t in db.ThongBaos
                          join gv in db.GiaoViens on t.MaGV equals gv.MaGV
                          where t.MaThongBao == id
                          select new
                          {
                              TenGV = gv.TenGV,
                              TieuDe = t.TieuDe,
                              NoiDung = t.NoiDung,
                              NgayTB = t.NgayTB.ToString(),
                              MaNhan = t.MaNhan,
                              MaThongBao = t.MaThongBao,


                          }).ToList();
                return Json(new { code = 200, dstb = tb }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "That bai!"  }, JsonRequestBehavior.AllowGet);
            }
        }
        //thêm bình luận 
        public JsonResult addBL(string NoiDungBL, string MaThongBao, DateTime ngayBL)
        {
            try
            {
                var maph = Session["MaPH"];
                // thêm bình luận

                var bl = new BinhLuan();
                bl.MaThongBao = MaThongBao;
                bl.NoiDungBL = NoiDungBL;
                bl.NgayBL = ngayBL;
                bl.MaPH = maph.ToString();

                db.BinhLuans.Add(bl);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lỗi"  }, JsonRequestBehavior.AllowGet);
            }

        }


        //xóa bình luân
        public JsonResult deleteBL(int maBL)
        {
            try
            {
                BinhLuan bl = db.BinhLuans.Find(maBL);
                db.BinhLuans.Remove(bl);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa không thành công"  }, JsonRequestBehavior.AllowGet);
            }

        }

        //đổi Mk
        public ActionResult DoiMK()
        {

            return View();
        }
        //ddooi MK
        public JsonResult DoiMK1(string mk, string mk1)
        {
            var maph = Session["MaPH"];
            var tk = from taikhoan in db.TaiKhoans where taikhoan.MaPH == maph && taikhoan.MatKhau == mk select taikhoan;
            int kt = tk.Count();
            if (kt > 0)
            {
                foreach(TaiKhoan tai in tk)
                {
                    tai.MatKhau = mk1;
                }
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thay đổi Thành Công" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 500, msg = "Mật khẩu không đúng!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}