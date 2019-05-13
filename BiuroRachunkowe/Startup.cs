using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BiuroRachunkowe.Startup))]
namespace BiuroRachunkowe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
