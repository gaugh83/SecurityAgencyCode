using SecurityAgency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecurityAgency.Controllers
{
    public class AdminController : Controller
    {
//        //
//        // GET: /Admin/

//        //DataClasses1DataContext dbContext = new DataClasses1DataContext();
//        //static List<User_model> userList = new List<User_model>();

        public ActionResult User()
        {
            return View();
        }

//        public IEnumerable<Role> GetRoleList()
//        {
//            DataClasses1DataContext db = new DataClasses1DataContext();
//            var query = (from p in db.Roles
//                         select p).ToList();
                      
//            return query;
//        }

        //public actionresult adduser()
        //{
        //    user_model user = new user_model();

        //    user.rolelist = new selectlist(getrolelist(), "roleid", "role1");

        //    return view(user);
        //}
//        [HttpPost]
//        public ActionResult AddUser(User_model user)
//        {
//            user.Rolelist = new SelectList(GetRoleList(), "RoleId", "Role1");
//            //user.Status = 0;
//            user.CreatedDate = System.DateTime.Now;
//            user.Last_ModifiedDate = System.DateTime.Now;
//            User objUser = new User();
//            //objUser.UserID = user.UserId;
//            objUser.UserName = user.Username;
//            objUser.Email = user.Email;
//            objUser.RoleId = user.RoleId;
//            objUser.Contact = user.Contact;
//            objUser.Address = user.Address;
//            objUser.CreatedDate = user.CreatedDate;
//            objUser.CreatedBy = 1;
//            objUser.IsDeleted = false;
//            objUser.Active = true;
//            objUser.Last_ModifiedDate = user.Last_ModifiedDate;
//            objUser.Last_ModifiedBy = 1;
//            objUser.Zip = user.Zip;
//            objUser.DeletedBy = 1;
//            //var errors = ModelState.Values.SelectMany(v => v.Errors);
//            if (ModelState.IsValid)
//            {
//                dbContext.Users.InsertOnSubmit(objUser);
//                // executes the appropriate commands to implement the changes to the database
//                dbContext.SubmitChanges();
//                ViewBag.Message = "New user added";
//                return View("After_addUser");
//            }

//            else
//            {
//                ViewBag.Message = "Not Valid";
//                return View("AddUser", user);
//            }

//         }

       
//        public ActionResult AddCustomer()
//        {
//            CustomerDetails customer=new CustomerDetails();
//            return View();
//        }

//    [HttpPost]
//        public ActionResult AddCustomer(CustomerDetails customer) 
//        {

//            customer.CreatedDate = System.DateTime.Now;
//            customer.Last_ModifiedDate = System.DateTime.Now;
//            Customer objCustomer = new Customer();
//            //objUser.UserID = user.UserId;
//            objCustomer.Name = customer.Name;
//            objCustomer.Email = customer.Email;
//            objCustomer.ContactNo = customer.ContactNo;
//            objCustomer.Address = customer.Address;
//            objCustomer.CreatedDate = customer.CreatedDate;
//            objCustomer.CreatedBy = 1;
//            objCustomer.IsDeleted = false;
//            objCustomer.Active = true;
//            objCustomer.Last_ModifiedDate = customer.Last_ModifiedDate;
//            objCustomer.Last_ModifiedBy = 1;
//            objCustomer.Zip = customer.Zip;
//            objCustomer.DeletedBy = 1;
//            //var errors = ModelState.Values.SelectMany(v => v.Errors);
//            if (ModelState.IsValid)
//            {
//                dbContext.Customers.InsertOnSubmit(objCustomer);
//                // executes the appropriate commands to implement the changes to the database
//                dbContext.SubmitChanges();
//                ViewBag.Message = "New customer added";
//                return View("After_addUser");
//            }

//            else
//            {
//                ViewBag.Message = "Not Valid";
//                return View(customer);
//            }
//        }

//    public ActionResult AddGuard()
//    {
//        SecurityGuardDetails customer = new SecurityGuardDetails();
//        return View();
//    }

//    [HttpPost]
//    public ActionResult AddGuard(SecurityGuardDetails guard)
//    {

//        guard.CreatedDate = System.DateTime.Now;
//        guard.Last_ModifiedDate = System.DateTime.Now;
//        Guard objGuard = new Guard();
//        //objUser.UserID = user.UserId;
//        objGuard.Name = guard.Name;
//        objGuard.Email = guard.Email;
//        objGuard.ContactNo = guard.ContactNo;
//        objGuard.Address = guard.Address;
//        objGuard.CreatedDate = guard.CreatedDate;
//        objGuard.CreatedBy = 1;
//        objGuard.IsDeleted = false;
//        objGuard.Active = true;
//        objGuard.Last_ModifiedDate = guard.Last_ModifiedDate;
//        objGuard.Last_ModifiedBy = 1;
//        objGuard.Zip = guard.Zip;
//        objGuard.DeletedBy = 1;
//        //var errors = ModelState.Values.SelectMany(v => v.Errors);
//        if (ModelState.IsValid)
//        {
//            dbContext.Guards.InsertOnSubmit(objGuard);
//            // executes the appropriate commands to implement the changes to the database
//            dbContext.SubmitChanges();
//            ViewBag.Message = "New guard added";
//            return View("After_addUser");
//        }

//        else
//        {
//            ViewBag.Message = "Not Valid";
//            return View(guard);
//        }
//    }
    }
}
