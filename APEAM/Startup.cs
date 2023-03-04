using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(APEAM.Startup))]
namespace APEAM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
