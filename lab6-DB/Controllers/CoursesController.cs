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
    public class CoursesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Courses
        [Auth]

        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        [Auth]

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        [Auth]

        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CrsId,CrsName")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        [Auth]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CrsId,CrsName")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Auth]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult addDept(int id)
        {
            var allDepts = db.Departments.ToList();
            var DeptInCrs = db.CrsDept.Where(m => m.CrsId == id).Select(a => a.Department).ToList();
            var DeptNotInCrs = allDepts.Except(DeptInCrs).ToList();
            ViewBag.crs = db.Courses.FirstOrDefault(addDept => addDept.CrsId == id);
            return View(DeptNotInCrs);
        }
        [HttpPost]
        public ActionResult addDept(int id, Dictionary<string, bool> dept)
        {
            foreach (var item in dept)
            {
                if (item.Value == true)
                {
                    db.CrsDept.Add(new CourseDepartment() { DeptId = int.Parse(item.Key), CrsId = id  });
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult removeDept(int id)
        {
            var DeptInCrs = db.CrsDept.Where(m => m.CrsId == id).Select(a => a.Department).ToList();
            ViewBag.crs = db.Courses.FirstOrDefault(addDept => addDept.CrsId == id);
            return View(DeptInCrs);
        }
        [HttpPost]
        public ActionResult removeDept(int id, Dictionary<string, bool> dept)
        {
            foreach (var item in dept)
            {
                if(item.Value == true)
                {
                    int deptid = int.Parse(item.Key);
                    var deptCourse = db.CrsDept.FirstOrDefault(a => a.DeptId == deptid && a.CrsId == id);
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
        public ActionResult addStudents(int id, int deptid)
        {
            var allStds = db.Students.Where(d => d.DeptId == deptid).ToList();
            var crsStds = db.StdCrs.Where(d => d.CrsId == id).Select(d => d.Student);
            var addstds = allStds.Except(crsStds).ToList();
            ViewBag.stdName = db.Courses.FirstOrDefault(d => d.CrsId == id);
            return View(addstds);
        }
        [HttpPost]

        public ActionResult addStudents(int id, Dictionary<string, bool> stds, int degree)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in stds)
                {
                    if (item.Value == true)
                    {
                        db.StdCrs.Add(new StudentCourse { Id = int.Parse(item.Key), CrsId = id, StuDegree = degree });
                    }

                }
                db.SaveChanges();
                return RedirectToAction("Details/" + id, "Courses");
            }
            return View();
        }
    }
}
