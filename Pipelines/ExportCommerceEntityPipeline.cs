using Microsoft.Extensions.Logging;
using Plugin.Sync.Commerce.RenderEntityView.Pipelines.Arguments;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Pipelines;

namespace Plugin.Sync.Commerce.RenderEntityView.Pipelines
{
    public class ExportCommerceEntityPipeline : CommercePipeline<ExportCommerceEntityArgument, ExportCommerceEntityArgument>, 
        IExportCommerceEntityPipeline, IPipeline<ExportCommerceEntityArgument, ExportCommerceEntityArgument, CommercePipelineExecutionContext>, 
        IPipelineBlock<ExportCommerceEntityArgument, ExportCommerceEntityArgument, CommercePipelineExecutionContext>, IPipelineBlock, IPipeline
    {
        public ExportCommerceEntityPipeline(IPipelineConfiguration<IExportCommerceEntityPipeline> configuration, ILoggerFactory loggerFactory) 
            : base((IPipelineConfiguration)configuration, loggerFactory)
        {
        }
    }
}

  