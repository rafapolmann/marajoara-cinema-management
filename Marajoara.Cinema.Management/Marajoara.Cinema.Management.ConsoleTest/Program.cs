using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Infra.Framework.IoC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marajoara.Cinema.Management.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            GuidTest();
            // var a = default(DateTime).ToString();
            //var uow = IoC.GetInstance().Get<IMarajoaraUnitOfWork>();

            //var allRooms = uow.CineRooms.RetrieveAll().ToList();
            //var allMovies = uow.Movies.RetrieveAll();
            //var allSessions = uow.Sessions.RetrieveAll().ToList();

            //DateTime dt = DateTime.Parse("26/08/2022 18:58:48");
            //DateTime dtFinal = dt.AddHours(1);
            //var session = GetSessionToTest(uow.CineRooms.Retrieve(1), uow.Movies.Retrieve(6), dt);

            //List<Session> sessionInDateToCineRoom = uow.Sessions.RetrieveByDateAndCineRoom(session.SessionDate, session.CineRoomID)
            //                                                    .Where(s => s.SessionDate <= session.SessionDate && s.EndSession >= session.SessionDate ||
            //                                                                s.SessionDate <= session.EndSession && s.EndSession >= session.EndSession).ToList();

            //Random rnd = new Random();
            //foreach (var movie in allMovies)
            //{
            //    Session s = GetSessionToTest(allRooms[rnd.Next(0, allRooms.Count - 1)],
            //                                movie,
            //                                DateTime.Now.AddDays(rnd.Next(1, 10)),
            //                                Convert.ToDecimal(rnd.Next(15, 50)));
            //    uow.Sessions.Add(s);
            //}

            //uow.Commit();

            
        }

        private static void GuidTest()
        {
            Console.WriteLine(new Guid());
            Console.ReadKey();
        }

        protected static Session GetSessionToTest(CineRoom cineRoom,
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
    }
}
