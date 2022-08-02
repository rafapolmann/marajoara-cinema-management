using System;
using System.Data.Entity;

namespace  Marajoara.Cinema.Management.Infra.EFManager
{
    /// <summary>
    /// Mantém uma lista de instancias de DbContext.
    /// </summary>
    public interface IDbContextCollection : IDisposable
    {
        /// <summary>
        /// Busca ou cria uma instancia de DbContext pelo seu tipo.
        /// </summary>
		TDbContext Get<TDbContext>() where TDbContext : DbContext;
    }
}