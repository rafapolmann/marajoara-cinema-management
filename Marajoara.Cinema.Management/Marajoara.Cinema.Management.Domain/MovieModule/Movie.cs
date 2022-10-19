using Marajoara.Cinema.Management.Domain.SessionModule;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Domain.MovieModule
{
    public class Movie
    {
        public int MovieID { get; set; }
        public byte[] Poster { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Minutes { get; set; }
        public bool Is3D { get; set; }
        public bool IsOriginalAudio { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Title))
                throw new Exception($"Movie title cannot be null or empty.");

            if (this.Minutes <= 0)
                throw new Exception($"Movie minutes cannot be less or equals zero.");

            return true;
        }

        public void CopyTo(Movie movieToCopy)
        {
            ValidateCopyToArguments(movieToCopy);

            movieToCopy.Title = Title;
            movieToCopy.Description = Description;
            movieToCopy.Minutes = Minutes;
            movieToCopy.Is3D = Is3D;
            movieToCopy.IsOriginalAudio = IsOriginalAudio;
        }

        private void ValidateCopyToArguments(Movie movieToCopy)
        {
            if (movieToCopy == null)
                throw new ArgumentException("Movie parameter cannot be null.", nameof(movieToCopy));
            if (movieToCopy.Equals(this))
                throw new ArgumentException("Movie to copy cannot be the same instance of the origin.", nameof(movieToCopy));
        }
    }
}
