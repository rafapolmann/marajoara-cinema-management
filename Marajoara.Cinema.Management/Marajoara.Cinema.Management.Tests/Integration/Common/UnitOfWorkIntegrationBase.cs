using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.TicketModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Domain.UserAccountModule;
using Marajoara.Cinema.Management.Infra.Data.EF;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace Marajoara.Cinema.Management.Tests.Integration.Common
{
    [TestClass]
    public class UnitOfWorkIntegrationBase
    {
        private static string _connectionString;
        protected static IMarajoaraUnitOfWork _marajoaraUnitOfWork;

        public static IMarajoaraUnitOfWork GetNewEmptyUnitOfWorkInstance(bool recreateContext = true)
        {
            MarajoaraContext context = new MarajoaraContext(GetDbContextOptionsForCurrentRequest());
            if (recreateContext)
            {
                context.Database.EnsureDeleted();
            }           

            context.Database.EnsureCreated();

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

        private static DbContextOptions GetDbContextOptionsForCurrentRequest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MarajoaraContext>();

            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=MarajoaraTestIntegration;Integrated Security=SSPI;");

            var options = optionsBuilder.Options;

            return options;
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
                                           int seatsColumn = 5,
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

        protected Session GetSessionToTest(CineRoom cineRoom,
                                           Movie movie,
                                           DateTime sessionDate,
                                           decimal price = 30)
        {
            return new Session
            {
                SessionDate = sessionDate,
                Price = price,
                CineRoom = cineRoom,
                Movie = movie
            };
        }

        protected Ticket GetTicketToTest(UserAccount userAccount,
                                         Session session,
                                         Guid code,
                                         DateTime purchaseDate,
                                         decimal price = 45,
                                         int seatNumber = 11,
                                         bool used = false)
        {
            return new Ticket
            {
                Used = used,
                SeatNumber = seatNumber,
                PurchaseDate = purchaseDate,
                Price = price,
                Code = code,
                UserAccount = userAccount,
                Session = session
            };
        }
        #endregion HelperMethods
    }
}
