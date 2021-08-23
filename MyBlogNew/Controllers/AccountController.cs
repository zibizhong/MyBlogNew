using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using MyBlogNew.Identity;
using MyBlogNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyBlogNew.Controllers
{
    public class AccountController : Controller
    {

        ApplicationUserManager userManager; //= 
        ApplicationSignInManager loginManager; //= 
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return loginManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                loginManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }
        // GET: Account
        private ActionResult RedirectToLocal(string returnUrl)//返回请求url
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);

            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()//打开注册页面
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel viewUserInfo)//提交注册数据
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = viewUserInfo.UserName };
                //  创建数据库管理员
                //  等待服务器处理结果
                var result = await UserManager.CreateAsync(user, viewUserInfo.Password);
                if (result.Succeeded)
                {
                    //  注册成功，跳转到首页
                    return RedirectToAction("Index", "Home");
                }
                
            }
            return View(viewUserInfo);


        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item);
            }
        }


        public ActionResult Login(string ReturnUrl)//打开登陆页面
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Login(string ReturnUrl, LoginViewModel model)//提交登陆数据
        {


            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

            if (result.Equals("Failure"))
            {
                return RedirectToAction("Login", "Account");
            }
            return RedirectToLocal(ReturnUrl);
            /*switch (result)
            {
                case SignInStatus.Success:
                    return Redirect(ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("用户被锁定");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = ReturnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "用户名或密码无效");
                    return View(model);
            }*/

        }
        public ActionResult Logout()//退出登陆
        {
            SignInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect("/Home/Index");
        }
    }
}