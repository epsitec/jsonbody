//	Copyright © 2019, EPSITEC SA, CH-1400 Yverdon-les-Bains, Switzerland
//	Author: Pierre ARNAUD, Maintainer: Pierre ARNAUD

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace JsonBody
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder (args).Build ().Run ();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder (args)
                .ConfigureWebHostDefaults (webBuilder =>
                {
                    webBuilder.UseStartup<Startup> ();
                    webBuilder.ConfigureKestrel ((context, options) => options.ListenLocalhost (5000));
                });
    }
}
