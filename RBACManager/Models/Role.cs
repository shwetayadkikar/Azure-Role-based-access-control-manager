using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RBACManager.Models
{
    public class Role
    {
        public string id { get; set; }
        public string name { get; set; }
        public RoleProperties properties { get; set; }
    }

    public class RoleProperties
    {
        public string roleName { get; set; }
        public string description { get; set; }
    }

    public class RoleCollection
    {
        public List<Role> value { get; set; }
    }
}