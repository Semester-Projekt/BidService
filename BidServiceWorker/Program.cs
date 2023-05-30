using System.Text;
using BidServiceWorker;
using NLog;
using NLog.Web;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

//Indlæs NLog.config-konfigurationsfil
var logger =
NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try // try/catch/finally fra m10.01 opgave b step 4
{


    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })

    .UseNLog() //Brug NLog

    .Build();

    host.Run();

}

catch (Exception ex)
{
    logger.Error(ex, "Stopped program because of exception");
    throw;
}

finally
{
    NLog.LogManager.Shutdown();
}