﻿using Newtonsoft.Json.Linq;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Conditions;

namespace Plugin.Sync.Commerce.RenderEntityView.Models
{
    /// <summary>
    /// Container class to hold Commerce entity in Commerce pipeline context
    /// </summary>
    public class EntityDataModel : Model
    {
        public EntityDataModel(object entity)
        {
            Condition.Requires<object>(entity).IsNotNull("Entity cannot be null");
            this.Entity = entity;
        }
        public object Entity { get; set; }
    }
}
