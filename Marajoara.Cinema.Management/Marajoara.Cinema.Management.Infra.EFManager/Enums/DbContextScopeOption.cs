namespace  Marajoara.Cinema.Management.Infra.EFManager
{
    public enum DbContextScopeOption
    {
        /// <summary>
        /// Se existir um DbContextScope faz a junção com o existente. Caso contrário, cria um novo.
        /// </summary>
        JoinExisting,

        /// <summary>
        /// Força a criação de um novo DbContextScope, ignorado a existência de um outro já existente.
        /// </summary>
        ForceCreateNew
    }
}