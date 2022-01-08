using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TesteBackendEnContact.Database;

namespace TesteBackendEnContact
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            // ele preenche tudo aqui... entendi mas nesse caso é a gente que tava chamando antes kkk
            /*eu nem me toquei, que tava chamando antes da chamada do startup*/
            CreateHostBuilder(args).Build().Run();
            // de boa ta compilando aqui ainda
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
