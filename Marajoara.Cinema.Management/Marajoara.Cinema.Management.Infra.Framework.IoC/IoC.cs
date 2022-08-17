using Marajoara.Cinema.Management.Application;
using Marajoara.Cinema.Management.Application.Features.CineRoom;
using Marajoara.Cinema.Management.Application.Features.CineRoom.Commands;
using Marajoara.Cinema.Management.Application.Features.CineRoom.Handlers;
using Marajoara.Cinema.Management.Application.Features.CineRoom.Models;
using Marajoara.Cinema.Management.Application.Features.CineRoom.Queries;
using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using MediatR;
using MediatR.Ninject;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Marajoara.Cinema.Management.Infra.Framework.IoC
{
    public class IoC
    {
        private static readonly object obj = new object();
        private static readonly IKernel _kernel = new StandardKernel();
        private static IoC _instance;

        public IKernel Kernel { get { return _kernel; } }

        private IoC()
        {
            DatabaseSetup();
            RepositoriesSetup();
            ApplicationSetup();
            MediatR();
        }

        private void ApplicationSetup()
        {
            _kernel.Bind<IUserAccountService>().To<UserAccountService>();
            _kernel.Bind<ICineRoomService>().To<CineRoomService>();
        }

        private void RepositoriesSetup()
        {
            _kernel.Bind<ICineRoomRepository>().To<CineRoomRepository>();
            _kernel.Bind<IMovieRepository>().To<MovieRepository>();
            _kernel.Bind<ISessionRepository>().To<SessionRepository>();
            _kernel.Bind<ITicketRepository>().To<TicketRepository>();
            _kernel.Bind<IUserAccountRepository>().To<UserAccountRepository>();            
        }
        private void MediatR()
        {
            _kernel.BindMediatR();
            _kernel.Bind<IRequestHandler<AllCineRoomsQuery, Result<Exception, List<CineRoomModel>>>>().To<AllCineRoomsHandler>();
            _kernel.Bind<IRequestHandler<AddCineRoomCommand, Result<Exception, int>>>().To<AddCineRoomHandler>();
            _kernel.Bind<IRequestHandler<DeleteCineRoomCommand, Result<Exception, bool>>>().To<DeleteCineRoomHandler>();
        }

        private void DatabaseSetup()
        {
            SqlConnectionStringBuilder _connectionStringBuilder = new SqlConnectionStringBuilder
            {
                InitialCatalog = "CineMarajoara",
                DataSource = "(localdb)\\mssqllocaldb"
            };

            _kernel.Bind<MarajoaraContext>().ToSelf()
                                            .InSingletonScope()
                                            .WithConstructorArgument("dbConnection", new SqlConnection(_connectionStringBuilder.ConnectionString));

            _kernel.Bind<IMarajoaraUnitOfWork>().To<MarajoaraUnitOfWork>();
        }

        public static IoC GetInstance()
        {
            lock (obj)
            {
                if (_instance == null)
                    _instance = new IoC();

                return _instance;
            }
        }

        public T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
