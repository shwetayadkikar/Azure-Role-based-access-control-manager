using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RBACManager
{
    public class AzureAuthenticationManager
    {


        public static async Task<AuthenticationResult> GetAccessTokenAsync(string resource = "https://management.azure.com/")
        {
            string clientId = ConfigurationManager.AppSettings["ClientId"];
            string clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
            string tenantId = ConfigurationManager.AppSettings["TenantId"];

            AuthenticationContext authContext = new AuthenticationContext
                    ("https://login.windows.net/" /* Azure AD URI */
                        + $"{tenantId}" /* Tenant ID */);

            var credential = new ClientCredential(clientId, clientSecret);

            var token = await authContext.AcquireTokenAsync(resource, credential);

            
            return token;
        }
    }
}