using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CloudClinic.Startup))]
namespace CloudClinic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
