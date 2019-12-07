using Plugin.Sync.Commerce.RenderEntityView.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sync.Commerce.RenderEntityView.Pipelines
{
    [PipelineDisplayName("IExportCommerceEntityPipeline")]
    public interface IExportCommerceEntityPipeline : IPipeline<ExportCommerceEntityArgument, ExportCommerceEntityArgument, CommercePipelineExecutionContext>, 
        IPipelineBlock<ExportCommerceEntityArgument, ExportCommerceEntityArgument, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
    }
}