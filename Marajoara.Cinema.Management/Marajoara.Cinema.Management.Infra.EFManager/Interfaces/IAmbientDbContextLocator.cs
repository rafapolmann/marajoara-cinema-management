namespace  Marajoara.Cinema.Management.Infra.EFManager
{
    /// <summary>
    /// Método utilizado para retornar instancias do DbContext no ambiente.
    /// </summary>
    public interface IAmbientDbContextLocator
    {
        /// <summary>
        /// Busca um DbContext baseado no seu tipo, se não encontrar retorna null.
        /// </summary>
        TDbContext Get<TDbContext>() where TDbContext : System.Data.Entity.DbContext;
    }
}