using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ABCUniversity.DAL;
using ABCUniversity.Models;

namespace ABCUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private VarsityContext db = new VarsityContext();

        // GET: Students
        public ActionResult Index(string sortOrder, string SearchStr)
        {
            ViewBag.SortLastName = sortOrder != "lastName_desc" ? "lastName_desc" : "";
            ViewBag.SortEnrollmentDate = sortOrder == "date" ? "date_desc" : "date";
            var students = db.Students.AsQueryable();

            if (!String.IsNullOrEmpty(SearchStr))
            {
                students = students.Where(s => s.FirstMidName.ToUpper().Contains(SearchStr.ToUpper()) ||  s.LastName.ToUpper().Contains(SearchStr.ToUpper()));
            }
                                    
            switch (sortOrder)
            {
                case "date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                case "lastName_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("abcError", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }


            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMsg = "Delete failed. Try again, and if the"
                                    + "problem persists see your system administrator.";
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Student student = db.Students.Find(id);
        //    db.Students.Remove(student);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {            
            try
            {
                //Student studentObj = db.Students.Find(id);
                //db.Students.Remove(studentObj);

                Student studentToDelete = new Student { ID = (int)id };
                db.Entry(studentToDelete).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                RedirectToAction("Delete", new { ID = id, saveChangesError = true});
            }
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
