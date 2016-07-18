using RBACManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RBACManager.ViewModel
{
    public class RBACModel
    {
        public List<AzureResource> Resources { get; set; }
        public List<Role> Roles { get; set;}

        public AzureResource SelectedResource { get; set; }

        public Role RoleToAssign { get; set; }

        public string PrincipalId { get; set; }
    }
}