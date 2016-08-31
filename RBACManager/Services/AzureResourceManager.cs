using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RBACManager.Models;

namespace RBACManager
{
    public class AzureResourceManager
    {
        string AzureResourceMangementResourceId = ConfigurationManager.AppSettings["AzureResourceMangementAPIUrl"];

        public string SubscriptionId
        {
            get
            {
                return ConfigurationManager.AppSettings["SubscriptionId"];
            }
        }

        private HttpClient _client = new HttpClient();

        private string apiVersion = "2015-01-01";
        private string AccessToken
        {
            get
            {
                if (HttpContext.Current.Cache["AccessToken"] != null)
                {
                    return Convert.ToString(HttpContext.Current.Cache["AccessToken"]);
                }
                else
                {
                    string token = (Task.Run(async () =>
                     {
                         var authContext = await AzureAuthenticationManager.GetAccessTokenAsync(AzureResourceMangementResourceId);
                         return authContext.AccessToken;
                     })).Result;
                    HttpContext.Current.Cache["AccessToken"] = token;
                    return token;
                }
            }
        }



        public AzureResourceManager()
        {
            _client.BaseAddress = new Uri("https://management.azure.com/");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<AzureResource>> GetAllResources(string resourceGroup)
        {
            //https://management.azure.com/subscriptions/{subscription-id}/resourcegroups/{resource-group-name}/resources?$top={top}$skiptoken={skiptoken}&$filter={filter}&api-version={api-version}



            var response = await _client.GetAsync($"subscriptions/{SubscriptionId}/resourceGroups/{resourceGroup}/resources?api-version={apiVersion}");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var collection = JsonConvert.DeserializeObject<ResourceCollection>(jsonResponse);
            return collection.value;
        }


        public async Task<List<AzureResourceGroup>> GetAllResourceGroups()
        {
            var response = await _client.GetAsync($"subscriptions/{SubscriptionId}/resourceGroups?api-version={apiVersion}");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var collection = JsonConvert.DeserializeObject<ResourceGroupCollection>(jsonResponse);
            return collection.value;
        }


        //https://management.azure.com/subscriptions/09cbd307-aa71-4aca-b346-5f253e6e3ebb/providers/Microsoft.Authorization/roleDefinitions?api-version=2014-07-01-preview HTTP/1.1


        public async Task<List<Role>> GetWellKnownRoles()
        {
            var response = await _client.GetAsync($"subscriptions/{SubscriptionId}/providers/Microsoft.Authorization/roleDefinitions?api-version=2014-07-01-preview");

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var collection = JsonConvert.DeserializeObject<RoleCollection>(jsonResponse);
            return collection.value;
        }

        //
        //https://management.azure.com/{scope}/providers/Microsoft.Authorization/roleAssignments/{role-assignment-id}?api-version={api-version}

        public async Task<bool> AssignRole(string resourceGroup, string resourceType, string resourceName, RoleAssignment roleAssignment)
        {
            RoleAssignmentContent roleAssignmentContent = new RoleAssignmentContent();
            roleAssignmentContent.properties = roleAssignment;
            var roleassignmentid = new Guid();
            var response = await _client.PutAsJsonAsync($"/subscriptions/{SubscriptionId}/resourceGroups/{resourceGroup}/providers/{resourceType}/{resourceName}/providers/Microsoft.Authorization/roleAssignments/{roleassignmentid.ToString()}?api-version=2015-07-01", roleAssignmentContent);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode;
        }
    }
}