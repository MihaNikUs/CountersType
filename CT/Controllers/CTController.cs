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
using System.Web.Helpers;
using System.Web.Hosting;
using System.IO;

namespace CT.Controllers
{
    public class CTController : Controller
    {
        private CTDBContext db = new CTDBContext();

        // GET: /CT/
        public async Task<ActionResult> Index()
        {
            var counter_types = db.Counter_Types.Include(c => c.Fase).Include(c => c.Modct).Include(c => c.Types_On);
            return View(await counter_types.ToListAsync());
        }

        // GET: /CT/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Counter_Types counter_types = await db.Counter_Types.FindAsync(id);
            if (counter_types == null)
            {
                return HttpNotFound();
            }
            return View(counter_types);
        }

        // GET: /CT/Create
        public ActionResult Create()
        {
            ViewBag.FaseId = new SelectList(db.Fase, "Id", "Name");
            ViewBag.ModctId = new SelectList(db.Modct, "Id", "Name");
            ViewBag.Types_OnId = new SelectList(db.Types_On, "Id", "Name");
            return View();
        }

        // POST: /CT/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,CT_NAME,CT_PERIOD,CT_LENGTH,CT_PREC,CT_ELEMENTS,CT_VOLTAGE,CT_CURRENT,ModctId,FaseId,Types_OnId")] Counter_Types counter_types)
        {
            if (ModelState.IsValid)
            {
                db.Counter_Types.Add(counter_types);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FaseId = new SelectList(db.Fase, "Id", "Name", counter_types.FaseId);
            ViewBag.ModctId = new SelectList(db.Modct, "Id", "Name", counter_types.ModctId);
            ViewBag.Types_OnId = new SelectList(db.Types_On, "Id", "Name", counter_types.Types_OnId);
            return View(counter_types);
        }

        // GET: /CT/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Counter_Types counter_types = await db.Counter_Types.FindAsync(id);
            if (counter_types == null)
            {
                return HttpNotFound();
            }
            ViewBag.FaseId = new SelectList(db.Fase, "Id", "Name", counter_types.FaseId);
            ViewBag.ModctId = new SelectList(db.Modct, "Id", "Name", counter_types.ModctId);
            ViewBag.Types_OnId = new SelectList(db.Types_On, "Id", "Name", counter_types.Types_OnId);
            return View(counter_types);
        }

        // POST: /CT/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include="Id,CT_NAME,CT_PERIOD,CT_LENGTH,CT_PREC,CT_ELEMENTS,CT_VOLTAGE,CT_CURRENT,ModctId,FaseId,Types_OnId")] Counter_Types counter_types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(counter_types).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FaseId = new SelectList(db.Fase, "Id", "Name", counter_types.FaseId);
            ViewBag.ModctId = new SelectList(db.Modct, "Id", "Name", counter_types.ModctId);
            ViewBag.Types_OnId = new SelectList(db.Types_On, "Id", "Name", counter_types.Types_OnId);
            return View(counter_types);
        }

        // GET: /CT/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Counter_Types counter_types = await db.Counter_Types.FindAsync(id);
            if (counter_types == null)
            {
                return HttpNotFound();
            }
            return View(counter_types);
        }

        // POST: /CT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Counter_Types counter_types = await db.Counter_Types.FindAsync(id);
            db.Counter_Types.Remove(counter_types);
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


        public void GetPhotoThumbnail(int realtyId, int width, int height)
        {
            // Loading photos’ info from database for specific Realty...
            string FotoPath  = HostingEnvironment.MapPath(@"~/Content/FotoCT/");
            string photo = "";
            if (System.IO.File.Exists(FotoPath + realtyId.ToString() + ".jpg"))
            {
                photo = FotoPath + realtyId.ToString() + ".jpg";
                new WebImage(photo)
                   .Resize(width, height, false, true) // Resizing the image to 100x100 px on the fly...
                   .Crop(1, 1) // Cropping it to remove 1px border at top and left sides (bug in WebImage)
                   .Write();
            }

            // Loading a default photo for realties that don't have a Photo
            new WebImage(HostingEnvironment.MapPath(@"~/Content/FotoCT/no-photo100x100.jpg")).Write();
        }



        #region Класс для Фото

        private string StorageRoot
        {
            get { return Path.Combine(Server.MapPath("~/Content/FotoCT")); }
        }

                //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpGet]
        public void Delete(string id)
        {
            var filename = id;
            var filePath = Path.Combine(Server.MapPath("~/Content/FotoCT"), filename);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpGet]
        public void Download(string id)
        {
            var filename = id;
            var filePath = Path.Combine(Server.MapPath("~/Content/FotoCT"), filename);

            var context = HttpContext;

            if (System.IO.File.Exists(filePath))
            {
                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
                context.Response.ContentType = "application/octet-stream";
                context.Response.ClearContent();
                context.Response.WriteFile(filePath);
            }
            else
                context.Response.StatusCode = 404;
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpPost]
        public ActionResult UploadFiles()
        {
            var r = new List<ViewDataUploadFilesResult>();

            foreach (string file in Request.Files)
            {
                var statuses = new List<ViewDataUploadFilesResult>();
                var headers = Request.Headers;

                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {
                    UploadWholeFile(Request, statuses);
                }
                else
                {
                    UploadPartialFile(headers["X-File-Name"], Request, statuses);
                }

                JsonResult result = Json(statuses);
                result.ContentType = "text/plain";

                return result;
            }

            return Json(r);
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadPartialFile(string fileName, HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;

            var fullName = Path.Combine(StorageRoot, Path.GetFileName(fileName));

            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            statuses.Add(new ViewDataUploadFilesResult()
            {
                name = fileName,
                size = file.ContentLength,
                type = file.ContentType,
                url = "/Home/Download/" + fileName,
                delete_url = "/Home/Delete/" + fileName,
                thumbnail_url = @"data:image/png;base64," + EncodeFile(fullName),
                delete_type = "GET",
            });
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadWholeFile(HttpRequestBase request, List<ViewDataUploadFilesResult> statuses)
        {
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];

                var fullPath = Path.Combine(StorageRoot, Path.GetFileName(file.FileName));

                file.SaveAs(fullPath);

                statuses.Add(new ViewDataUploadFilesResult()
                {
                    name = file.FileName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    url = "/Home/Download/" + file.FileName,
                    delete_url = "/Home/Delete/" + file.FileName,
                    thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath),
                    delete_type = "GET",
                });
            }
        }
    }

    public class ViewDataUploadFilesResult
    {
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string delete_url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_type { get; set; }
    }
        #endregion

}
