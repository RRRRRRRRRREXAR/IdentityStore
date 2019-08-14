using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityStore.Startup))]
namespace IdentityStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
