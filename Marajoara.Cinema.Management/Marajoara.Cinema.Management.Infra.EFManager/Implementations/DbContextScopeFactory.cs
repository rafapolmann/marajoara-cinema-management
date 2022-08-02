using System;
using System.Data;

namespace  Marajoara.Cinema.Management.Infra.EFManager
{
    /// <summary>
    /// Implementa os métodos para gerenciar um novo ambiente contido em um DbContextScope.
    /// </summary>
    public class DbContextScopeFactory : IDbContextScopeFactory
    {
        private readonly IDbContextFactory _dbContextFactory;

        public DbContextScopeFactory(IDbContextFactory dbContextFactory = null)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <summary>
        /// Cria um novo DbContext Scope
        ///
        /// Por padrão, o novo escopo se unirá ao escopo de ambiente existente.
        /// Isso é o que você quer na maioria dos casos. Isso garante que as mesmas instâncias DbContext
        /// sejam usadas por todos os métodos de serviços chamados dentro do escopo de uma transação.
        ///
        /// Se definir 'joiningOption' para 'ForceCreateNew' você quer ignorar o escopo ambiente atual
        /// e forçar a criação de novas instâncias DbContext dentro desse escopo. Usando 'ForceCreateNew'
        /// é um recurso avançado que deve ser usado com muito cuidado e somente se você entender as
        /// implicações de fazer isso.
        /// </summary>
        /// <param name="joiningOption"></param>
        /// <returns></returns>
        public IDbContextScope Create(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            return new DbContextScope(
                joiningOption: joiningOption,
                readOnly: false,
                isolationLevel: null,
                dbContextFactory: _dbContextFactory);
        }

        /// <summary>
        /// Cria um novo DbContextScope para queries "read-only".
        ///
        /// Segue a mesma lógica do método Create, porém, marca o DbContextScope como "read-only"
        /// Isso implicará na abstenção do SaveChanges() do DbContext, pois ações do tipo Query não necessitam disso.
        /// </summary>
        public IDbContextReadOnlyScope CreateReadOnly(DbContextScopeOption joiningOption = DbContextScopeOption.JoinExisting)
        {
            return new DbContextReadOnlyScope(
                joiningOption: joiningOption,
                isolationLevel: null,
                dbContextFactory: _dbContextFactory);
        }

        /// <summary>
        /// Força a criação de um novo ambiente DbContextScope (ou seja, não utiliza um ambiente existente)
        /// Além disso, envolve todas as instâncias do DbContext criado, dentro desse escopo em uma transação de banco de dados explícita
        /// com o IsolationLevel fornecido.
        ///
        /// ATENÇÃO: a transação do banco de dados permanecerá aberta durante toda a duração do escopo
        /// Portanto, mantenha o escopo o menor possível.
        /// Não faça chamadas para API remotas ou execute qualquer RPC de longa duração dentro desse escopo.
        ///
        /// Este é um recurso avançado que você deve usar com muito cuidado
        /// e somente se você entender completamente as implicações de fazer isso.
        /// </summary>
        public IDbContextScope CreateWithTransaction(IsolationLevel isolationLevel)
        {
            return new DbContextScope(
                joiningOption: DbContextScopeOption.ForceCreateNew,
                readOnly: false,
                isolationLevel: isolationLevel,
                dbContextFactory: _dbContextFactory);
        }

        /// <summary>
        /// Força a criação de um novo ambiente DbContextScope, porém para queries "read-only".
        /// Além disso, envolve todas as instâncias do DbContext criado, dentro desse escopo em uma transação de banco de dados explícita
        /// com o IsolationLevel fornecido.
        ///
        /// Mas porque criar uma transação para fazer queries no Banco de Dados?
        /// Se isso lhe soa estranho é porque você só aprendeu o básico de transações SQL (Commit e Rollback).
        /// Por padrão as conexões com o SQL são abertas como READ COMMITTED. Nesse caso, só vamos conseguir ler o que foi processado após a execução do COMMIT.
        /// Passando o IsolationLevel como READ UNCOMMITTED a conexão será aberta sem que o SQL dê um LOCK na tabela de leitura
        /// Com isso é possível ler os dados de uma tabela, mesmo ela sendo utilizada dentro de uma transação longa, que executa vários processos (INSERTS, UPDATES e DELETES).
        /// Esse cenário é raro e nem deve acontecer, somente em casos extremos.
        /// Quando isso acontece, chamamos de leitura suja (Dirty Read)
        /// </summary>
        public IDbContextReadOnlyScope CreateReadOnlyWithTransaction(IsolationLevel isolationLevel)
        {
            return new DbContextReadOnlyScope(
                joiningOption: DbContextScopeOption.ForceCreateNew,
                isolationLevel: isolationLevel,
                dbContextFactory: _dbContextFactory);
        }

        /// <summary>
        /// Elimina temporariamente um DbContextScope do ambiente.
        ///
        /// Sempre use isso se você precisar iniciar tarefas paralelas dentro de um DbContextScope.
        /// Isso impedirá que as tarefas paralelas usem o escopo de ambiente atual.
        /// Se você iniciar tarefas paralelas dentro de um DbContextScope sem suprimir o DbContextScope do ambiente,
        /// todas as tarefas paralelas vão acabar usando o mesmo ambiente DbContextScope,
        /// que resultaria em múltiplas threads acessando as mesmas instâncias de DbContext ao mesmo tempo(CRASH).
        /// </summary>
        /// <returns></returns>
        public IDisposable SuppressAmbientContext()
        {
            return new AmbientContextSuppressor();
        }
    }
}