using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
using IndustrialEnergyManagementSystem.Models;

namespace IndustrialEnergyManagementSystem.Controllers
{
    public class MachineController : Controller
    {
        private IEMSContext db = new IEMSContext();

        // 🔐 AUTH CHECK (LOGIN REQUIRED)
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserId"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "Account");
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        // =========================
        // GET: Machine (NO CACHE FIXED)
        // =========================
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            var machines = db.Machines
                             .Include(m => m.Department)
                             .ToList();

            return View(machines);
        }

        // GET: Machine/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var machine = db.Machines
                            .Include(m => m.Department)
                            .FirstOrDefault(m => m.MachineId == id);

            if (machine == null)
                return HttpNotFound();

            return View(machine);
        }

        // GET: Machine/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId =
                new SelectList(db.Departments, "DepartmentId", "DepartmentName");

            return View();
        }

        // POST: Machine/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Machine machine)
        {
            if (ModelState.IsValid)
            {
                db.Machines.Add(machine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId =
                new SelectList(db.Departments, "DepartmentId", "DepartmentName", machine.DepartmentId);

            return View(machine);
        }

        // GET: Machine/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var machine = db.Machines.Find(id);

            if (machine == null)
                return HttpNotFound();

            ViewBag.DepartmentId =
                new SelectList(db.Departments, "DepartmentId", "DepartmentName", machine.DepartmentId);

            return View(machine);
        }

        // POST: Machine/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Machine machine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(machine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId =
                new SelectList(db.Departments, "DepartmentId", "DepartmentName", machine.DepartmentId);

            return View(machine);
        }

        // GET: Machine/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var machine = db.Machines
                            .Include(m => m.Department)
                            .FirstOrDefault(m => m.MachineId == id);

            if (machine == null)
                return HttpNotFound();

            return View(machine);
        }

        // POST: Machine/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var machine = db.Machines.Find(id);

            if (machine != null)
            {
                db.Machines.Remove(machine);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // DISPOSE
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