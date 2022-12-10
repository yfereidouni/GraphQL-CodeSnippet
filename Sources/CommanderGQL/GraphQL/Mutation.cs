using CommanderGQL.Data;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.PLatforms;
using CommanderGQL.Models;
using HotChocolate.Subscriptions;

namespace CommanderGQL.GraphQL;

public class Mutation
{
    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input,
        [ScopedService] AppDbContext context,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        var platform = new Platform
        {
            Name = input.Name
        };

        context.Platforms.Add(platform);

        await context.SaveChangesAsync(cancellationToken);

        ///After save we pass the message for subscribers that platform was added.
        await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded), platform, cancellationToken);
        
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
