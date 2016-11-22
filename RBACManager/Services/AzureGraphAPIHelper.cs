using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.Azure.ActiveDirectory.GraphClient.Extensions;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace RBACManager.Services
{
    public class AzureGraphAPIHelper
    {
        private static string GraphResourceId = ConfigurationManager.AppSettings["GraphUrl"];
        private static string TenantId = ConfigurationManager.AppSettings["TenantId"];

        public static string AccessToken
        {
            get; set;
        }

        public static async Task<string> AcquireTokenAsync()
        {
            if (AccessToken == null || string.IsNullOrEmpty(AccessToken))
            {
                var authContext = await AzureAuthenticationManager.GetAccessTokenAsync(GraphResourceId);
                AccessToken = authContext.AccessToken;
            }
            return AccessToken;
        }

        public static ActiveDirectoryClient GetActiveDirectoryClient()
        {
            Uri baseServiceUri = new Uri(GraphResourceId);
            ActiveDirectoryClient activeDirectoryClient =
                new ActiveDirectoryClient(new Uri(baseServiceUri, TenantId), async () => await AcquireTokenAsync());
            return activeDirectoryClient;
        }
    }
}