using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using mongodb;
using MongoDB.Driver;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureMongoDb(this IServiceCollection services)
    {
        // Register IMongoClient singleton
        services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoDBSettings>>();
            var settings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);

            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            return new MongoClient(settings);
        });

        return services;
    }
}
