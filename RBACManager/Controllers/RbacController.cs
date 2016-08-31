using RBACManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using RBACManager.Services;
using System.Web;

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

        [HttpGet]
        public async Task<List<UserModel>> GetUsers(string searchQuery = "")
        {
          var userList = new List<User>();
            try
            {
                ActiveDirectoryClient client = AzureGraphAPIHelper.GetActiveDirectoryClient();

                IPagedCollection<IUser> pagedCollection = await client.Users.Where(user =>
                user.UserPrincipalName.StartsWith(searchQuery) ||
                user.DisplayName.StartsWith(searchQuery) ||
                user.GivenName.StartsWith(searchQuery) ||
                user.Surname.StartsWith(searchQuery)).ExecuteAsync();

                if (pagedCollection != null)
                {
                    do
                    {
                        List<IUser> usersList = pagedCollection.CurrentPage.ToList();
                        foreach (IUser user in usersList)
                        {
                            userList.Add((User)user);
                        }
                        pagedCollection = await pagedCollection.GetNextPageAsync();
                    } while (pagedCollection != null);
                }
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Authorization Required."))
                {
                    //
                    // Send an OpenID Connect sign-in request to get a new set of tokens.
                    // If the user still has a valid session with Azure AD, they will not be prompted for their credentials.
                    // The OpenID Connect middleware will return to this controller after the sign-in response has been handled.
                    //                 
                }
            }
            return userList.Select(x => new UserModel { Name = x.DisplayName, Id = x.ObjectId }).ToList();
        }

        [HttpPut]
        //[Route("api/assignRole/{resourceGroup}/{resourceType}/{resourceName}")]
        public async Task<bool> AssignRole(RoleAssignment roleAssignment)
        {
            return await _azureResourceManager.AssignRole(roleAssignment.resourceGroup, roleAssignment.resourceType, roleAssignment.resourceName, roleAssignment);
        }
    }
}
