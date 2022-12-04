using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommanderConStr"));
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddFiltering()
    .AddSorting()
    //.AddProjections()
    .AddInMemorySubscriptions();


var app = builder.Build();

app.UseWebSockets();

app.MapGet("/", () => "Use this links: \r\n\r\nhttps://localhost:7252/graphql\r\nhttps://localhost:7252/graphql-voyager");
app.MapGraphQL("/graphql");// Checkout Result -> https://localhost:7252/graphql/
app.MapGraphQLVoyager("/graphql-voyager");



app.Run();
