using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using SESCAP.Ecommerce.Libraries.Login;

namespace SESCAP.Ecommerce.Libraries.Filtros
{
    public class ClientelaAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        LoginClientela loginClientela;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            loginClientela = (LoginClientela)context.HttpContext.RequestServices.GetService(typeof(LoginClientela));

            var clientela = loginClientela.Obter();

            if (clientela == null)
            {
                context.Result = new RedirectResult("Login");
            }
        }
    }
}
