using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using MyBlogNew.Identity;

[assembly: OwinStartup(typeof(MyBlogNew.Startup1))]

namespace MyBlogNew
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"), ///  未登录时，跳转到登录页面
                LogoutPath = new PathString("/Home/Index"),  //  退出登录时，跳转到首页
                Provider = new CookieAuthenticationProvider()
            });
			// 讲用户管理器和登录管理器注入到httpcontext中，
			//解决在账号控制器中UserManager、SignInManager丢失问题
            app.CreatePerOwinContext(MyIdentity.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
        }
    }
}
