using HotChocolatePOC;
using James.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

//var config = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json")
//    .AddEnvironmentVariables()
//    .Build();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<BankMutationType>()
    .AddSubscriptionType<Subscription>()
    .AddMutationConventions()
    .AddInMemorySubscriptions()
    ;
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        //policy.WithOrigins("https://localhost:7265/");
    });
});

var app = builder.Build();

app.UseCors();
app.MapGraphQL("/graphql");
app.MapGet("/HelloWorld", () => "Hello World!");
//app.MapGet("/", () => "Go to /graphql");
app.MapGet("/", req =>
{
    req.Response.Redirect("/graphql");
    return Task.FromResult("Go to /graphql");
});
//For subscriptions
//app.UseRouting();
//app.UseWebSockets();
//app.UseEndpoints(endpoints =>
//{
//    _ = endpoints.MapGraphQL();
//});

app.Run();
