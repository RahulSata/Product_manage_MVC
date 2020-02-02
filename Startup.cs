using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Product_manage_RAHUL_SATA.Startup))]
namespace Product_manage_RAHUL_SATA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
