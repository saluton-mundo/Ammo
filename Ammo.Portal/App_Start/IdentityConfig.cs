using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Ammo.Portal.Models;
using Ammo.Domain.Authentication;
using Ammo.Domain.Repositories;
using Ammo.Domain.Utilities;
using Twilio;

namespace Ammo.Portal
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Twilio Begin
            var Twilio = new TwilioRestClient(
              System.Configuration.ConfigurationManager.AppSettings["SMSAccountIdentification"],
              System.Configuration.ConfigurationManager.AppSettings["SMSAccountPassword"]);
            var result = Twilio.SendMessage(
              System.Configuration.ConfigurationManager.AppSettings["SMSAccountFrom"],
              message.Destination, 
              message.Body
            );
            //Twilio doesn't currently have an async API, so return success.
            return Task.FromResult(0);
            // Twilio End
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class IdentityUserManager : UserManager<IdentityUser, Guid>
    {
        public IdentityUserManager(IUserStore<IdentityUser, Guid> store)
            : base(store)
        {
        }

        public static IdentityUserManager Create(IdentityFactoryOptions<IdentityUserManager> options, IOwinContext context)
        {
            var manager = new IdentityUserManager(new UserRepository<IdentityUser>(context.Get<IdentityConnection>().Connection.ConnectionString));

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<IdentityUser, Guid>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<IdentityUser, Guid>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<IdentityUser, Guid>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<IdentityUser, Guid>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }

    // Configure the application sign-in manager which is used in this application.
    public class IdentitySignInManager : SignInManager<IdentityUser, Guid>
    {
        public IdentitySignInManager(IdentityUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(IdentityUser user)
        {
            return user.GenerateUserIdentityAsync((IdentityUserManager)UserManager, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static IdentitySignInManager Create(IdentityFactoryOptions<IdentitySignInManager> options, IOwinContext context)
        {
            return new IdentitySignInManager(context.GetUserManager<IdentityUserManager>(), context.Authentication);
        }
    }
}
