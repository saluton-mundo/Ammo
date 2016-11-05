using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Ammo.Portal.Models;
using Ammo.Domain.Authentication;
using Microsoft.Owin.Security.DataProtection;
using Ammo.Domain.Utilities;
using System.Configuration;
using Owin.Security.Providers.GitHub;

namespace Ammo.Portal
{
    public partial class Startup
    {
        // TODO Remove!!!
        string Admin = "169b3b4e-71cc-4fc6-86b8-240cf23de247";

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext<IdentityConnection>(() => { return new IdentityConnection(ConfigurationManager.ConnectionStrings["AmmoDbConnection"]); });
            app.CreatePerOwinContext<IdentityUserManager>((options, context) =>
            {
                options.DataProtectionProvider = app.GetDataProtectionProvider();
                return IdentityUserManager.Create(options, context);
            });
            app.CreatePerOwinContext<IdentitySignInManager>(IdentitySignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<IdentityUserManager, IdentityUser, Guid>(
                        validateInterval: TimeSpan.FromMinutes(120),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager, DefaultAuthenticationTypes.ApplicationCookie),
                        getUserIdCallback: (claim) => Guid.Parse(claim.GetUserId()))
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            app.UseTwitterAuthentication(
               consumerKey: "vuXxgkaTyyUb23xXVupY67H8K",
               consumerSecret: "sLi7xj3HtEo4Pq4SUcqJBd0ik1kcSGGn0ZbdSbgwVH21jOaOEd"
            );

            app.UseFacebookAuthentication(
               appId: "1759617267622891",
               appSecret: "9611ea03aa8470eb3b75b521cda20410"
            );

            app.UseTrelloAuthentication(
                key: "da65f02baed72c5112a80a23680492ed",
                secret: "47eb0d6404ba1e64958479db41c852f296e70198cebf1944a14091d2f55f84da",
                appName: "Ammo - Bullet Journal"
            );

            app.UseGitHubAuthentication(
                "2605899794d858fd4b1a",
                "f959c3a4810b80811caf16b657b37dcb379922d7"
            );
        }
    }
}