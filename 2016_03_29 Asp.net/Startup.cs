using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_2016_03_29_Asp.net.Startup))]
namespace _2016_03_29_Asp.net
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
