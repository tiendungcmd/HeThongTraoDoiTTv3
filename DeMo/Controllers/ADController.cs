using DeMo.Models.model;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DeMo.Controllers
{
    public class ADController : Controller
    {
       DADB db = new DADB();
        // GET: AD
        public ActionResult Index()
        {
            return View();
        }
        //lay danh sách lớp
        [HttpGet]
        public JsonResult DsLop()
        {
            try
            {// lấy danh sách phân trang
                var dsLop = (from l in db.LopHocs
                             join gv in db.GiaoViens on l.GVCN equals gv.MaGV

                             select new
                             {
                                 MaLop = l.MaLopHoc,
                                 TenLop = l.TenLop,
                                 PhongHoc = l.PhongHoc,
                                 TenGV = gv.TenGV
                             }).ToList();
                //tính số record

                return Json(new { code = 200, ds = dsLop }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "looi" }, JsonRequestBehavior.AllowGet);
            }

        }


        //lấy danh sách giáo viên
        [HttpGet]
        public JsonResult DsGV(int page = 1, int pageSize = 3)
        {
            var dsGV = (from gv in db.GiaoViens
                        join mh in db.MonHocs on gv.MaMH equals mh.MaMH
                        join lop in db.LopHocs on gv.MaLop equals lop.MaLopHoc
                        select new
                        {
                            maGV = gv.MaGV,
                            TenGV = gv.TenGV,
                            NgaySinh = gv.NgaySinh.ToString(),
                            GioiTinh = gv.GioiTinh,
                            DiaChi = gv.DiaChi,
                            SDT = gv.SDT,
                            MonHoc = mh.TenMH,
                            Lop = lop.TenLop
                        }).ToList();
            int totalRow = dsGV.Count();
            var ds = dsGV.Skip((page - 1) * pageSize).Take(pageSize);

            return Json(new { code = 200, dsGv = ds, total = totalRow }, JsonRequestBehavior.AllowGet);
        }

        //lấy danh sách môn học
        [HttpGet]
        public JsonResult DsMonHoc()
        {
            try
            {
                var dsMH = (from mh in db.MonHocs
                            select new
                            {
                                maMH = mh.MaMH,
                                tenMH = mh.TenMH

                            }).ToList();
                return Json(new { code = 200, dsMH = dsMH }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var dsMH = from mh in db.MonHocs select mh;
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }

        }

        //thêm giáo viên
        [HttpPost]
        public JsonResult AddGV(string maGV, string tenGV, int SDT, string diaChi, string monHoc, string Lop, string gioiTinh, DateTime ngaySinh)
        {
            var ds = from i in db.GiaoViens where i.MaGV == maGV select i;
            int kiemtra = ds.Count();
            if (kiemtra == 0)
            {
                try
                {
                    //gán giá trị cho giáo viên
                    var gv = new GiaoVien();
                    gv.TenGV = tenGV;
                    gv.MaGV = maGV;
                    gv.SDT = SDT;
                    gv.DiaChi = diaChi;
                    gv.MaMH = monHoc;
                    gv.MaLop = Lop;
                    gv.NgaySinh = ngaySinh;
                    gv.GioiTinh = gioiTinh;

                    db.GiaoViens.Add(gv);
                    db.SaveChanges();//lưu vào db
                    return Json(new { code = 200, msg = "Thêm Thành Công" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { code = 500, msg = "Lỗi, Mời nhập lại" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { code = 500, msg = "Mã Giáo Viên trùng, Mời bạn nhập lại" }, JsonRequestBehavior.AllowGet);
            }

        }

        //sửa giáo viên
        [HttpPost]
        public JsonResult EditGV(string maGV, string tenGV, int SDT, string diaChi, string monHoc, string Lop, string gioiTinh, DateTime ngaySinh)
        {
            var ds = from i in db.GiaoViens where i.MaGV == maGV select i;
            int kiemtra = ds.Count();
            //kiểm tra nếu chưa có trong database thì thêm mới
            if (kiemtra == 0)
            {
                try
                {
                    //gán giá trị cho giáo viên
                    var gv = new GiaoVien();
                    gv.TenGV = tenGV;
                    gv.MaGV = maGV;
                    gv.SDT = SDT;
                    gv.DiaChi = diaChi;
                    gv.MaMH = monHoc;
                    gv.MaLop = Lop;
                    gv.NgaySinh = ngaySinh;
                    gv.GioiTinh = gioiTinh;

                    db.GiaoViens.Add(gv);
                    db.SaveChanges();//lưu vào db
                    return Json(new { code = 200, msg = "Cập nhật Thành Công" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { code = 500, msg = "Lỗi, Mời nhập lại" }, JsonRequestBehavior.AllowGet);
                }
            }
            //có trong db thì sửa db
            else
            {
                foreach (GiaoVien gv in ds)
                {
                    gv.TenGV = tenGV;

                    gv.SDT = SDT;
                    gv.DiaChi = diaChi;
                    gv.MaMH = monHoc;
                    gv.MaLop = Lop;
                    gv.NgaySinh = ngaySinh;
                    gv.GioiTinh = gioiTinh;
                }
                db.SaveChanges();//lưu vào db
                return Json(new { code = 200, msg = "Cập nhật Thành Công" }, JsonRequestBehavior.AllowGet);

            }
        }

        //xóa giáo viên
        [HttpPost]
        public JsonResult DelGV(string maGV)
        {
            try
            {
                // GiaoVien gv = new GiaoVien();
                GiaoVien gv = db.GiaoViens.Find(maGV);
                db.GiaoViens.Remove(gv);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa không thành công" }, JsonRequestBehavior.AllowGet);
            }
        }

        //lấy thông tin giáo viên theo id
        public JsonResult DetailGv(string id)
        {
            try
            {
                var detail = from gv in db.GiaoViens
                             where gv.MaGV == id
                             select new
                             {
                                 MaGV = gv.MaGV,
                                 TenGV = gv.TenGV,
                                 NgaySinh = gv.NgaySinh.ToString(),
                                 GioiTinh = gv.GioiTinh,
                                 DiaChi = gv.DiaChi,
                                 SDT = gv.SDT,
                                 MaLop = gv.MaLop,
                                 MaMH = gv.MaMH
                             };
                return Json(new { code = 200, detailGv = detail }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "loi roi" }, JsonRequestBehavior.AllowGet);
            }

        }

        //logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Login");

        }
        //index lop
        public ActionResult Lop()
        {
            return View();
        }
        //index thong bao
        public ActionResult ThongBao()
        {
            return View();
        }

        //lấy danh sách thông báo
        [HttpGet]
        public JsonResult dsTB(int page = 1, int pageSize = 3)
        {
            try
            {
                var dsTB = (from i in db.ThongBaos
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
                return Json(new { code = 500, msg = "Loi" }, JsonRequestBehavior.AllowGet);
            }
        }

        //đọc thông báo + bình luận
        public JsonResult dsBLTB(string id)
        {
            try
            {
                var tb = (from t in db.ThongBaos
                          join gv in db.GiaoViens on t.MaGV equals gv.MaGV
                          join bl in db.BinhLuans on t.MaThongBao equals bl.MaThongBao
                          join ph in db.PhuHuynhs on bl.MaPH equals ph.MaPH
                          where t.MaThongBao == id
                          select new
                          {
                              TenGV = gv.TenGV,
                              TieuDe = t.TieuDe,
                              NoiDung = t.NoiDung,
                              NgayTB = t.NgayTB.ToString(),
                              MaNhan = t.MaNhan,

                              MaThongBao = t.MaThongBao,
                              TenPh = ph.TenPh,
                              NoiDungBL = bl.NoiDungBL,
                              MaPH = ph.MaPH

                          }).ToList();
                return Json(new { code = 200, dstb = tb }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "That bai!" }, JsonRequestBehavior.AllowGet);
            }
        }


        //lấy id thong báo
        public ActionResult DetailThongBao(string id)
        {

            ViewData["idTB"] = id;

            return View();
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
                return Json(new { code = 500, msg = "That bai!" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { code = 500, msg = "Tải bình luận lỗi" }, JsonRequestBehavior.AllowGet);
            }
        }

        //thêm bình luận 
        public JsonResult addBL(string NoiDungBL, string MaThongBao, DateTime ngayBL)
        {
            try
            {
                // thêm bình luận
                //  DateTime x =DateTime.Now;
                //   DateTime x = new DateTime();
                //  DateTime= x.Date();
                var bl = new BinhLuan();
                bl.MaThongBao = MaThongBao;
                bl.NoiDungBL = NoiDungBL;
                bl.NgayBL = ngayBL;
                db.BinhLuans.Add(bl);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lỗi" }, JsonRequestBehavior.AllowGet);
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
                return Json(new { code = 500, msg = "Xóa không thành công" }, JsonRequestBehavior.AllowGet);
            }

        }

        //lấy danh sách học sinh
        [HttpGet]
        public JsonResult loadDSHS(string idLop)
        {
            try
            {
                //string x = "6A1";
                var dsHS = (from hs in db.HocSinhs
                            join hs_lp in db.LH_HS on hs.MaHS equals hs_lp.MaHS
                            join lp in db.LopHocs on hs_lp.MaLopHoc equals lp.MaLopHoc
                            where lp.MaLopHoc == idLop
                            select new
                            {
                                MaHS = hs.MaHS,
                                TenHS = hs.TenHS
                            }).ToList();
                return Json(new { code = 200, dsHS = dsHS }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lỗi" }, JsonRequestBehavior.AllowGet);
            }
        }

        //thêm thông báo
        public JsonResult addTB(string MaThongBao, string TieuDe, string NoiDung, string MaNhan, DateTime NgayBL)
        {
            string maGV = "GVAD";
            ThongBao tb = db.ThongBaos.FirstOrDefault(x => x.MaThongBao == MaThongBao);
            if (tb != null)
            {
                return Json(new { code = 500, msg = "Trùng mã thông báo! Mời nhập lại!" }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                tb = new ThongBao();
                tb.MaThongBao = MaThongBao;
                tb.TieuDe = TieuDe;
                tb.NoiDung = NoiDung;
                tb.MaNhan = MaNhan;
                tb.MaGV = maGV;
                tb.NgayTB = NgayBL;

                db.ThongBaos.Add(tb);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm thành công!" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { code = 500, msg = "Thêm Thất Bại!" }, JsonRequestBehavior.AllowGet);
            }
        }

        //xóa thông báo
        public JsonResult deleteTB(string maTB)
        {
            try
            {

                var bl = db.BinhLuans.Where(x => x.MaThongBao == maTB);
                db.BinhLuans.RemoveRange(bl);

                ThongBao tb = db.ThongBaos.Find(maTB);
                db.ThongBaos.Remove(tb);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa không thành công " }, JsonRequestBehavior.AllowGet);
            }

        }

        // lấy danh sách học sinh
        public ActionResult DsHocSinh(string id)
        {
            ViewData["idLop"] = id;
            return View();

        }

        public JsonResult dsHS(string id, int page = 1, int pageSize = 3)
        {
            try
            {
                var dsHs = (from hs in db.HocSinhs
                            join lh_hs in db.LH_HS on hs.MaHS equals lh_hs.MaHS
                            join lop in db.LopHocs on lh_hs.MaLopHoc equals lop.MaLopHoc
                            where lop.MaLopHoc == id
                            select new
                            {
                                MaHS = hs.MaHS,
                                TenHS = hs.TenHS,
                                GioiTinh = hs.GioiTinh,
                                NgaySinh = hs.NgaySinh.ToString(),
                                DiaChi = hs.DiaChi,
                                SDT = hs.SDT
                            }).ToList();
                int totalRow = dsHs.Count();
                var ds = dsHs.Skip((page - 1) * pageSize).Take(pageSize);
                return Json(new { code = 200, dsHS = ds, total = totalRow }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Thất bại" }, JsonRequestBehavior.AllowGet);
            }

        }

        //Lấy điểm của học sinh theo mã học sinh
        public ActionResult diemHSHK1(string id)
        {

            ViewData["maHocSinh"] = id;
            return View();
        }
        //lay diem hoc ki 1
        public JsonResult DiemHK1(string id)
        {
            try
            {
                var diem1 = (from diem in db.Diems
                             join gv in db.GiaoViens on diem.MaGVPT equals gv.MaGV
                             join mh in db.MonHocs on gv.MaMH equals mh.MaMH
                             join hk in db.HocKis on diem.MaHK equals hk.MaHocKi
                             join hs in db.HocSinhs on diem.MaHS equals hs.MaHS
                             where hk.MaHocKi == "HK1" && hs.MaHS == id
                             orderby mh.Nhom descending
                             select new
                             {
                                 MaDiem = diem.MaDiem,
                                 DiemCK = diem.DiemCK,
                                 DiemTx1 = diem.DiemTx1,
                                 DiemTx2 = diem.DiemTx2,
                                 DiemGK = diem.DiemGK,
                                 DiemTx3 = diem.DiemTx3,
                                 DiemTx4 = diem.DiemTx4,
                                 TenGV = gv.TenGV,
                                 TenMH = mh.TenMH,
                                 Nhom = mh.Nhom,
                                 MaMH = mh.MaMH,
                                 MaGV = gv.MaGV

                             }).ToList();
                //foreach (Diem hs in di)
                //{
                //    ViewData["TenHS"] = hs.TenHS;
                //    ViewData["TenNH"] = hs.TenNH;
                //}

                return Json(new { code = 200, diem = diem1, msg = "lấy diểm thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "lấy điểm lỗi" }, JsonRequestBehavior.AllowGet);
            }

        }
        //Lấy điểm của học sinh theo mã học sinh
        public ActionResult diemHSHK2(string id)
        {

            ViewData["maHocSinh"] = id;
            return View();
        }

        //thêm điểm HS học kì 1
        public JsonResult addDiemHK1(string MaMH, string MaGV, int diemTx1, int diemTx2, int diemTx3, int diemTx4, int diemGK, int diemCK, string maHS)
        {

            try
            {
                //kiểm tra xem đã tồn tại chưa

                var d = db.Diems.FirstOrDefault(x => x.MaMH == MaMH && x.MaHK == "HK1" && x.MaHS == maHS);
                if (d != null)
                {
                    return Json(new { code = 500, msg = "Đã tồn tại điểm, mời nhập Lại" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    Diem diem = new Diem();
                    diem.DiemCK = diemCK;
                    diem.DiemGK = diemGK;
                    diem.MaGVPT = MaGV;
                    diem.DiemTx1 = diemTx1;
                    diem.DiemTx2 = diemTx2;
                    diem.DiemTx3 = diemTx3;
                    diem.DiemTx4 = diemTx4;
                    diem.MaHK = "HK1";
                    diem.MaHS = maHS;
                    diem.MaMH = MaMH;

                    db.Diems.Add(diem);
                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Thêm thành công" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Mời nhập lại " }, JsonRequestBehavior.AllowGet);
            }
        }

        //lấy danh sách giáo viên theo ID Môn Học
        public JsonResult dsGV_ID(string id)
        {
            try
            {
                var dsGV_ID = (from gv in db.GiaoViens
                               where gv.MaMH == id
                               select new
                               {
                                   maGV = gv.MaGV,
                                   tenGV = gv.TenGV
                               }).ToList();
                return Json(new { code = 200, dsGV_ID = dsGV_ID }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Lỗi " }, JsonRequestBehavior.AllowGet);
            }
        }

        //xóa điểm
        public JsonResult deleteDiem(int maTB)
        {
            try
            {
                Diem tb = db.Diems.Find(maTB);
                db.Diems.Remove(tb);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa không thành công" }, JsonRequestBehavior.AllowGet);
            }
        }

        //lay diem hoc ki 2
        public JsonResult DiemHK2(string id)
        {
            try
            {
                var diem1 = (from diem in db.Diems
                             join gv in db.GiaoViens on diem.MaGVPT equals gv.MaGV
                             join mh in db.MonHocs on gv.MaMH equals mh.MaMH
                             join hk in db.HocKis on diem.MaHK equals hk.MaHocKi
                             join hs in db.HocSinhs on diem.MaHS equals hs.MaHS
                             where hk.MaHocKi == "HK2" && hs.MaHS == id
                             orderby mh.Nhom descending
                             select new
                             {
                                 MaDiem = diem.MaDiem,
                                 DiemCK = diem.DiemCK,
                                 DiemTx1 = diem.DiemTx1,
                                 DiemTx2 = diem.DiemTx2,
                                 DiemGK = diem.DiemGK,
                                 DiemTx3 = diem.DiemTx3,
                                 DiemTx4 = diem.DiemTx4,
                                 TenGV = gv.TenGV,
                                 TenMH = mh.TenMH,
                                 Nhom = mh.Nhom,
                                 MaMH = mh.MaMH,
                                 MaGV = gv.MaGV

                             }).ToList();
                //foreach (Diem hs in di)
                //foreach (Diem hs in di)
                //{
                //    ViewData["TenHS"] = hs.TenHS;
                //    ViewData["TenNH"] = hs.TenNH;
                //}

                return Json(new { code = 200, diem = diem1, msg = "lấy diểm thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "lấy điểm lỗi" }, JsonRequestBehavior.AllowGet);
            }

        }

        //thêm điểm HS học kì 2
        public JsonResult addDiemHK2(string MaMH, string MaGV, int diemTx1, int diemTx2, int diemTx3, int diemTx4, int diemGK, int diemCK, string maHS)
        {

            try
            {
                //kiểm tra xem đã tồn tại chưa

                var d = db.Diems.FirstOrDefault(x => x.MaMH == MaMH && x.MaHK == "HK2" && x.MaHS == maHS);
                if (d != null)
                {
                    return Json(new { code = 500, msg = "Đã tồn tại điểm, Mời Nhập Lại" }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    Diem diem = new Diem();
                    diem.DiemCK = diemCK;
                    diem.DiemGK = diemGK;
                    diem.MaGVPT = MaGV;
                    diem.DiemTx1 = diemTx1;
                    diem.DiemTx2 = diemTx2;
                    diem.DiemTx3 = diemTx3;
                    diem.DiemTx4 = diemTx4;
                    diem.MaHK = "HK2";
                    diem.MaHS = maHS;
                    diem.MaMH = MaMH;

                    db.Diems.Add(diem);
                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Thêm thành công" }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Mời Nhập Lại " }, JsonRequestBehavior.AllowGet);
            }
        }

        //cuoi kì view
        public ActionResult diemCuoiKi(string id)
        {
            ViewData["maHocSinh"] = id;
            return View();
        }

        //thêm học sinh
        public JsonResult AddHS(string maHS, string tenHS, int SDT, string diaChi, string email, string gioiTinh, DateTime ngaySinh, string idLop)
        {
            //kiểm tra xem học sinh đã tồn tại chưa
            var hs1 = from hocsinh in db.HocSinhs where hocsinh.MaHS == maHS select hocsinh;
            int kt = hs1.Count();
            if (kt == 0)
            {
                try
                {
                    HocSinh hs = new HocSinh();
                    hs.MaHS = maHS;
                    hs.TenHS = tenHS;
                    hs.GioiTinh = gioiTinh;
                    hs.NgaySinh = ngaySinh;
                    hs.DiaChi = diaChi;
                    hs.Email = email;
                    hs.SDT = SDT;
                    db.HocSinhs.Add(hs);

                    LH_HS lp_hs = new LH_HS();
                    lp_hs.MaHS = maHS;
                    lp_hs.MaLopHoc = idLop;
                    db.LH_HS.Add(lp_hs);

                    db.SaveChanges();
                    return Json(new { code = 200, msg = "Thêm Thành Công" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { code = 500, msg = "Thêm thất bại " }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { code = 500, msg = "Học Sinh bị trùng, mời nhập lại" }, JsonRequestBehavior.AllowGet);
            }
        }

        //lấy thong tin học sinh theo id
        //lấy thông tin giáo viên theo id
        public JsonResult DetailHS(string id)
        {
            try
            {
                var detail = from hs in db.HocSinhs
                             where hs.MaHS == id
                             select new
                             {
                                 MaHS = hs.MaHS,
                                 TenHS = hs.TenHS,
                                 NgaySinh = hs.NgaySinh.ToString(),
                                 GioiTinh = hs.GioiTinh,
                                 DiaChi = hs.DiaChi,
                                 SDT = hs.SDT,
                                 email = hs.Email
                             };
                return Json(new { code = 200, detailGv = detail }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { code = 500, msg = "loi roi" }, JsonRequestBehavior.AllowGet);
            }

        }
        //sửa học sinh
        [HttpPost]
        public JsonResult EditHS(string maHS, string tenHS, int SDT, string diaChi, string email, string gioiTinh, DateTime ngaySinh)
        {
            var ds = from i in db.HocSinhs where i.MaHS == maHS select i;
            int kiemtra = ds.Count();
            //kiểm tra nếu chưa có trong database thì thêm mới
            if (kiemtra == 0)
            {
                try
                {
                    //gán giá trị cho giáo viên
                    var gv = new HocSinh();
                    gv.TenHS = tenHS;
                    gv.MaHS = maHS;
                    gv.SDT = SDT;
                    gv.DiaChi = diaChi;
                    gv.Email = email;

                    gv.NgaySinh = ngaySinh;
                    gv.GioiTinh = gioiTinh;

                    db.HocSinhs.Add(gv);
                    db.SaveChanges();//lưu vào db
                    return Json(new { code = 200, msg = "Cập nhật Thành Công" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { code = 500, msg = "Lỗi, Mời nhập lại" }, JsonRequestBehavior.AllowGet);
                }
            }
            //có trong db thì sửa db
            else
            {
                foreach (HocSinh gv in ds)
                {
                    gv.TenHS = tenHS;

                    gv.SDT = SDT;
                    gv.DiaChi = diaChi;
                    gv.Email = email;

                    gv.NgaySinh = ngaySinh;
                    gv.GioiTinh = gioiTinh;
                }
                db.SaveChanges();//lưu vào db
                return Json(new { code = 200, msg = "Cập nhật Thành Công" }, JsonRequestBehavior.AllowGet);

            }
        }
        //xóa học sinh
        [HttpPost]
        public JsonResult DelHS(string maHS, string maLop)
        {
            try
            {
                // xóa ràng buộc
                var lp = from lop in db.LH_HS where lop.MaHS == maHS && lop.MaLopHoc == maLop select lop;
                foreach (LH_HS lop in lp)
                {
                    db.LH_HS.Remove(lop);
                }
                HocSinh hs = db.HocSinhs.Find(maHS);
                db.HocSinhs.Remove(hs);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa thành công" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa không thành công" }, JsonRequestBehavior.AllowGet);
            }
        }
        //view quản lý tài khoản
        public ActionResult TaiKhoan()
        {
            return View();
        }
        //lấy danh sách tài khoản
        //public JsonResult DsTaiKhoan()
        //{
        //    var dsTaiKhoan = from taikhoan in db.TaiKhoans select taikhoan;
        //    return Json(new { code = 200,dsTaiKhoan = dsTaiKhoan }, JsonRequestBehavior.AllowGet);
        //}
        //thêm tài khoản

        public JsonResult AddTaiKhoan(string tenTK,string matKhau)
        {
            Console.WriteLine("va");
            //kiểm tra tên tài khoản đã có chưa
            var tk = from taikhoan in db.TaiKhoans where taikhoan.TenTk == tenTK select taikhoan;
            if (tk.Count() == 0)
            {
                TaiKhoan taikhoan = new TaiKhoan();
                taikhoan.TenTk = tenTK;
                taikhoan.MatKhau = matKhau;
                db.TaiKhoans.Add(taikhoan);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Thêm thành công!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 500, msg = "Tên tài khoản đã tồn tại!" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}