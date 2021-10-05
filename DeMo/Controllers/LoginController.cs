using DeMo.Models.model;
using System.Web.Mvc;
using DeMo.Models;

namespace DeMo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAction(TaiKhoan taiKhoan)
        {
            LoginDAO login = new LoginDAO();
            bool check = login.Login(taiKhoan.TenTk, taiKhoan.MatKhau);
            if (check)
            {
                Session["MaGV"] = null;
                Session["UserName"] = taiKhoan.TenTk;
                int role = int.Parse(login.Role(taiKhoan.TenTk));

                if (role == 1)
                {
                    return RedirectToAction("Index", "AD");
                }
                else if (role == 2)
                {
                    Session["MaPH"] = login.Ma(taiKhoan.TenTk);
                    string maph = login.Ma(taiKhoan.TenTk);
                    Session["MaHS"] = login.MaHS(maph);
                  
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    Session["MaGV"] = login.Ma(taiKhoan.TenTk);
                    return RedirectToAction("Index", "GiaoViens");
                }

            }
            else return RedirectToAction("Index", "Login");
        }

    }
}