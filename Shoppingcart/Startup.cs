using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Shoppingcart.Startup))]
namespace Shoppingcart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
