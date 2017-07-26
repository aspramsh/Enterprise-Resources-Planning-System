using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERPS.Startup))]
namespace ERPS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
