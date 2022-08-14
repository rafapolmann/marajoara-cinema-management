using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using Marajoara.Cinema.Management.Infra.Framework.IoC;
using System;

namespace Marajoara.Cinema.Management.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var uow = IoC.GetInstance().Get<IMarajoaraUnitOfWork>();

            var all = uow.CineRooms.RetrieveByName("RoomName2");


            all.SeatsRow = 10;

            uow.CineRooms.Delete(all);
            uow.Commit();

            var cineRoom = new CineRoom
            {
                Name = "RoomName",
                SeatsColumn = 10,
                SeatsRow = 5
            };

            uow.CineRooms.Add(cineRoom);
            uow.Commit();

            var cineRoom2 = new CineRoom
            {
                Name = "RoomName2",
                SeatsColumn = 10,
                SeatsRow = 5
            };

            uow.CineRooms.Add(cineRoom2);
            uow.Commit();


            Console.WriteLine("Hello World!");
        }
    }
}
