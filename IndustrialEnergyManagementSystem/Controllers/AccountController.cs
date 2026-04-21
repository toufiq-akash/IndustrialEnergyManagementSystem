using System.Linq;
using System.Web.Mvc;
using IndustrialEnergyManagementSystem.Models;

namespace IndustrialEnergyManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private IEMSContext db = new IEMSContext();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            System.Diagnostics.Debug.WriteLine("LOGIN HIT");
            System.Diagnostics.Debug.WriteLine("USERNAME: " + username);
            System.Diagnostics.Debug.WriteLine("PASSWORD: " + password);

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Username and password required";
                return View();
            }

            var user = db.Users
                .FirstOrDefault(u =>
                    u.Username.Trim() == username.Trim() &&
                    u.Password.Trim() == password.Trim());

            System.Diagnostics.Debug.WriteLine("USER FOUND: " + (user != null));

            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["Username"] = user.Username;

                return RedirectToAction("Index", "Machine");
            }

            ViewBag.Error = "Invalid login";
            return View();
        }

        // Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}