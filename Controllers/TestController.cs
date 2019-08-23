//	Copyright © 2019, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace JsonBody.Controllers
{
    [ApiController]
    [Route ("test")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route ("hello")]
        public IActionResult Hello()
        {
            return this.Ok ("Hello, world.");
        }

        [HttpPost]
        [Route ("demo")]
        public IActionResult Demo([FromBody] JObject x)
        {
            System.Console.WriteLine (x.ToString ());
            return this.Ok ();
        }

        [HttpPost]
        [Route ("raw")]
        public IActionResult Raw()
        {
            foreach (var header in this.Request.Headers)
            {
                System.Console.WriteLine ($"{header.Key}: {header.Value}");
            }
            return this.Ok ();
        }
    }
}
