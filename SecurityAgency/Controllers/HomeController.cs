using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityAgency;

namespace SecurityAgency.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        //DataClasses1DataContext dbContext = new DataClasses1DataContext();
        //static List<User_model> userList = new List<User_model>();
        
        

        public ActionResult Index()
        {
            return View();
        }

//        public ActionResult Login()
//        {
//            return View();
//        }

//        public ActionResult afterLogin(User userModel)
//        {

//            User usertd = (from p in dbContext.Users
//                                 where p.Email == userModel.Email && p.Password == userModel.Password
//                                 select p).SingleOrDefault();
//            if (usertd != null)
//            {
//                ViewBag.Message = "Login Successful";
//            }
//            else
//                ViewBag.Message = "Login unsuccessful";

//            return View();
//        }

        //public ActionResult Admin()
        //{

        //    var userDetail = (from u in dbContext.Users

        //                      select new User_model
        //                      {
        //                          UserId = u.UserID,
        //                          Username = u.UserName,
                                 
        //                      }).ToList();
        //    //userList = userDetail;
        //    return View(userDetail);
        //}

//        //public ActionResult Permission()
//        //{
//        //    var userPermission = (from u in dbContext.Permission
//        //                          select new Permission
//        //                          {
//        //                              PermissionId = u.PermissionId,
//        //                              Name = u.Name,
//        //                              Parent_PermissionId = u.Parent_permissionId
//        //                          }).ToList();

//        //    return View(userPermission);
//        //}

//        public IEnumerable<Role> GetRoleList()
//        {
//            DataClasses1DataContext db = new DataClasses1DataContext();
//            var query = (from p in db.Roles
//                         select p).ToList();

//            //var result = con.Query<Mobiledata>(query);  
//            return query;
//        }

//        public ActionResult Register()
//        {
//            User_model user=new User_model();

//            user.Rolelist = new SelectList(GetRoleList(), "RoleId", "Role");

//            return View(user);
//        }

//        public ActionResult AddUser(User_model user)
//        {

//            user.Rolelist = new SelectList(GetRoleList(), "RoleId", "Role");
//            user.Status = 0;
//            User objUser = new User();
//            //objUser.UserID = user.UserId;
//            objUser.UserName = user.Username;
//            objUser.Email = user.Email;
//            objUser.RoleId = user.RoleId;
//            objUser.Contact = user.Contact;
//            objUser.Status = user.Status;
//            objUser.Address = user.Address;
//            //var errors = ModelState.Values.SelectMany(v => v.Errors);
//            if (ModelState.IsValid)
//            {
//                dbContext.Users.InsertOnSubmit(objUser);
//                // executes the appropriate commands to implement the changes to the database
//                dbContext.SubmitChanges();
//                ViewBag.Message = "Registered";
//                return View();
//            }

//            else
//            {
//                ViewBag.Message = "Not Valid";
//                return View("Register", user);
//            }

//        }

//        public ActionResult ApproveUser(User_model user)
//        {

            
//            user.Status = 1;
//            User usertd = dbContext.Users.Single(u => u.UserID == user.UserId);
//            usertd.Status = user.Status;
//            dbContext.SubmitChanges();
//            userList = (from u in dbContext.Users

//                        select new User_model
//                      {
//                          UserId = u.UserID,
//                          Username = u.UserName,
//                          Status = u.Status
//                      }).ToList();
//            return View("Admin",userList);
//        }

//        public ActionResult DisapproveUser(User_model user)
//        {
            
//            User usertd = dbContext.Users.Single(u => u.UserID == user.UserId);
//            dbContext.Users.DeleteOnSubmit(usertd);
//            dbContext.SubmitChanges();
//            userList = (from u in dbContext.Users

//                        select new User_model
//                            {
//                                UserId = u.UserID,
//                                Username = u.UserName,
//                                Status = u.Status
//                            }).ToList();
//            return View("Admin", userList);

//        }

//        public ActionResult PermissionSubmit()
//        {
//            return View();
//        }

    }
}



             
        
    


    

