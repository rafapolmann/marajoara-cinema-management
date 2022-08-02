using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace  Marajoara.Cinema.Management.Infra.EFManager
{
    /// <summary>
    /// Como o nome sugere, DbContextCollection mantém uma coleção de instancias de DbContext.
    /// </summary>
    public class DbContextCollection : IDbContextCollection
    {
        private readonly Dictionary<Type, DbContext> _initializedDbContexts;
        private readonly Dictionary<DbContext, DbContextTransaction> _transactions;
        private readonly IsolationLevel? _isolationLevel;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly bool _readOnly;
        private bool _disposed;
        private bool _completed;

        internal Dictionary<Type, DbContext> InitializedDbContexts { get { return _initializedDbContexts; } }

        public DbContextCollection(bool readOnly = false, IsolationLevel? isolationLevel = null, IDbContextFactory dbContextFactory = null)
        {
            _disposed = false;
            _completed = false;

            _initializedDbContexts = new Dictionary<Type, DbContext>();
            _transactions = new Dictionary<DbContext, DbContextTransaction>();

            _readOnly = readOnly;
            _isolationLevel = isolationLevel;
            _dbContextFactory = dbContextFactory;
        }

        public TDbContext Get<TDbContext>() where TDbContext : DbContext
        {
            if (_disposed)
                throw new ObjectDisposedException("DbContextCollection");

            var requestedType = typeof(TDbContext);

            if (!_initializedDbContexts.ContainsKey(requestedType))
            {
                // Verifica se existe um DbContextFactory customizado para o projeto, senão usa Reflection.
                // DbContextFactory customizados permitem que sejam criadas Contexto com connectionStrings dinâmicas.
                var dbContext = _dbContextFactory != null
                    ? _dbContextFactory.CreateDbContext<TDbContext>()
                    : Activator.CreateInstance<TDbContext>();

                _initializedDbContexts.Add(requestedType, dbContext);

                if (_readOnly)
                {
                    dbContext.Configuration.AutoDetectChangesEnabled = false;
                }

                if (_isolationLevel.HasValue)
                {
                    var tran = dbContext.Database.BeginTransaction(_isolationLevel.Value);
                    _transactions.Add(dbContext, tran);
                }
            }

            return _initializedDbContexts[requestedType] as TDbContext;
        }

        public int Commit()
        {
            if (_disposed)
                throw new ObjectDisposedException("DbContextCollection");
            if (_completed)
                throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");

            // Com muito esforço, você vai notar que não estamos realmente implementando um commit atômico aqui.
            // É inteiramente possível que uma instância DbContext será confirmada com êxito
            // e outra irá falhar. A implementação de uma submissão atomica nos obrigaria a embrulhar
            // Tudo isso em um TransactionScope. O problema é que com TransactionScope
            // a base de dados da transação criada, pode ser promovida automaticamente para uma
            // transação distribuída se os nossos DbContext acontecerem de estar usando diferentes
            // bancos de dados. E isso exige que o serviço DTC (Distributed Transaction Coordinator)
            // Para ser ativado em todos os nossos servidores ao Prod e Dev, bem como em todas as nossas estações de trabalho Dev.
            // Caso contrário, a coisa toda iria explodir em tempo de execução.

            // Na prática, se os nossos serviços são implementados seguindo uma abordagem razoavelmente DDD,
            // Uma transação comercial (isto é, um método de serviço) só deve modificar as entidades em um único
            // DbContext. Assim, nunca devemos nos encontrar em uma situação onde duas instâncias DbContext
            // Contêm alterações não confirmadas aqui. Devemos, portanto, nunca estar em uma situação em que o abaixo
            // Resultaria em uma parcial Commit.

            var c = 0;

            foreach (var dbContext in _initializedDbContexts.Values)
            {
                if (!_readOnly)
                {
                    c += dbContext.SaveChanges();
                }

                // Se existe uma transação explícita, é hora de fazer Commit
                var tran = GetValueOrDefault(_transactions, dbContext);
                if (tran != null)
                {
                    tran.Commit();
                    tran.Dispose();
                }
            }

            _transactions.Clear();
            _completed = true;

            return c;
        }

        //public Task<int> CommitAsync()
        //{
        //    return CommitAsync(CancellationToken.None);
        //}

        //public async Task<int> CommitAsync(CancellationToken cancelToken)
        //{
        //    if (cancelToken == null)
        //        throw new ArgumentNullException("cancelToken");
        //    if (_disposed)
        //        throw new ObjectDisposedException("DbContextCollection");
        //    if (_completed)
        //        throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");


        //    var c = 0;

        //    foreach (var dbContext in _initializedDbContexts.Values)
        //    {
        //        if (!_readOnly)
        //        {
        //            c += dbContext.SaveChanges();
        //        }

        //        // Se existe uma transação explícita, é hora de fazer Commit
        //        var tran = GetValueOrDefault(_transactions, dbContext);
        //        if (tran != null)
        //        {
        //            tran.Commit();
        //            tran.Dispose();
        //        }

        //    }

        //    _transactions.Clear();
        //    _completed = true;

        //    return c;
        //}

        public void Rollback()
        {
            if (_disposed)
                throw new ObjectDisposedException("DbContextCollection");
            if (_completed)
                throw new InvalidOperationException("You can't call Commit() or Rollback() more than once on a DbContextCollection. All the changes in the DbContext instances managed by this collection have already been saved or rollback and all database transactions have been completed and closed. If you wish to make more data changes, create a new DbContextCollection and make your changes there.");

            foreach (var dbContext in _initializedDbContexts.Values)
            {
                // Se existe uma transação explícita, é hora de fazer Commit
                var tran = GetValueOrDefault(_transactions, dbContext);
                if (tran != null)
                {
                    tran.Rollback();
                    tran.Dispose();
                }
            }

            _transactions.Clear();
            _completed = true;
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            // Faz Commit () ou Rollback () em primeiro lugar se conseguir executar sem erro

            if (!_completed)
            {
                try
                {
                    if (_readOnly) Commit();
                    else Rollback();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }

            foreach (var dbContext in _initializedDbContexts.Values)
            {
                try
                {
                    dbContext.Dispose();
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }
            }

            _initializedDbContexts.Clear();
            _disposed = true;
        }

        /// <summary>
        /// Retorna o valor associado com a chave especificada ou o valor padrão
        /// </summary>
        private static TValue GetValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }
    }
}