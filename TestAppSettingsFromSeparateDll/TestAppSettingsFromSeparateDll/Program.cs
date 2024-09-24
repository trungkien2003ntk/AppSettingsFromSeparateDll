using ClassLibForTestAppSettingsFromSeparateDll.Settings;
using ClassLibForTestAppSettingsFromSeparateDll.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using TestAppSettingsFromSeparateDll;


var host = CreateHostBuilder(args).Build();

using (var serviceScope = host.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var testService = services.GetRequiredService<TestService>();
    testService.Run();
}

await host.RunAsync();


static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
            // Load the main project's appsettings.json
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Load the embedded appsettings.json from the DLL
            var embeddedJson = EmbeddedResourceHelper.GetEmbeddedResourceJson("ClassLibForTestAppSettingsFromSeparateDll.appsettings.json");
            var embeddedConfig = new ConfigurationBuilder()
                .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(embeddedJson)))
                .Build();

            config.AddConfiguration(embeddedConfig);
        })
        .ConfigureServices((context, services) =>
        {
            services.Configure<AxSettings>(context.Configuration.GetSection("AxSettings"));
            services.AddTransient<TestService>();
        });