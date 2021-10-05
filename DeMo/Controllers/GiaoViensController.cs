using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeMo.Controllers
{
    public class GiaoViensController : Controller
    {
        // GET: GiaoViens
        public ActionResult Index()
        {
            return View();
        }

        // GET: GiaoViens/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GiaoViens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GiaoViens/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GiaoViens/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GiaoViens/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: GiaoViens/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GiaoViens/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
