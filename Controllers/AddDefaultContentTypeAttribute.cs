//	Copyright © 2019, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

using System.Threading.Tasks;

namespace JsonBody.Controllers
{
    [System.AttributeUsage (System.AttributeTargets.Method | System.AttributeTargets.Class)]
    public class AddDefaultContentTypeAttribute : System.Attribute, IAsyncResourceFilter
    {
        public AddDefaultContentTypeAttribute(string contentType)
        {
            this.contentType = new StringValues (contentType);
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var request = context.HttpContext.Request;
            
            if (request.Headers.ContainsKey ("Content-Type") == false)
            {
                //  No Content-Type found for this request...
                request.Headers.Add ("Content-Type", this.contentType);
            }

            await next.Invoke ();
        }

        private readonly StringValues contentType;
    }
}
