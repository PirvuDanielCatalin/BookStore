using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Booktopia.Startup))]
namespace Booktopia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
