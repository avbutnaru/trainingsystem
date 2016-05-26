using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrainingSystem.Startup))]
namespace TrainingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
