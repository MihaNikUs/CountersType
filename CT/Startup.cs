using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CT.Startup))]
namespace CT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
