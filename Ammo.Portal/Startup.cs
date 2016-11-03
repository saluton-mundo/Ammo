using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ammo.Portal.Startup))]
namespace Ammo.Portal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
