using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MvcWebRole1.Entities
{
    public class Identity : IIdentity
    {
        public Identity(string name, bool isAuthenticated)
        {
            Name = name;
            IsAuthenticated = isAuthenticated;
        }

        public string AuthenticationType
        {
            get { return "HSDemo"; }
        }

        public bool IsAuthenticated
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Password 
        { 
            get; 
            set; 
        }

        public bool Authenticate()
        {
            return true;
        }
    }
}