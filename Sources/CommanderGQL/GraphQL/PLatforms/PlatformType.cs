using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate.Execution;

namespace CommanderGQL.GraphQL.PLatforms;

public class PlatformType : ObjectType<Platform>
{
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Description("YF-> Represents any software or service that has a command line interface!");

        /// For ignoring a field from showing and using
        //descriptor.Field(p => p.LicenseKey).Ignore();

        descriptor.Field(p => p.Commands)
            .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("YF-> This is the list f available commands for this platform");
    }

    private class Resolvers
    {
        public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] AppDbContext context)
        {
            return context.Commands.Where(p => p.PlatformId == platform.Id);
        }
    }
}