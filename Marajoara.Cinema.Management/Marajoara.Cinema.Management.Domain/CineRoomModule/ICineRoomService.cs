﻿using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.CineRoomModule
{
    public interface ICineRoomService
    {
        int AddCineRoom(CineRoom cineRoom);
        IEnumerable<CineRoom> RetrieveAll();
        bool RemoveCineRoom(CineRoom cineRoom);
    }
}
