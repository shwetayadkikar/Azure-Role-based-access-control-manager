using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RBACManager.Models
{
    public class RoleAssignment
    {
        public string roleDefinitionId { get; set; }
        public string principalId { get; set; }

        public string resourceGroup { get; set; }
        public string resourceType { get; set; }
        public string resourceName { get; set; }
    }

    public class RoleAssignmentContent
    {
        public RoleAssignment properties { get; set; }
    }
}