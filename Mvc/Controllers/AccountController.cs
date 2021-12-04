using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc.Models;
using System.Web.Security;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Text;
using System.Drawing;

namespace Mvc.Controllers
{
   
    public class AccountController : Controller
    {
        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Detail model)
        {
            using (var context = new Entities())
            {
                bool isValid = context.Details.Any(x => x.Email == model.Email && x.Password == model.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Email,false);
                    return RedirectToAction("Index", "Home", new { loginUserEmail = model.Email });
                }

               ModelState.AddModelError("","Invalid Username and Password");
                return View();
            }
               
        }

        //[HandleError]
        //[HandleError(ExceptionType = typeof(Exception), View = "~/Views/Error/NullReference.cshtml")]
        //public ActionResult Contact()
        public ActionResult Detail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Detail(ImageViewModel model)
        {
            //using db entities
            if (model.Image.InputStream != null)
            {
                byte[] arr = ReadFully(model.Image.InputStream);
                string imreBase64Data = Convert.ToBase64String(arr);
                
                using (var context = new Entities())
            {
                Detail data = new Detail
                {
                    Id = model.Id,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Email = model.Email,
                    Password = model.Password,
                    Phone = model.Phone,
                    Country = model.Country,
                    Image= imreBase64Data
                };

                context.Details.Add(data);
                context.SaveChanges();
            }
            }
                return RedirectToAction("Login");
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }


        //@logout handling
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}