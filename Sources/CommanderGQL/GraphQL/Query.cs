using CommanderGQL.Data;
using CommanderGQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CommanderGQL.GraphQL;

public class Query
{
    #region Way01: Constructor Dependency-Injection of AppDbContext
    //private readonly AppDbContext _context;

    //public Query(AppDbContext context)
    //{
    //    _context = context;
    //}
    //public IQueryable<Platform> GetPlatforms()
    //{
    //    return _context.Platforms;
    //}
    #endregion

    #region Way01: Method Dependency-Injection of AppDbContext
    [UseDbContext(typeof(AppDbContext))]
    [UseFiltering]
    [UseSorting]
    //[UseProjection]
    public IQueryable<Platform> GetPlatforms([ScopedService] AppDbContext context)
    {
        return context.Platforms;
    }

    [UseDbContext(typeof(AppDbContext))]
    [UseFiltering]
    [UseSorting]
    //[UseProjection]
    public IQueryable<Command> GetCommands([ScopedService] AppDbContext context)
    {
        return context.Commands;
    }
    #endregion
}
