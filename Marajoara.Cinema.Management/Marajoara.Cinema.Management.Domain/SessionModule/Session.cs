﻿using Marajoara.Cinema.Management.Domain.CineRoomModule;
using Marajoara.Cinema.Management.Domain.MovieModule;
using System;

namespace Marajoara.Cinema.Management.Domain.SessionModule
{
    public class Session
    {
        public int SessionID { get; set; }
        public DateTime SessionDate { get; set; }
        public DateTime SessionTime { get; set; }
        public int CineRoomID { get; set; }
        public CineRoom CineRoom { get; set; }
        public int MovieID { get; set; }
        public Movie Movie { get; set; }
        public decimal Price { get; set; }
    }
}