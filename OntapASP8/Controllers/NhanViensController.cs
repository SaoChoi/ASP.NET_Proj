﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OntapASP8.Models;

namespace OntapASP8.Controllers
{
    public class NhanViensController : Controller
    {
        private QLNV db = new QLNV();

        // GET: NhanViens
        public ActionResult Index()
        {
            var nhanViens = db.NhanViens.Include(n => n.Phong);
            return View(nhanViens.ToList());
        }

        [ChildActionOnly]
        public PartialViewResult CategoryMenu()
        {
            var li = db.Phongs.ToList();
            return PartialView(li);
        }

        [Route("NhanVienTheoPhong/{Maphong}")]
        public ActionResult NVTheoPhong(int Maphong)
        {
            var li = db.NhanViens.Where(u => u.Maphong == Maphong).ToList();
            return View(li);
        }

        // GET: NhanViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public ActionResult Create()
        {
            ViewBag.Maphong = new SelectList(db.Phongs, "Maphong", "Tenphong");
            if (Session["IsLogin"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(NhanVien nv)
        {
                try
                {
                    db.NhanViens.Add(nv);
                    db.SaveChanges();
                    return Json(new { result = true, JsonRequestBehavior.AllowGet });
                }
                catch (Exception ex)
                {
                    return Json(new { result = false, error = ex.Message });
                }
        }

        // GET: NhanViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["IsLogin"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                NhanVien nhanVien = db.NhanViens.Find(id);
                if (nhanVien == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Maphong = new SelectList(db.Phongs, "Maphong", "Tenphong", nhanVien.Maphong);
                return View(nhanVien);
            }
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Manv,Hoten,Tuoi,Diachi,Luong,Maphong")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Maphong = new SelectList(db.Phongs, "Maphong", "Tenphong", nhanVien.Maphong);
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["IsLogin"] == null) {
                return RedirectToAction("Login", "Login");
            }
            else { 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
            }
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            db.NhanViens.Remove(nhanVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
