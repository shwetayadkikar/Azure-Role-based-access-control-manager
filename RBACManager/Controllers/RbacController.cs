using RBACManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RBACManager.Controllers
{


    public class RbacController : ApiController
    {
        AzureResourceManager _azureResourceManager;

        public RbacController()
        {
            _azureResourceManager = new AzureResourceManager();
        }

        [HttpGet]
        public async Task<List<AzureResource>> GetResources(string resourceGroup)
        {
            return await _azureResourceManager.GetAllResources(resourceGroup);
        }

     
        [HttpGet]
        public List<AzureResourceGroup> GetResourceGroups()
        {
            List<AzureResourceGroup> resourceGroups = new List<AzureResourceGroup>();
            Task.Run(async () =>
                resourceGroups = await _azureResourceManager.GetAllResourceGroups()
            ).Wait();
            return resourceGroups;
        }

        [HttpGet]
        public async Task<List<Role>> GetRoles()
        {
            return await _azureResourceManager.GetWellKnownRoles();
        }

        [HttpPut]
        //[Route("api/assignRole/{resourceGroup}/{resourceType}/{resourceName}")]
        public async Task<bool> AssignRole(RoleAssignment roleAssignment)
        {
            return await _azureResourceManager.AssignRole(roleAssignment.resourceGroup, roleAssignment.resourceType, roleAssignment.resourceName, roleAssignment);
        }
    }
}
