using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BMG.Startup))]
namespace BMG
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
