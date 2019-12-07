using Microsoft.AspNetCore.OData.Builder;
using Sitecore.Commerce.Core;
using Sitecore.Commerce.Core.Commands;
using Sitecore.Framework.Conditions;
using Sitecore.Framework.Pipelines;
using System.Threading.Tasks;

namespace Plugin.Sync.Commerce.RenderEntityView
{
    [PipelineDisplayName("CatalogExportConfigureServiceApiBlock")]
    public class ConfigureServiceApiBlock : PipelineBlock<ODataConventionModelBuilder, ODataConventionModelBuilder, CommercePipelineExecutionContext>
    {
        public override Task<ODataConventionModelBuilder> Run(ODataConventionModelBuilder modelBuilder, CommercePipelineExecutionContext context)
        {
            Condition.Requires(modelBuilder).IsNotNull($"{this.Name}: The argument cannot be null.");

            var RenderEntityView = modelBuilder.Action("RenderEntityView");
            RenderEntityView.ReturnsFromEntitySet<CommerceCommand>("Commands");

            var RenderEntityViews = modelBuilder.Action("RenderEntityViews");
            RenderEntityViews.ReturnsFromEntitySet<CommerceCommand>("Commands");

            return Task.FromResult(modelBuilder);
        }
    }
}
