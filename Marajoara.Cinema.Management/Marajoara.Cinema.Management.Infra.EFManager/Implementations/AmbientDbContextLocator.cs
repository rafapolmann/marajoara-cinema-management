using System;
using System.Data.Entity;

namespace  Marajoara.Cinema.Management.Infra.EFManager
{
    public class AmbientDbContextLocator : IAmbientDbContextLocator
    {
        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            var ambientDbContextScope = DbContextScope.GetAmbientScope();
            TDbContext context = ambientDbContextScope == null ? null : ambientDbContextScope.DbContexts.Get<TDbContext>();
            if (context == null)
                throw new InvalidOperationException("No ambient DbContext of type TDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");
            return context;
        }
    }
}