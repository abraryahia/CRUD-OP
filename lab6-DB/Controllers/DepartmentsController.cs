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
    public class DepartmentsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Departments
        [Auth]
        public ActionResult Index()
        {
            return View(db.Departments.ToList());
        }

        // GET: Departments/Details/5
        [Auth]

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        [Auth]

        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeptId,DeptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(department);
        }

       
        [Auth]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeptId,DeptName")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Departments.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = db.Departments.Find(id);
            db.Departments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

     
        public ActionResult addCourses(int id)
        {
            var allCurs = db.Courses.ToList();
            var CursInDept = db.CrsDept.Where(m => m.DeptId == id).Select(a => a.Course).ToList();
            var CursNotInDept = allCurs.Except(CursInDept).ToList();
            ViewBag.dept = db.Departments.FirstOrDefault(addCourses => addCourses.DeptId == id);
            return View(CursNotInDept);
        }
        [HttpPost]
        public ActionResult addCourses(int id , Dictionary<string,bool> Crs)
        {
            foreach(var item in Crs)
            {
                if(item.Value == true)
                {
                    db.CrsDept.Add(new CourseDepartment() { DeptId = id, CrsId = int.Parse(item.Key) });
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult removeCourses(int id)
        {
            var CursInDept = db.CrsDept.Where(m => m.DeptId == id).Select(a => a.Course).ToList();
            ViewBag.dept = db.Departments.FirstOrDefault(removeCourses => removeCourses.DeptId == id);
            return View(CursInDept);
        }
        [HttpPost]
        public ActionResult removeCourses(int id, Dictionary<string, bool> Crs)
        {
            foreach (var item in Crs)
            {
                if (item.Value == true)
                {
                    int crsid = int.Parse(item.Key);
                    var deptCourse = db.CrsDept.FirstOrDefault(a => a.DeptId == id && a.CrsId == crsid);
                    db.CrsDept.Remove(deptCourse);
                }
                
            }
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
