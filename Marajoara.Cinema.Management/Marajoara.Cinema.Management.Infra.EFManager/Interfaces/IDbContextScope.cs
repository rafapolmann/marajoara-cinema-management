using System;
using System.Collections;

namespace  Marajoara.Cinema.Management.Infra.EFManager
{
    /// <summary>
    /// Cria e gerencia as instancias de DbContext usandas nesse "coding block".
    /// </summary>
    public interface IDbContextScope : IDisposable
    {
        /// <summary>
        /// Método usado para aplicar o SaveChanges nos contextos que estão dentro do Scope
        /// </summary>
        /// <returns></returns>

        int SaveChanges();

        ///// <summary>
        ///// Método Assíncrono usado para aplicar o SaveChanges nos contextos que estão dentro do Scope
        ///// </summary>
        ///// <returns></returns>
        //Task<int> SaveChangesAsync();

        ///// <summary>
        ///// Método Assíncrono, com possibilidade de cancelamento, usado para aplicar o SaveChanges nos contextos que estão dentro do Scope
        ///// </summary>
        ///// <returns></returns>
        //Task<int> SaveChangesAsync(CancellationToken cancelToken);

        /// <summary>
        /// Método usado para atualizar as entidades dos Contextos que pertencem a outro Scope, em uma operação aninhada
        /// </summary>
        /// <returns></returns>
        void RefreshEntitiesInParentScope(IEnumerable entities);

        ///// <summary>
        ///// Método Assíncrono, usado para atualizar as entidades dos Contextos que pertencem a outro Scope, em uma operação aninhada
        ///// </summary>
        ///// <returns></returns>
        //Task RefreshEntitiesInParentScopeAsync(IEnumerable entities);

        /// <summary>
        /// Coleção de instâncias de Contextos
        /// </summary>
        /// <returns></returns>
        IDbContextCollection DbContexts { get; }
    }
}