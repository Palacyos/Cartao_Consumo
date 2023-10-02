using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;

namespace SESCAP.Ecommerce.Libraries.Midleware
{
    public class ValidateAntiForgeryTokenMiddleware
    {

        private RequestDelegate _next;
        private IAntiforgery _antiforgery;



        public ValidateAntiForgeryTokenMiddleware( RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;

        }


        public async Task Invoke(HttpContext context)
        {

            if (HttpMethods.IsPost(context.Request.Method))
            {
                await _antiforgery.ValidateRequestAsync(context);
            }

            await _next(context);
        }
    }
}
