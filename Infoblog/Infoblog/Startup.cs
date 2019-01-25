using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Infoblog.Startup))]
namespace Infoblog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
