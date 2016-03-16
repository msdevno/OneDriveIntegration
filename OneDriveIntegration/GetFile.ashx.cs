using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OneDriveIntegration
{
    /// <summary>
    /// Summary description for GetFile
    /// </summary>
    public class GetFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "Application/binary";

            var path = context.Server.MapPath("~/index.html");
            context.Response.WriteFile(path);
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