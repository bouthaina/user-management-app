using Microsoft.AspNetCore.Mvc;
using UserApp.Models;

namespace UserApp.Controllers
{
    public class UserController : Controller
    {

        AppDbContext dbcontext = new AppDbContext();

        public IActionResult Index(string SearchByName)
        {
            var users = GetUsers();

            if (!string.IsNullOrEmpty(SearchByName))
            {
                users = users.Where(u => u.UserName.ToLower().Contains(SearchByName.ToLower())).ToList();
            }

            return View(users);
        }

        private List<User> GetUsers()
        {
            var users = (from u in dbcontext.Users
                         join department in dbcontext.Departments on u.Departmentid equals department.DepartmentId
                         select new User
                         {
                             UserId = u.UserId,
                             UserNumber = u.UserNumber,
                             UserName = u.UserName,
                             BirthDate = u.BirthDate,
                             GrossSalary = u.GrossSalary,
                             NetSalary = u.NetSalary,
                             Departmentid = u.Departmentid,
                             DepartmentName = department.DepartmentName
                         }).ToList();
            return users;
        }


        public IActionResult Create()
        {
            ViewBag.Departments = this.dbcontext.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(User model)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if (ModelState.IsValid)
            {
                dbcontext.Users.Add(model);
                dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = this.dbcontext.Departments.ToList();

            return View();
        }
        public IActionResult Edit(int ID)
        {
            User data = this.dbcontext.Users.Where(u => u.UserId == ID).FirstOrDefault();
            ViewBag.Departments = this.dbcontext.Departments.ToList();
            return View("Create", data);
        }
        [HttpPost]
        public IActionResult Edit(User model)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");
            if (ModelState.IsValid)
            {
                dbcontext.Users.Update(model);
                dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = this.dbcontext.Departments.ToList();

            return View("Create", model);
        }

        public IActionResult Delete(int ID)
        {
            User data = this.dbcontext.Users.Where(u => u.UserId == ID).FirstOrDefault();
            if (data != null)
            {
                dbcontext.Users.Remove(data);
                dbcontext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}


