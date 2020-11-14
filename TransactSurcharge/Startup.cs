using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TransactSurcharge.Startup))]
namespace TransactSurcharge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
