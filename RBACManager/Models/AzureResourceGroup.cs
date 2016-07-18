using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RBACManager.Models
{
    public class AzureResourceGroup
    {
        /// <summary>
        /// Specifies the identifying URL of the resource group.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Specifies the name of the resource group.
        /// </summary>
        public string name { get; set; }

    }

    public class ResourceGroupCollection
    {
        public List<AzureResourceGroup> value { get; set; }
    }
}