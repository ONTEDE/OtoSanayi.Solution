using OtoSanayi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OtoSanayi.WepApp.Filter
{
    public class AuthAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            Kullanici User = filterContext.HttpContext.Session["login"] as Kullanici;

            if (User == null)
            {
                filterContext.Result = new RedirectResult("/Home/AccessDenied");
            }
        }
    }
}