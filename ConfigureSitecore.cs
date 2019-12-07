using Microsoft.Extensions.DependencyInjection;
using Plugin.Sync.Commerce.RenderEntityView.Pipelines;
using Plugin.Sync.Commerce.RenderEntityView.Pipelines.Blocks;
//using Plugin.Sync.Commerce.RenderEntityView.Pipelines.Blocks;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;
using System.Reflection;

namespace Plugin.Sync.Commerce.RenderEntityView
{
    public class ConfigureSitecore : IConfigureSitecore
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);

            services.Sitecore().Pipelines(config => config
                .ConfigurePipeline<IConfigureServiceApiPipeline>(configure => configure.Add<ConfigureServiceApiBlock>())

                 .AddPipeline<IExportCommerceEntityPipeline, ExportCommerceEntityPipeline>(
                    configure =>
                    {
                        configure
                            .Add<GetEntityBlock>()
                            .Add<GetTemplateFromSitecoreBlock>()
                            .Add<GetTemplateFromFilesystemBlock>()
                            .Add<RenderEntityViewBlock>();
                            //.Add<GetResponseBlock>();
                    }));

            services.RegisterAllCommands(assembly);
            //services.AddSingleton<IViewRenderService, ViewRenderService>();
            //services.AddScoped<IViewRenderService, ViewRenderService>();
        }
    }
}