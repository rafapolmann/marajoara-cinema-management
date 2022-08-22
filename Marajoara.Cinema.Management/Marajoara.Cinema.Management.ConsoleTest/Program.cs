using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Infra.Framework.IoC;
using System;
using System.Linq;

namespace Marajoara.Cinema.Management.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var uow = IoC.GetInstance().Get<IMarajoaraUnitOfWork>();

            var allRooms = uow.CineRooms.RetrieveAll().ToList();
            var allMovies = uow.Movies.RetrieveAll();


            Random rnd = new Random();
            foreach (var movie in allMovies)
            {
                Session s = GetSessionToTest(allRooms[rnd.Next(0, allRooms.Count - 1)],
                                            movie, 
                                            DateTime.Now.AddDays(rnd.Next(1, 10)), 
                                            Convert.ToDecimal(rnd.Next(15, 50)));
                uow.Sessions.Add(s);
            }

            uow.Commit();

            Console.WriteLine("Hello World!");
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
