using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(APP.Store.Mvc.Startup))]
namespace APP.Store.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
