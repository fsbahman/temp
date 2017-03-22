using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(test_aspx.Startup))]
namespace test_aspx
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
