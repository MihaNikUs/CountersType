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
    public class TOController : Controller
    {
        private CTDBContext db = new CTDBContext();

        // GET: /TO/
        public async Task<ActionResult> Index()
        {
            return View(await db.Types_On.ToListAsync());
        }

        // GET: /TO/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Types_On types_on = await db.Types_On.FindAsync(id);
            if (types_on == null)
            {
                return HttpNotFound();
            }
            return View(types_on);
        }

        // GET: /TO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TO/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,ShortName")] Types_On types_on)
        {
            if (ModelState.IsValid)
            {
                db.Types_On.Add(types_on);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(types_on);
        }

        // GET: /TO/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Types_On types_on = await db.Types_On.FindAsync(id);
            if (types_on == null)
            {
                return HttpNotFound();
            }
            return View(types_on);
        }

        // POST: /TO/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,Name,ShortName")] Types_On types_on)
        {
            if (ModelState.IsValid)
            {
                db.Entry(types_on).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(types_on);
        }

        // GET: /TO/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Types_On types_on = await db.Types_On.FindAsync(id);
            if (types_on == null)
            {
                return HttpNotFound();
            }
            return View(types_on);
        }

        // POST: /TO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Types_On types_on = await db.Types_On.FindAsync(id);
            db.Types_On.Remove(types_on);
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
