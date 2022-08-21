using FluentAssertions;
using Marajoara.Cinema.Management.Domain.MovieModule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Marajoara.Cinema.Management.Tests.Unit.Domain
{
    [TestClass]
    public class MovieTests
    {
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
        #endregion Validate

        protected Movie GetMovieToTest(int movieID = 1,
                                       string title = "Title",
                                       string description = "Description",
                                       bool is3D = false,
                                       bool isOrignalAudio = false)
        {

            return new Movie
            {
                MovieID = movieID,
                Title = title,
                Description = description,
                Duration = new TimeSpan(1, 30, 0),
                Is3D = is3D,
                IsOrignalAudio = isOrignalAudio
            };
        }
    }
}
