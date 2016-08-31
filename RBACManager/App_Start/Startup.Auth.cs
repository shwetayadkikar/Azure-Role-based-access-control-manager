using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using RBACManager.Models;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Configuration;
using System.Globalization;
using RBACManager.Services;

namespace RBACManager
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        private static string clientId = ConfigurationManager.AppSettings["ClientId"];
        private static string appKey = ConfigurationManager.AppSettings["ClientSecret"];
        private static string aadInstance = ConfigurationManager.AppSettings["AADInstance"];
        private static string tenant = ConfigurationManager.AppSettings["Tenant"];
        private static string postLogoutRedirectUri = ConfigurationManager.AppSettings["PostLogoutRedirectUri"];

        public static readonly string Authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

        // This is the resource ID of the AAD Graph API.  We'll need this to request a token to call the Graph API.
        string graphResourceId = ConfigurationManager.AppSettings["GraphUrl"];

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {

                    ClientId = clientId,
                    Authority = Authority,
                    PostLogoutRedirectUri = postLogoutRedirectUri,

                    Notifications = new OpenIdConnectAuthenticationNotifications()
                    {
                        //
                        // If there is a code in the OpenID Connect response, redeem it for an access token and refresh token, and store those away.
                        //

                        AuthorizationCodeReceived = (context) =>
                        {
                            var code = context.Code;

                            // Create a Client Credential Using an Application Key
                            ClientCredential credential = new ClientCredential(clientId, appKey);
                            //string userObjectID = context.AuthenticationTicket.Identity.FindFirst(
                            //    "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
                            AuthenticationContext authContext = new AuthenticationContext(Authority);
                            var uri = new Uri(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Path));
                            Task.Run(async () =>
                            {
                                AuthenticationResult result = await authContext.AcquireTokenByAuthorizationCodeAsync(
                                    code, uri, credential, graphResourceId);
                                AzureGraphAPIHelper.AccessToken = result.AccessToken;
                            }).Wait();
                            return Task.FromResult(0);
                        }

                    }

                });
        }

    }
}