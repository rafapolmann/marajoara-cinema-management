using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule
{
    public class SessionService : ISessionService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;
        private readonly ICineRoomService _cineRoomService;
        private readonly IMovieService _movieService;
        public SessionService(IMarajoaraUnitOfWork unitOfWork, ICineRoomService cineRoomService, IMovieService movieService)
        {
            _unitOfWork = unitOfWork;
            _cineRoomService = cineRoomService;
            _movieService = movieService;
        }

        public int AddSession(Session session)
        {
            session = GetValidatedSession(session);

            _unitOfWork.Sessions.Add(session);
            _unitOfWork.Commit();

            return session.SessionID;
        }

        public bool UpdateSession(Session session)
        {
            Session sessionOnDB = _unitOfWork.Sessions.Retrieve(session.SessionID);
            if (sessionOnDB == null)
                throw new Exception($"Session to update not found.");

            session = GetValidatedSession(session);
            session.CopyTo(sessionOnDB);

            _unitOfWork.Sessions.Update(sessionOnDB);
            _unitOfWork.Commit();

            return true;
        }

        public bool RemoveSession(Session session)
        {
            if (session == null)
                throw new ArgumentException("Session parameter cannot be null.", nameof(session));

            Session sessionToDelete = _unitOfWork.Sessions.Retrieve(session.SessionID);

            if (sessionToDelete == null)
                throw new Exception($"Session not found.");
  
            _unitOfWork.Sessions.Delete(sessionToDelete);
            _unitOfWork.Commit();

            return true;
        }

        public Session GetSession(int id)
        {
            return _unitOfWork.Sessions.Retrieve(id);
        }

        public IEnumerable<Session> GetAllSessions()
        {
            return _unitOfWork.Sessions.RetrieveAll();
        }

        public IEnumerable<Session> GetSessionsByCineRoom(int cineRoomID)
        {
            CineRoom cineRoomToSearch = _unitOfWork.CineRooms.Retrieve(cineRoomID);
            if (cineRoomToSearch == null)
                throw new Exception($"Cine room not found. CineRoomID: {cineRoomID}");

            return _unitOfWork.Sessions.RetrieveByCineRoom(cineRoomToSearch);
        }

        public IEnumerable<Session> GetSessionsByMovieTitle(string movieTitle)
        {
            return _unitOfWork.Sessions.RetrieveByMovieTitle(movieTitle);
        }

        public IEnumerable<Session> GetSessionsByDate(DateTime dateTime)
        {
            return _unitOfWork.Sessions.RetrieveByDate(dateTime);
        }

        public IEnumerable<Session> GetSessionsByDateRange(DateTime initialDate, DateTime finalDate)
        {
            return _unitOfWork.Sessions.RetrieveByDate(initialDate, finalDate);
        }

        private Session GetValidatedSession(Session session)
        {
            if (session == null)
                throw new ArgumentException("Session parameter cannot be null.", nameof(session));

            CineRoom sessionCineRoom = _cineRoomService.GetCineRoom(session.CineRoomID);
            if (sessionCineRoom == null)
                throw new Exception($"Cine Room not found. CineRoomID: {session.CineRoomID}");

            session.CineRoom = sessionCineRoom;

            Movie sessionMovie = _movieService.GetMovie(session.MovieID);
            if (sessionMovie == null)
                throw new Exception($"Movie not found. MovieID: {session.MovieID}");

            session.Movie = sessionMovie;

            if (GetSessionsInTheSameSessionRangeTime(session).Any())
                throw new Exception($"Already exists other session in the {session.CineRoom.Name} " +
                                    $"at {session.SessionDate.ToString("HH:mm:ss")}" +
                                    $" - {session.EndSession.ToString("HH:mm:ss")}");

            return session;
        }

        private IEnumerable<Session> GetSessionsInTheSameSessionRangeTime(Session session)
        {
            return _unitOfWork.Sessions.RetrieveByDateAndCineRoom(session.SessionDate, session.CineRoomID)
                                       .Where(s => !s.SessionID.Equals(session.SessionID) &&
                                                   (s.SessionDate <= session.SessionDate && s.EndSession >= session.SessionDate ||
                                                    s.SessionDate <= session.EndSession && s.EndSession >= session.EndSession));
        }
    }
}
//!s.SessionID.Equals(session.SessionID) &&

/*
 
 {
  "sessionID": 2,
  "sessionDate": "2022-08-30T18:57:48.88",
  "price": 45,
  "cineRoomID": 12,
  "movieID": 2
}*/