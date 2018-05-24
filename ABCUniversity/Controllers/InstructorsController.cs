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
using ABCUniversity.ViewModels;
using System.Data.Entity.Infrastructure;

namespace ABCUniversity.Controllers
{
    public class InstructorsController : Controller
    {
        private VarsityContext db = new VarsityContext();

        // GET: Instructors
        //public ActionResult Index()
        //{
        //    var instructors = db.Instructors.Include(i => i.OfficeAssignment);
        //    return View(instructors.ToList());
        //}

        //GET :
        public ActionResult Index(int? instructorsID, int? courseID)
        {
            var viewModel = new InstructorIndexData();

            //viewModel.CourseInstructors = db.CourseInstructors
            //                                .Include(c => c.Courses)
            //                                .Include(c => c.Instructors.OfficeAssignment)
            //                                .OrderBy(c => c.Instructors.FirstMidName).ToList();


            viewModel.Instructors = db.Instructors
                                    .Include(instructor => instructor.OfficeAssignment)
                                    .Include(instructor => instructor.CourseInstructors.Select(courseInstructor => courseInstructor.Courses.Enrollments.Select(course => course.Students )))
                                    .OrderBy(instructor => instructor.FirstMidName).ToList();

            if (instructorsID != null)
            {
                ViewBag.InstructorID = instructorsID.Value;
                //ViewModel.CourseInstructors = ViewModel.Instructors.Select(c => c.CourseInstructors.Select(q => q.CourseID)).Where(c => c.InstructorID == instructorsID.Value)
                //                                  .Single();
                viewModel.CourseInstructors = viewModel.Instructors.Where(i => i.InstructorID == instructorsID.Value).Single().CourseInstructors;
                
            }

            if (courseID != null)
            {
                ViewBag.CourseID = courseID.Value;
                viewModel.Courses = viewModel.Instructors.Select(i => i.CourseInstructors.Select(ci => ci.Courses).Where(c => c.CourseID == courseID.Value)).Single();
            }

            return View(viewModel);
        }    

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            //ViewBag.OfficeAssignment = new SelectList(db.OfficeAssignments, "InstructorID", "Location");
            return View();
        }

        // POST: Instructors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstructorID,LastName,FirstMidName,HireDate")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.OfficeAssignment = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.InstructorID);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Instructor instructor = db.Instructors.Find(id);

            Instructor instructor = db.Instructors
                                      .Include(i => i.OfficeAssignment)
                                      .Single(i => i.InstructorID == id);

            if (instructor == null)
            {
                return HttpNotFound();
            }
            ViewBag.OfficeAssignment = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.InstructorID);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditInstructor(/*[Bind(Include = "InstructorID,LastName,LastName,HireDate,")] Instructor instructor*/ int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Instructor instructorToUpdate = db.Instructors
                                      .Include(i => i.OfficeAssignment)
                                      .Where(i => i.InstructorID == id)
                                      .Single();  

            if(TryUpdateModel("instructorToUpdate", "", new string[] { "LastName", "LastName", "HireDate", "OfficeAssignment" }))
            {                
                try
                {
                    if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                    {
                        instructorToUpdate.OfficeAssignment.Location = null;
                    }
                    db.Entry(instructorToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException ex)
                {
                    ModelState.AddModelError("", ex);
                }
                
            }
            return View(instructorToUpdate);

            //if (ModelState.IsValid)
            //{
            //    db.Entry(instructor).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.OfficeAssignment = new SelectList(db.OfficeAssignments, "InstructorID", "Location", instructor.InstructorID);
            //ViewBag.OfficeAssignment = instructor.OfficeAssignment.Location;
            //return View(instructor);
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            db.Instructors.Remove(instructor);
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
