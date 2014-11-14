using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Security;
using MvcWebRole1.Entities;

namespace MvcWebRole1.Controllers
{
    public class AuthController : ApiController
    {
        public bool Post([FromBody] Identity identity)
        {
            if (identity.Authenticate())
            {
                FormsAuthentication.SetAuthCookie(identity.Name, true);
                return true;
            }
            return false;
        }

        [Authorize]
        public Identity Get()
        {
            return new Identity(Thread.CurrentPrincipal.Identity.Name,
                Thread.CurrentPrincipal.Identity.IsAuthenticated) ;
        }
    }
}
