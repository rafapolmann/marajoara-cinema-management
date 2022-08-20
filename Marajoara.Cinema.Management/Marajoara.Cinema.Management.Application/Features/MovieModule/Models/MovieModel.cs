﻿using System;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Models
{
    public class MovieModel
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Is3D { get; set; }
        public bool IsOrignalAudio { get; set; }
    }
}
