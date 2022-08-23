using Marajoara.Cinema.Management.Domain.SessionModule;
using Marajoara.Cinema.Management.Domain.UnitOfWork;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule
{
    public class SessionService : ISessionService
    {
        private readonly IMarajoaraUnitOfWork _unitOfWork;
        public SessionService(IMarajoaraUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
