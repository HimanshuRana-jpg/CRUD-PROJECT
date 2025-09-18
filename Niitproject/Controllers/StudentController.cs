using Niitproject.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Niitproject.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        ProjectEntities dbObj = new ProjectEntities();
        public ActionResult Studentview(Student Obj)
        {
            if (Obj != null)
                return View(Obj);
            else
                return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student Model)
        {
            Student Obj = new Student();

            Obj.Studentid = Model.Studentid;

            Obj.Studentid = Model.Studentid;
            Obj.Student_Name = Model.Student_Name;
            Obj.Age = Model.Age;
            Obj.DOB = Model.DOB;

            if(Model.Studentid == 0) // it means the data is coming for the insert
            {
                dbObj.Students.Add(Obj);
                dbObj.SaveChanges();
            }
            else
            {
                dbObj.Entry(Obj).State = EntityState.Modified; //The state of the coming object is coming for modified
                dbObj.SaveChanges();
            }
        
            return View("Studentview");
        }

        public ActionResult Studentlist()
        {
            var res = dbObj.Students.ToList();
            return View(res);
        }

        public ActionResult Delete(int Studentid)
        {
            var res = dbObj.Students.Where(x => x.Studentid == Studentid).First();
            dbObj.Students.Remove(res);
            dbObj.SaveChanges();

            var record = dbObj.Students.ToList();
            return View("Studentlist",record);
        }
    }
}