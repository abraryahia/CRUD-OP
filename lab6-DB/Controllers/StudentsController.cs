using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lab6_DB.cstFilter;
using lab6_DB.Models;

namespace lab6_DB.Controllers
{
    public class StudentsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Students
        [Auth]
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.department);
            return View(students.ToList());
        }

       
        [Auth]

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student std = db.Students.Find(id);
            if (std == null)
            {
                return HttpNotFound();
            }
            return View(std);
        }

       
        [Auth]

        public ActionResult Create()
        {
            ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "DeptName");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student std , HttpPostedFileBase stdImg)
        {
            
            if (stdImg != null)
            {
                string imgFile = std.Id.ToString() + "." + stdImg.FileName.Split('.')[1];
                stdImg.SaveAs(Server.MapPath("~/images/" + imgFile));
                std.photo = imgFile;
            }
            if (ModelState.IsValid)
            {
                db.Students.Add(std);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "DeptName", std.DeptId);
            return View(std);
        }

       
        [Auth]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student std = db.Students.Find(id);
            if (std == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "DeptName", std.DeptId);
            return View(std);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Student std ,HttpPostedFileBase stdImg)
        {
            string filename;
            if (stdImg != null)
            {
                filename = std.Id + "." + stdImg.FileName.Split('.')[1];
                stdImg.SaveAs(Server.MapPath("~/images/") + filename);
                std.photo = filename;
            }
            if (ModelState.IsValid)
            {
                db.Entry(std).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "DeptName", std.DeptId);
            return View(std);
        }

       
        [Auth]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student std = db.Students.Find(id);
            if (std == null)
            {
                return HttpNotFound();
            }
            return View(std);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student std = db.Students.Find(id);
            db.Students.Remove(std);
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

            public ActionResult Login()
            {
                return View();
            }

            [HttpPost]
           public ActionResult Login(Login User)
            {
                if (ModelState.IsValid)
                {
                    
                        var log = db.Students.FirstOrDefault(m=>m.UserName == User.UserName && m.Password == User.Password);
                        if (log != null)
                        {
                           Session["UserName"] = User.UserName;
                            return RedirectToAction("Index");
                        }
                        
                }
                return View(User);
            }
        public ActionResult Register()
        {
            ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "DeptName");
            return View();
        }

        [HttpPost]
        public ActionResult Register(Student std, HttpPostedFileBase stdImg)
        {
            if (stdImg != null)
            {
                string imgFile = std.Id.ToString() + "." + stdImg.FileName.Split('.')[1];
                stdImg.SaveAs(Server.MapPath("~/images/" + imgFile));
                std.photo = imgFile;
            }
            if (ModelState.IsValid)
            {

                var reg = db.Students.FirstOrDefault(m => m.UserName == std.UserName);

                if (reg == null)
                {
                    db.Students.Add(std);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }

            }
            ViewBag.DeptId = new SelectList(db.Departments, "DeptId", "DeptName", std.DeptId);

            return View(std);
        }
        [Auth]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult addDegree(int id)
        {
            //var Std= db.Students.Find(id);
            //var stdDept = Std.DeptId;
            //var CursInDept = db.CrsDept.Where(m => m.DeptId == id).Select(a => a.Course).ToList();
            var allCrs = db.Courses.ToList();
            var courseWithDegree = db.StdCrs.Where(a => a.Id == id && (a.StuDegree != null)).Select(m => m.Course);
            var courseWithNoDegree = allCrs.Except(courseWithDegree).ToList();
            return View(courseWithNoDegree);
    }
    [HttpPost]
    public ActionResult addDegree(int id, Dictionary<string, int> deg)
    {
        foreach (var item in deg)
        {
            if (item.Value > 0)
            {
                db.StdCrs.Add(new StudentCourse() { Id = id, CrsId = int.Parse(item.Key), StuDegree = item.Value });
            }
        }
        db.SaveChanges();
        return RedirectToAction("index");
    }
   
    }
}
