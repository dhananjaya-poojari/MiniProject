using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TinyUrl.Data;
using TinyUrl.Service;
using TinyUrl.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection(nameof(MongoDBSettings)));
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(s.GetRequiredService<IOptions<MongoDBSettings>>().Value.ConnectionString));

// Add services to the container.
builder.Services.AddSingleton<MongoDBSettings>(sp => sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
builder.Services.AddScoped<ITinyService, TinyService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    var mongoClient = app.Services.GetRequiredService<IMongoClient>();
    var result = mongoClient.GetDatabase("UrlShortner").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
    Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

app.Run();