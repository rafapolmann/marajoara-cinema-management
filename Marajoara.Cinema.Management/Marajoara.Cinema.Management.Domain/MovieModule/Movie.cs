﻿using System;

namespace Marajoara.Cinema.Management.Domain.MovieModule
{
    public class Movie
    {
        public int MovieID { get; set; }
        public byte[] Poster { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Is3D { get; set; }
        public bool IsOrignalAudio { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
                throw new Exception($"Movie title cannot be null or empty.");

            return true;
        }
    }
}
