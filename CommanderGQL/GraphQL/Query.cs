using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL;

public class Query
{
    [UseDbContext(typeof(AppDbContext))]
    [UseProjection]
    public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext context)
    {
        return context.Platforms;
    }
}
