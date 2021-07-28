using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample_Web_Application_1.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Sample_Web_Application_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment hostEnvironment1;
        private static UserContext UC = new UserContext();
        private static AdminContext AC = new AdminContext();
        private static ProductContext PC = new ProductContext();
        
        private static bool admin_logged = false;
        private static bool user_logged = true;
        private static int admin_id = 0;
        private static int user_id = 0;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            this.hostEnvironment1 = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult User_Index()
        {
            return View();
        }

        public IActionResult Admin_Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult View_User_Profile()
        {
            return View(UC.Get_user_by_id(user_id));
        }

        [HttpPost]
        public IActionResult View_User_Profile(User Curr_user)
        {
            return Redirect("User_Index");
        }

        public IActionResult View_Admin_Profile()
        {
            return View(AC.Get_admin_by_id(admin_id));
        }

        [HttpPost]
        public IActionResult View_Admin_Profile(User Curr_user)
        {
            return Redirect("Admin_Index");
        }


        public IActionResult Edit_User_Profile()
        {
            return View(UC.Get_user_by_id(user_id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_User_Profile(User Curr_user)
        {

            User To_Insert = UC.Get_user_by_id(user_id);

            To_Insert.First_Name = Curr_user.First_Name;
            To_Insert.Middle_Name = Curr_user.Middle_Name;
            To_Insert.Last_Name = Curr_user.Last_Name;
            To_Insert.Gender = Curr_user.Gender;
            To_Insert.DOB = Curr_user.DOB;
            To_Insert.Address = Curr_user.Address;

            //if (ModelState.IsValid)
            //{
                Console.WriteLine("Saving changes of Pic");
                string wwwRootPath = hostEnvironment1.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(Curr_user.PictureFile.FileName);
                string extension = Path.GetExtension(Curr_user.PictureFile.FileName);
                To_Insert.PicTitle = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await Curr_user.PictureFile.CopyToAsync(filestream);
                }
           // }
            await UC.SaveChangesAsync();
            return Redirect("User_Index");
        }

        
        public IActionResult Edit_Admin_Profile()
        {
            return View(AC.Get_admin_by_id(admin_id));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Admin_Profile(Admin Curr_admin)
        {

            Admin To_Insert = AC.Get_admin_by_id(admin_id);

            To_Insert.First_Name = Curr_admin.First_Name;
            To_Insert.Middle_Name = Curr_admin.Middle_Name;
            To_Insert.Last_Name = Curr_admin.Last_Name;
            To_Insert.Gender = Curr_admin.Gender;
            To_Insert.DOB = Curr_admin.DOB;
            To_Insert.Address = Curr_admin.Address;

            //if (ModelState.IsValid)
            //{
                Console.WriteLine("Saving changes of Pic");
                string wwwRootPath = hostEnvironment1.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(Curr_admin.PictureFile.FileName);
                string extension = Path.GetExtension(Curr_admin.PictureFile.FileName);
                To_Insert.PicTitle = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await Curr_admin.PictureFile.CopyToAsync(filestream);
                }
            //}
            await AC.SaveChangesAsync();
            return Redirect("Admin_Index");
        }

        public IActionResult View_Product()
        {
            return View(PC.get_available_products());
        }

        public IActionResult Add_Product()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Product(Product curr_prd)
        {
            Product product_  = new Product();
            product_.SellerId = user_id;
            product_.status = "Pending";
            product_.Title = curr_prd.Title;
            product_.Description = curr_prd.Description;
            product_.Category = curr_prd.Category;
            product_.Price = curr_prd.Price;
            product_.ShippingCharges = curr_prd.ShippingCharges;

            if (ModelState.IsValid)
            {
                Console.WriteLine("Saving changes of Pic");
                string wwwRootPath = hostEnvironment1.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(curr_prd.PictureFile.FileName);
                string extension = Path.GetExtension(curr_prd.PictureFile.FileName);
                product_.PicTitle = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var filestream = new FileStream(path, FileMode.Create))
                {
                    await curr_prd.PictureFile.CopyToAsync(filestream);
                }
            }
            PC.Products.Add(product_);
            await PC.SaveChangesAsync();
            return Redirect("User_Index");
        }

        public IActionResult Login1()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult Login1(User Curr_User)
        {
            using (var User_ctx = new UserContext())
            {
                var user_ = User_ctx.Get_user(Curr_User.Username, Curr_User.Password);
                if (user_ != null)
                {
                    user_logged = true;
                    user_id = user_.UserId;
                    Console.WriteLine("User with username: " + Curr_User.Username + " and password: " + Curr_User.Password + " Logged in");
                    return Redirect("User_Index");
                }
                else
                {
                    Console.WriteLine("User with username: " + Curr_User.Username + " and password: " + Curr_User.Password + " could not Log in");
                    return Redirect("Index");
                }
            }
        }

        public IActionResult Login2()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult Login2(Admin Curr_Admin)
        {
            using (var Admin_ctx = new AdminContext())
            {
                Admin admin_ = Admin_ctx.Get_admin(Curr_Admin.Username, Curr_Admin.Password, Curr_Admin.Account_key);
                if (admin_ != null)
                {
                    admin_logged = true;
                    admin_id = admin_.AdminId;
                    Console.WriteLine("Admin with username: " + Curr_Admin.Username + " and password: " + Curr_Admin.Password + " and key: " + Curr_Admin.Account_key + " Logged in");
                    return Redirect("Admin_Index");
                }
                else
                {
                    Console.WriteLine("Admin with username: " + Curr_Admin.Username + " and password: " + Curr_Admin.Password + " and key: " + Curr_Admin.Account_key + " could not log in");
                    return Redirect("Index");
                }
            }
        }

        public IActionResult SignUP1()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult SignUP1(User Curr_User)
        {
            UserContext UC = new UserContext();
            User To_Insert = new User();
            To_Insert.Username = Curr_User.Username;
            To_Insert.Password = Curr_User.Password;
            To_Insert.Email = Curr_User.Email;
            To_Insert.Contact = Curr_User.Contact;
            UC.Users.Add(To_Insert);
            UC.SaveChanges();
            Console.WriteLine("User Added");
            return Redirect("Index");
        }

        public IActionResult SignUP2()
        {
            return View();
        }

        [HttpPost]
        public RedirectResult SignUP2(Admin Curr_Admin)
        {
            AdminContext AC = new AdminContext();
            Admin To_Insert = new Admin();
            To_Insert.Username = Curr_Admin.Username;
            To_Insert.Password = Curr_Admin.Password;
            To_Insert.Email = Curr_Admin.Email;
            To_Insert.Contact = Curr_Admin.Contact;
            To_Insert.Account_key = Curr_Admin.Account_key;
            AC.Admins.Add(To_Insert);
            AC.SaveChanges();
            Console.WriteLine("Admin Added");
            return Redirect("Index");
        }

        public IActionResult Buy(int productid)
        {
            Product curr_prd = PC.Get_product_by_id(productid);
            curr_prd.BuyerId = user_id;
            curr_prd.status = "Sold";
            PC.SaveChanges();
            return Redirect("User_Index");
        }

        public IActionResult Upload_Requests()
        {
            return View(PC.get_pending_products());
        }

        public IActionResult Approve_Product(int productid)
        {
            Console.WriteLine("The productId recved as a parameter: " + productid);
            Product curr_prd = PC.Get_product_by_id(productid);
            curr_prd.approvingId = admin_id;
            curr_prd.status = "Available";
            PC.SaveChanges();
            return Redirect("Admin_Index");
        }

        public IActionResult Buying_History()
        {
            return View(PC.get_purchase_history_of_user(user_id));
        }
        [HttpPost]
        public IActionResult Rate_Product(Product prd)
        {
            var to_save = PC.Get_product_by_id(prd.ProductId);
            to_save.Rating = prd.Rating;
            PC.SaveChanges();
            return Redirect("User_Index");
        }
        public IActionResult Rate_Product(int productid)
        {
            return View(PC.Get_product_by_id(productid));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
