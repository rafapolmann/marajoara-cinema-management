using FluentAssertions;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Marajoara.Cinema.Management.Tests.Unit.Domain
{
    [TestClass]
    public class MovieTests
    {
        #region CopyTo
        [TestMethod]
        public void Movie_CopyTo_Should_Copy_All_Properties_Except_MovieID_And_Poster()
        {
            int originalID = 3;

            Movie movie = GetMovieToTest();
            Movie movieToCopy = new Movie
            {
                MovieID = originalID,
                Poster = new byte[1024]
            };

            movie.CopyTo(movieToCopy);

            movieToCopy.Title.Should().Be(movie.Title);
            movieToCopy.Description.Should().Be(movie.Description);
            movieToCopy.Minutes.Should().Be(movie.Minutes);
            movieToCopy.Is3D.Should().Be(movie.Is3D);
            movieToCopy.IsOriginalAudio.Should().Be(movie.IsOriginalAudio);
            movieToCopy.MovieID.Should().NotBe(movie.MovieID);
            movieToCopy.MovieID.Should().Be(originalID);
            movieToCopy.Poster.Should().NotBeNull();
        }

        [TestMethod]
        public void Movie_CopyTo_Should_Not_Copy_MovieID()
        {
            int originalID = 3;

            Movie movie = GetMovieToTest();
            Movie movieToCopy = new Movie { MovieID = originalID };

            movie.CopyTo(movieToCopy);

            movieToCopy.MovieID.Should().NotBe(movie.MovieID);
            movieToCopy.MovieID.Should().Be(originalID);
        }

        [TestMethod]
        public void Movie_CopyTo_Should_Not_Copy_Poster()
        {
            Movie movie = GetMovieToTest();
            Movie movieToCopy = new Movie { Poster = new byte[1024] };

            movie.CopyTo(movieToCopy);

            movieToCopy.Poster.Should().NotBeNull();
        }

        [TestMethod]
        public void Movie_CopyTo_Should_Should_Throw_ArgumentException_When_MovieToCopy_Is_The_Same_Instance()
        {
            Movie movie = GetMovieToTest();

            Action action = () => movie.CopyTo(movie);
            action.Should().Throw<ArgumentException>().WithMessage("Movie to copy cannot be the same instance of the origin. (Parameter 'movieToCopy')");
        }

        [TestMethod]
        public void Movie_CopyTo_Should_Should_Throw_ArgumentException_When_MovieToCopy_Is_Null()
        {
            Movie movie = GetMovieToTest();

            Action action = () => movie.CopyTo(null);
            action.Should().Throw<ArgumentException>().WithMessage("Movie parameter cannot be null. (Parameter 'movieToCopy')");
        }
        #endregion CopyTo

        #region Validate
        [TestMethod]
        public void Movie_Validate_Should_Return_True_When_All_Rules_Are_OK()
        {
            Movie movie = GetMovieToTest();
            movie.Validate().Should().BeTrue();
        }

        [TestMethod]
        public void Movie_Validate_Should_Throw_Exception_When_Movie_Name_Is_Empty()
        {
            Movie movie = GetMovieToTest(1, string.Empty);

            Action action = () => movie.Validate();
            action.Should().Throw<Exception>().WithMessage("Movie title cannot be null or empty.");
        }

        [TestMethod]
        public void Movie_Validate_Should_Throw_Exception_When_Movie_Name_Is_Null()
        {
            Movie movie = GetMovieToTest(1, null);

            Action action = () => movie.Validate();
            action.Should().Throw<Exception>().WithMessage("Movie title cannot be null or empty.");
        }

        [TestMethod]
        public void Movie_Validate_Should_Throw_Exception_When_Movie_Minutes_Is_Zero()
        {
            Movie movie = GetMovieToTest(1, "Movie", "Description", 0);

            Action action = () => movie.Validate();
            action.Should().Throw<Exception>().WithMessage("Movie minutes cannot be less or equals zero.");
        }

        [TestMethod]
        public void Movie_Validate_Should_Throw_Exception_When_Movie_Minutes_Less_Than_Zero()
        {
            Movie movie = GetMovieToTest(1, "Movie", "Description", -1);

            Action action = () => movie.Validate();
            action.Should().Throw<Exception>().WithMessage("Movie minutes cannot be less or equals zero.");
        }
        #endregion Validate

        protected Movie GetMovieToTest(int movieID = 1,
                                       string title = "Title",
                                       string description = "Description",
                                       int minutes = 60,
                                       bool is3D = false,
                                       bool IsOriginalAudio = false)
        {

            return new Movie
            {
                MovieID = movieID,
                Title = title,
                Description = description,
                Minutes = minutes,
                Is3D = is3D,
                IsOriginalAudio = IsOriginalAudio
            };
        }
    }
}
