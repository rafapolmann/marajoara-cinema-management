using System;
using System.Data;

namespace  Marajoara.Cinema.Management.Infra.EFManager
{
    /// <summary>
    /// Expoe os métodos para gerenciar um novo ambiente contido em um DbContextScope.
    /// </summary>
    public interface IDbContextScopeFactory
    {
        /// <summary>
        /// Cria um novo DbContextScope.
        /// </summary>
        IDbContextScope Create(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting);

        /// <summary>
        /// Cria um novo DbContextScope para queries "read-only".
        /// </summary>
        IDbContextReadOnlyScope CreateReadOnly(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting);

        /// <summary>
        /// Força a criação de um novo DbContextScope no ambiente, envolvendo em um escopo de transação
        /// </summary>
        IDbContextScope CreateWithTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Força a criação de um novo DbContextScope "read-only" no ambiente, envolvendo em um escopo de transação
        /// </summary>
        IDbContextReadOnlyScope CreateReadOnlyWithTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Elimina temporariamente um DbContextScope do ambiente.
        /// </summary>
        IDisposable SuppressAmbientContext();
    }
}