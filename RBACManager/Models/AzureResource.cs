using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RBACManager.Models
{
    public class AzureResource
    {
        /// <summary>
        /// Specifies the identifying URL of the resource.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Specifies the name of the resource.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Specifies the type of the resource.
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Specifies the supported Azure location where the resource exists. For more information, see List all of the available geo-locations.
        /// </summary>
        public string location { get; set; }  

    }

    public class ResourceCollection
    {
        public List<AzureResource> value { get; set; }
    }
}