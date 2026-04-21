using IndustrialEnergyManagementSystem.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace IndustrialEnergyManagementSystem.Controllers
{
    public class EnergyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private bool IsLoggedIn()
        {
            return Session["UserId"] != null;
        }

        // GET: Energy
        public ActionResult Index(string searchMachine, DateTime? startDate, DateTime? endDate)
        {
         
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            var logsQuery = db.EnergyRecords.Include(e => e.Machine).AsQueryable();

            if (!string.IsNullOrEmpty(searchMachine))
                logsQuery = logsQuery.Where(l => l.Machine.MachineName.Contains(searchMachine));

            if (startDate.HasValue)
                logsQuery = logsQuery.Where(l => l.RecordDate >= startDate.Value);

            if (endDate.HasValue)
                logsQuery = logsQuery.Where(l => l.RecordDate <= endDate.Value);

            var logsList = logsQuery
                           .OrderByDescending(l => l.RecordDate)
                           .ToList();

            var logs = logsList.Select(l => new EnergyLogViewModel
            {
                RecordId = l.RecordId,
                MachineName = l.Machine?.MachineName ?? "No Machine",
                RunHours = l.RunHours,
                EnergyConsumed = l.Machine != null ? l.CalculateEnergy(l.Machine.PowerRatingKW) : 0,
                RecordDate = l.RecordDate
            }).ToList();

            return View(logs);
        }

        // GET: Energy/Create
        public ActionResult Create()
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            ViewBag.MachineId = new SelectList(db.Machines, "MachineId", "MachineName");
            return View();
        }

        // POST: Energy/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordId,MachineId,RunHours,RecordDate")] EnergyRecord log)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                db.EnergyRecords.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MachineId = new SelectList(db.Machines, "MachineId", "MachineName", log.MachineId);
            return View(log);
        }

        // GET: Energy/Delete
        public ActionResult Delete(int? id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var log = db.EnergyRecords
                        .Include(e => e.Machine)
                        .FirstOrDefault(e => e.RecordId == id);

            if (log == null)
                return HttpNotFound();

            var viewModel = new EnergyLogViewModel
            {
                RecordId = log.RecordId,
                MachineName = log.Machine?.MachineName ?? "No Machine",
                RunHours = log.RunHours,
                EnergyConsumed = log.Machine != null ? log.CalculateEnergy(log.Machine.PowerRatingKW) : 0,
                RecordDate = log.RecordDate
            };

            return View(viewModel);
        }

        // POST: Energy/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsLoggedIn())
                return RedirectToAction("Login", "Account");

            EnergyRecord log = db.EnergyRecords.Find(id);

            if (log != null)
            {
                db.EnergyRecords.Remove(log);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Dispose
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