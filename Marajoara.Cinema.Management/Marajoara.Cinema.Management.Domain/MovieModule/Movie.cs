using System;

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

        public void CopyTo(Movie movieToCopy)
        {
            ValidateCopyToArguments(movieToCopy);

            movieToCopy.Title = Title;
            movieToCopy.Description = Description;
            movieToCopy.Duration = Duration;
            movieToCopy.Is3D = Is3D;
            movieToCopy.IsOrignalAudio = IsOrignalAudio;
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
