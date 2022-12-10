using CommanderGQL.Data;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.PLatforms;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL;

public class Mutation
{
    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input,
        [ScopedService] AppDbContext context)
    {
        var platform = new Platform
        {
            Name = input.Name
        };

        context.Platforms.Add(platform);

        await context.SaveChangesAsync();

        return new AddPlatformPayload(platform);
    }

    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input,
        [ScopedService] AppDbContext context)
    {
        var command = new Command
        {
            CommandLine = input.commandLine,
            HowTo = input.howTo,
            PlatformId = input.platformId
        };

        context.Commands.Add(command);

        await context.SaveChangesAsync();

        return new AddCommandPayload(command);
    }



}
