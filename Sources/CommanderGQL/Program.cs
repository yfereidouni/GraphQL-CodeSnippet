using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.PLatforms;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//For Parallel queries using "AddPooledDbContextFactory"
builder.Services.AddPooledDbContextFactory<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("CommanderConStr"));
});

// Confinguring GraphQL to pipeline ----------
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddFiltering()
    .AddSorting();
    //.AddProjections();
//--------------------------------------------

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Configuring GraphQL endpoint ---------------
//http://localhost:5000/graphql/
//https://localhost:5001/graphql/
app.MapGraphQL();

//http://localhost:5000/graphql-voyager/
//https://localhost:5001/graphql-voyager/
app.MapGraphQLVoyager("/graphql-voyager");
//---------------------------------------------

app.Run();
