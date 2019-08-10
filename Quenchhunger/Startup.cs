using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quenchhunger.Startup))]
namespace Quenchhunger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
