using System.Data.Entity;

namespace  Marajoara.Cinema.Management.Infra.EFManager
{
    /// <summary>
    /// Factory para classes derivadas de DbContext que não podem ser instanciadas pelo construtor Default
    /// </summary>
    public interface IDbContextFactory
    {
        /// <summary>
        /// Método que será usado para que a Factory crie um novo Contexto
        /// </summary>
        /// <typeparam name="TDbContext">Contexto Genérico</typeparam>
        /// <returns>Instancia do contexto desejado</returns>
        TDbContext CreateDbContext<TDbContext>() where TDbContext : DbContext;
    }
}