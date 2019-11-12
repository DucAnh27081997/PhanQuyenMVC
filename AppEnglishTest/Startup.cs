using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppEnglishTest.Startup))]
namespace AppEnglishTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
