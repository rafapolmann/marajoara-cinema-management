using System;
using System.IO;

namespace Marajoara.Cinema.Management.Domain.MovieModule
{
    public class Movie
    {
        public int MovieID { get; set; }
        public FileStream Poster { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public bool Is3D { get; set; }
        public bool IsOrignalAudio { get; set; }
    }
}
