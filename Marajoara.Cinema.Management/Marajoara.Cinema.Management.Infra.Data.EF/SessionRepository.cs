using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Infra.Data.EF.Commom;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marajoara.Cinema.Management.Infra.Data.EF
{
    public class SessionRepository : ISessionRepository
    {
        private readonly MarajoaraContext DBContext;

        public SessionRepository(MarajoaraContext dbContext)
        {
            DBContext = dbContext;
        }

        public void Add(Session sessionToAdd)
        {
            DBContext.Sessions.Add(sessionToAdd);
        }

        public void Delete(Session sessionToDelete)
        {
            DBContext.Entry(sessionToDelete).State = EntityState.Deleted;
        }

        public Session Retrieve(int sessionID)
        {
            return DBContext.Sessions.Include(s => s.Movie)
                                     .Include(s => s.CineRoom)
                                     .Where(s => s.SessionID == sessionID)
                                     .FirstOrDefault();
        }

        public IEnumerable<Session> RetrieveByDate(DateTime sessionDate)
        {
            return DBContext.Sessions.Include(s => s.Movie)
                                     .Include(s => s.CineRoom)
                                     .Where(s => s.SessionDate.Day == sessionDate.Day &&
                                                 s.SessionDate.Month == sessionDate.Month &&
                                                 s.SessionDate.Year == sessionDate.Year);
        }

        public IEnumerable<Session> RetrieveByDate(DateTime initialDate, DateTime finalDate)
        {
            return DBContext.Sessions.Include(s => s.Movie)
                                     .Include(s => s.CineRoom)
                                     .Where(s => s.SessionDate >= initialDate &&
                                                 s.SessionDate <= finalDate);
        }

        public IEnumerable<Session> RetrieveByMovieTitle(string movieTitle)
        {
            return DBContext.Sessions.Include(s => s.Movie)
                                     .Include(s => s.CineRoom)
                                     .Where(s => s.Movie.Title == movieTitle);
        }

        public IEnumerable<Session> RetrieveAll()
        {
            return DBContext.Sessions.Include(s => s.Movie)
                                     .Include(s => s.CineRoom);
        }

        public void Update(Session sessionToUpdate)
        {
            DBContext.Entry(sessionToUpdate).State = EntityState.Modified;
        }

        public IEnumerable<Session> RetrieveByCineRoom(CineRoom cineRoom)
        {
            return DBContext.Sessions.Include(s => s.Movie)
                                     .Include(s => s.CineRoom)
                                     .Where(s => s.CineRoomID == cineRoom.CineRoomID);
        }

        public IEnumerable<Session> RetrieveByDateAndCineRoom(DateTime sessionDate, int cineRoomID)
        {
            return DBContext.Sessions.Include(s => s.Movie)
                                     .Include(s => s.CineRoom)
                                     .Where(s => s.CineRoomID == cineRoomID &&
                                                 s.SessionDate.Day == sessionDate.Day &&
                                                 s.SessionDate.Month == sessionDate.Month &&
                                                 s.SessionDate.Year == sessionDate.Year);
        }
    }
}
