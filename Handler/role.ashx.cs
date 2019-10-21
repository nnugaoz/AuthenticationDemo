using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth.Handler
{
    /// <summary>
    /// role 的摘要说明
    /// </summary>
    public class role : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

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