using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.SessionModule
{
    public interface ISessionService
    {
        IEnumerable<Session> GetAllSessions();
    }
}
