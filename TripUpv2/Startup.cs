using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TripUpv2.Startup))]
namespace TripUpv2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
