using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pvp.Web.Startup))]
namespace Pvp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
