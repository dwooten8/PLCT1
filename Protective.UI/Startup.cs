using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Protective.UI.Startup))]
namespace Protective.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
