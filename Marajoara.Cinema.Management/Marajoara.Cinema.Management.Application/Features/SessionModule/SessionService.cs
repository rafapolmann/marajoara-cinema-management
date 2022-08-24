using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using System.Collections.Generic;

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
            CineRoom sessionCineRoom = _cineRoomService.GetCineRoom(session.CineRoomID);
            //validar se tem sala válida
            //validar se ja não existe sessão na sala no mesmo horario (horario sessão + duração do filme)
            session.CineRoom = sessionCineRoom;

            Movie sessionMovie = _movieService.GetMovie(session.MovieID);
            //validar se tem filme valido
            session.Movie = sessionMovie;

            _unitOfWork.Sessions.Add(session);
            _unitOfWork.Commit();

            return session.SessionID;
        }

        public Session GetSession(int id)
        {
            return _unitOfWork.Sessions.Retrieve(id);
        }

        public IEnumerable<Session> GetAllSessions()
        {
            return _unitOfWork.Sessions.RetrieveAll();
        }
    }
}
