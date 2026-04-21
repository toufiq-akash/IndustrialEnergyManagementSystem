using IndustrialEnergyManagementSystem.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace IndustrialEnergyManagementSystem.Controllers
{
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchMachine, DateTime? startDate, DateTime? endDate)
        {
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

            var reportLogs = logsList.Select(l => new EnergyLogViewModel
            {
                RecordId = l.RecordId,
                MachineName = l.Machine?.MachineName ?? "No Machine",
                RunHours = l.RunHours,
                EnergyConsumed = l.Machine != null ? l.CalculateEnergy(l.Machine.PowerRatingKW) : 0,
                RecordDate = l.RecordDate
            }).ToList();

            return View(reportLogs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}