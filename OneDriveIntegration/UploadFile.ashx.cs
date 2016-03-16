using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Web;

namespace OneDriveIntegration
{
    /// <summary>
    /// Summary description for UploadFile
    /// </summary>
    public class UploadFile : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // Get the things from the request body
            var accessToken = context.Request.Params["accessToken"];
            var folderId = context.Request.Params["folderId"];

            // Decode the JWT access token and get the tenant information (aud)
            var token = new JwtSecurityToken(accessToken);
            var tenant = token.Claims.Where(c => c.Type == "aud").Single();
            var tenantUrl = tenant.Value;

            var filename = "index.html";
            var path = context.Server.MapPath($"~/{filename}");

            // Build the API URL
            var baseUrl = $"{tenant.Value}/_api/v2.0/me/";
            var url = $"{baseUrl}drive/items/{folderId}/children/{filename}/content";

            using ( var client = new WebClient())
            {
                client.Headers["Authorization"] = "bearer "+accessToken;
                
                try {

                    // You could do an async here...
                    client.UploadFile(url, "PUT", path);
                } catch (Exception ex)
                {
                    var i = 0;
                    i++;
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}