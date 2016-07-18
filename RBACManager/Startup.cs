using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RBACManager.Startup))]
namespace RBACManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
