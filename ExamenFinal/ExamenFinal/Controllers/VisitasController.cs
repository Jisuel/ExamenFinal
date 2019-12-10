using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamenFinal.Models;

namespace ExamenFinal.Controllers
{
    public class VisitasController : Controller
    {
        private ExamenWEBEntities db = new ExamenWEBEntities();

        // GET: Visitas
        public ActionResult Index()
        {

            var visita = db.Visita.Include(v => v.Contactos);

            return View(visita.ToList());
        }

        // GET: Visitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        // GET: Visitas/Create
        public ActionResult Create()
        {
            var consulta = from x in db.Contactos.AsEnumerable() select (new { ID = x.ID, Nombre = string.Format("{0} {1}", x.Nombre, x.Apellido) });

            ViewBag.Contacto = new SelectList(consulta, "ID", "Nombre");
            return View();
        }

        // POST: Visitas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Motivo,Fecha_Visita,Hora_Entrada,Hora_Salida,Nombre,Contacto")] Visita visita)
        {
            var consulta = from x in db.Contactos.AsEnumerable() select (new { ID = x.ID, Nombre = string.Format("{0} {1}", x.Nombre, x.Apellido) });

            if (ModelState.IsValid)
            {
                db.Visita.Add(visita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Contacto = new SelectList(consulta, "ID", "Nombre", visita.Contacto);
            return View(visita);
        }

        // GET: Visitas/Edit/5
        public ActionResult Edit(int? id)
        {
            var consulta = from x in db.Contactos.AsEnumerable() select (new { ID = x.ID, Nombre = string.Format("{0} {1}", x.Nombre, x.Apellido) });

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            ViewBag.Contacto = new SelectList(consulta, "ID", "Nombre", visita.Contacto);
            return View(visita);
        }

        // POST: Visitas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Motivo,Fecha_Visita,Hora_Entrada,Hora_Salida,Nombre,Contacto")] Visita visita)
        {
            var consulta = from x in db.Contactos.AsEnumerable() select (new { ID = x.ID, Nombre = string.Format("{0} {1}", x.Nombre, x.Apellido) });

            if (ModelState.IsValid)
            {
                db.Entry(visita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Contacto = new SelectList(consulta, "ID", "Nombre", visita.Contacto);
            return View(visita);
        }

        // GET: Visitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visita visita = db.Visita.Find(id);
            if (visita == null)
            {
                return HttpNotFound();
            }
            return View(visita);
        }

        // POST: Visitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visita visita = db.Visita.Find(id);
            db.Visita.Remove(visita);
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
