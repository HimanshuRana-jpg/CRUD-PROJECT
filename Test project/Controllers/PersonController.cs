using System.Linq;
using System.Web.Mvc;
using Test_project.Models;

namespace Test_project.Controllers
{
    public class PersonController : Controller
    {

        FirstDBEntities dbobj = new FirstDBEntities();//MyModel

        public ActionResult Person(person obj)
        {
                return View(obj);
        }

        [HttpPost]
        public ActionResult AddPerson(person model)
        {
            if(ModelState.IsValid)
            {
                person obj = new person();
                obj.PersonID = model.PersonID;
                obj.FName = model.FName;
                obj.LName = model.LName;

                if(model.PersonID == 0)
                {
                    dbobj.person.Add(obj);
                    dbobj.SaveChanges();

                }
                else
                {
                    dbobj.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    dbobj.SaveChanges();
                }
            }
            
            ModelState.Clear();

            return View("Person");
        }

        public ActionResult Personlist()
        {
            var res = dbobj.person.ToList();
            return View(res);
        }

        public ActionResult Delete(int Personid)
        {
            var res = dbobj.person.Where(x => x.PersonID == Personid).First();
            dbobj.person.Remove(res);
            dbobj.SaveChanges();

            var list = dbobj.person.ToList();

            return View("Personlist", list);
        }
    }
}