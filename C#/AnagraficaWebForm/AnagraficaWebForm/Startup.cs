using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnagraficaWebForm.Startup))]
namespace AnagraficaWebForm
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
