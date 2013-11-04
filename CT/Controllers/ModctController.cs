using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CT.Models;

namespace CT.Controllers
{
    public class ModctController : Controller
    {
        private CTDBContext db = new CTDBContext();

        // GET: /Modct/
        public async Task<ActionResult> Index()
        {
            return View(await db.Modct.ToListAsync());
        }

        // GET: /Modct/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modct modct = await db.Modct.FindAsync(id);
            if (modct == null)
            {
                return HttpNotFound();
            }
            return View(modct);
        }

        // GET: /Modct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Modct/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name")] Modct modct)
        {
            if (ModelState.IsValid)
            {
                db.Modct.Add(modct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(modct);
        }

        // GET: /Modct/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modct modct = await db.Modct.FindAsync(id);
            if (modct == null)
            {
                return HttpNotFound();
            }
            return View(modct);
        }

        // POST: /Modct/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name")] Modct modct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(modct);
        }

        // GET: /Modct/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modct modct = await db.Modct.FindAsync(id);
            if (modct == null)
            {
                return HttpNotFound();
            }
            return View(modct);
        }

        // POST: /Modct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Modct modct = await db.Modct.FindAsync(id);
            db.Modct.Remove(modct);
            await db.SaveChangesAsync();
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
