﻿//	Copyright © 2019, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace JsonBody.Middlewares
{
    public class AddDefaultContentTypeMiddleware
    {
        public AddDefaultContentTypeMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            if ((context.Request.Method == "POST") &&
                (context.Request.Headers.ContainsKey ("Content-Type") == false))
            {
                //  No Content-Type found for this request...
                context.Request.Headers.Add ("Content-Type", new Microsoft.Extensions.Primitives.StringValues ("application/json"));
            }

            await this.next (context);
        }
        
        private readonly RequestDelegate next;
    }
}
