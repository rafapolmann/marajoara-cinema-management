using System;

namespace  Marajoara.Cinema.Management.Infra.EFManager
{
    /// <summary>
    /// Um DbContextScope "read-only". Usado para operações de Leitura de Dados, sem necessidade de transação
    /// </summary>
    public interface IDbContextReadOnlyScope : IDisposable
    {
        /// <summary>
        /// Coleção de Contextos instanciados pela aplicação.
        /// </summary>
        IDbContextCollection DbContexts { get; }
    }
}