using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aplicacao.Startup))]
namespace Aplicacao
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
