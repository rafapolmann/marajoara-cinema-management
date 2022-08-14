using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity;
using System.Data.SqlClient;


namespace Marajoara.Cinema.Management.Tests.Common
{
    [TestClass]
    public class UnitOfWorkIntegrationBase
    {
        private static string _connectionString;
        protected static IMarajoaraUnitOfWork _marajoaraUnitOfWork;

        public static IMarajoaraUnitOfWork GetNewEmptyUnitOfWorkInstance(bool recreateContext = true)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = "MarajoaraTestIntegration";
            builder.DataSource = "(localdb)\\mssqllocaldb";
            _connectionString = builder.ConnectionString;

            MarajoaraContext context = new MarajoaraContext(new SqlConnection(_connectionString));
            if (recreateContext)
            {
                try
                {
                    //context.Database.ForceDelete();
                }
                catch (SqlException) { /*Could not delete, because database was not createad*/ }

                Database.SetInitializer(new DropCreateDatabaseAlways<MarajoaraContext>());

            }
            else
                Database.SetInitializer<MarajoaraContext>(null);

            context.Database.Initialize(recreateContext);

            ICineRoomRepository cineRoomRepository = new CineRoomRepository(context);
            IMovieRepository movieRepository = new MovieRepository(context);
            ISessionRepository sessionRepository = new SessionRepository(context);
            ITicketRepository ticketRepository = new TicketRepository(context);
            IUserAccountRepository userAccountRepository = new UserAccountRepository(context);

            IMarajoaraUnitOfWork unitOfWork = new MarajoaraUnitOfWork(context, cineRoomRepository,
                                                                      movieRepository, sessionRepository,
                                                                      ticketRepository, userAccountRepository);

            return unitOfWork;
        }

        #region HelperMethods
        protected UserAccount GetUserAccountToTest(string name = "FullName",
                                         string mail = "email",
                                         string password = "P@ssW0rd",
                                         AccessLevel accountLevel = AccessLevel.Manager)
        {
            return new UserAccount
            {
                Name = name,
                Mail = mail,
                Password = password,
                Level = accountLevel
            };
        }

        protected CineRoom GetCineRoomToTest(string name = "CineRoomName",
                                           int seatsColumn = 20,
                                           int seatsRow = 10)
        {
            return new CineRoom
            {
                Name = name,
                SeatsColumn = seatsColumn,
                SeatsRow = seatsRow
            };
        }

        protected Movie GetMovieToTest(string title = "Title",
                                       string description = "Description",
                                       bool is3D = false,
                                       bool isOrignalAudio = false)
        {

            return new Movie
            {
                Title = title,
                Description = description,
                Duration = new TimeSpan(1, 30, 0),
                Is3D = is3D,
                IsOrignalAudio = isOrignalAudio
            };
        }
        #endregion HelperMethods
    }
}
