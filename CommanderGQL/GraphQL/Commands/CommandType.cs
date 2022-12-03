using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL.Commands;

public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represent any executable command");

        descriptor
            .Field(p => p.Platform)
            .ResolveWith<Resolvers>(p => p.GetPlatform(default, default))
            .UseDbContext<AppDbContext>()
            .Description("This is the platform to which the command belongs");


        //base.Configure(descriptor);
    }

    private class Resolvers
    {
        public IQueryable<Platform> GetPlatform(Command command, [ScopedService] AppDbContext context)
        {
            return context.Platforms.Where(p => p.Id == command.PlatformId);
        }
    }
}
