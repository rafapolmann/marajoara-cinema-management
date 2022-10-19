using Marajoara.Cinema.Management.Application.Authorization.Commands;
using Marajoara.Cinema.Management.Application.Authorization.Handlers;
using Marajoara.Cinema.Management.Application.Authorization.Models;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Commands;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Handlers;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Models;
using Marajoara.Cinema.Management.Application.Features.CineRoomModule.Queries;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Commands;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Handlers;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Models;
using Marajoara.Cinema.Management.Application.Features.MovieModule.Queries;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Commands;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Handlers;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Models;
using Marajoara.Cinema.Management.Application.Features.SessionModule.Queries;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Commands;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Handlers;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Application.Features.TicketModule.Queries;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Handlers;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Models;
using Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using MediatR.Ninject;
using Ninject;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Infra.Framework.IoC.Extensions
{
    public static class MediatRExtensions
    {
        public static void BindMediatRSetup(this IKernel kernel)
        {
            kernel.BindMediatR();

            kernel.BindCineRoomSetup();
            kernel.BindMovieSetup();
            kernel.BindSessionSetup();
            kernel.BindUserAccountSetup();
            kernel.BindAuthorizationSetup();
            kernel.BindTicketSetup();
        }

        private static void BindSessionSetup(this IKernel kernel)
        {
            kernel.Bind<IRequestHandler<AllSessionsQuery, Result<Exception, List<SessionModel>>>>().To<AllSessionsHandler>();
            kernel.Bind<IRequestHandler<GetSessionQuery, Result<Exception, SessionModel>>>().To<GetSessionHandler>();
            kernel.Bind<IRequestHandler<AddSessionCommand, Result<Exception, int>>>().To<AddSessionHandler>();
            kernel.Bind<IRequestHandler<UpdateSessionCommand, Result<Exception, bool>>>().To<UpdateSessionHandler>();
            kernel.Bind<IRequestHandler<DeleteSessionCommand, Result<Exception, bool>>>().To<DeleteSessionHandler>();
            kernel.Bind<IRequestHandler<GetSessionsByCineRoomQuery, Result<Exception, List<SessionModel>>>>().To<GetSessionsByCineRoomHandler>();
            kernel.Bind<IRequestHandler<GetSessionsByMovieTitleQuery, Result<Exception, List<SessionModel>>>>().To<GetSessionsByMovieTitleHandler>();
            kernel.Bind<IRequestHandler<GetSessionsByDateQuery, Result<Exception, List<SessionModel>>>>().To<GetSessionsByDateHandler>();
            kernel.Bind<IRequestHandler<GetSessionsByDateRangeQuery, Result<Exception, List<SessionModel>>>>().To<GetSessionsByDateRangeHandler>();
            kernel.Bind<IRequestHandler<GetSessionTicketsQuery, Result<Exception, List<TicketModel>>>>().To<GetSessionTicketsHandler>();
            kernel.Bind<IRequestHandler<GetSessionOccupiedSeatsQuery, Result<Exception, List<TicketSeatModel>>>>().To<GetSessionOccupiedSeatsHandler>();
        }

        private static void BindCineRoomSetup(this IKernel kernel)
        {
            kernel.Bind<IRequestHandler<GetCineRoomQuery, Result<Exception, CineRoomModel>>>().To<GetCineRoomHandler>();
            kernel.Bind<IRequestHandler<AllCineRoomsQuery, Result<Exception, List<CineRoomModel>>>>().To<AllCineRoomsHandler>();
            kernel.Bind<IRequestHandler<AddCineRoomCommand, Result<Exception, int>>>().To<AddCineRoomHandler>();
            kernel.Bind<IRequestHandler<DeleteCineRoomCommand, Result<Exception, bool>>>().To<DeleteCineRoomHandler>();
            kernel.Bind<IRequestHandler<UpdateCineRoomCommand, Result<Exception, bool>>>().To<UpdateCineRoomHandler>();
        }

        private static void BindMovieSetup(this IKernel kernel)
        {
            kernel.Bind<IRequestHandler<GetMovieQuery, Result<Exception, MovieModel>>>().To<GetMovieHandler>();
            kernel.Bind<IRequestHandler<AllMoviesQuery, Result<Exception, List<MovieModel>>>>().To<AllMoviesHandler>();
            kernel.Bind<IRequestHandler<AddMovieCommand, Result<Exception, int>>>().To<AddMovieHandler>();
            kernel.Bind<IRequestHandler<DeleteMovieCommand, Result<Exception, bool>>>().To<DeleteMovieHandler>();
            kernel.Bind<IRequestHandler<UpdateMovieCommand, Result<Exception, bool>>>().To<UpdateMovieHandler>();
            kernel.Bind<IRequestHandler<UpdateMoviePosterCommand, Result<Exception, bool>>>().To<UpdateMoviePosterHandler>();
            kernel.Bind<IRequestHandler<GetMoviePosterQuery, Result<Exception, byte[]>>>().To<GetMoviePosterHandler>();
            kernel.Bind<IRequestHandler<DeleteMoviePosterCommand, Result<Exception, bool>>>().To<DeleteMoviePosterHandler>();
            kernel.Bind<IRequestHandler<GetMoviesBySessionDateQuery, Result<Exception, List<MovieWithSessionsModel>>>>().To<GetMoviesBySessionDateHandler>();            

        }

        private static void BindUserAccountSetup(this IKernel kernel)
        {
            kernel.Bind<IRequestHandler<GetUserAccountQuery, Result<Exception, UserAccountModel>>>().To<GetUserAccountHandler>();
            kernel.Bind<IRequestHandler<AllUserAccountsQuery, Result<Exception, List<UserAccountModel>>>>().To<AllUserAccountsHandler>();
            kernel.Bind<IRequestHandler<AddCustomerUserAccountCommand, Result<Exception, int>>>().To<AddCustomerUserAccountHandler>();
            kernel.Bind<IRequestHandler<AddAttendantUserAccountCommand, Result<Exception, int>>>().To<AddAttendantUserAccountHandler>();
            kernel.Bind<IRequestHandler<AddManagerUserAccountCommand, Result<Exception, int>>>().To<AddManagerUserAccountHandler>();
            kernel.Bind<IRequestHandler<DeleteUserAccountCommand, Result<Exception, bool>>>().To<DeleteUserAccountHandler>();
            kernel.Bind<IRequestHandler<GetUserAccountTicketsQuery, Result<Exception, List<TicketModel>>>>().To<GetUserAccountTicketsHandler>();
            kernel.Bind<IRequestHandler<UpdateUserAccountPhotoCommand, Result<Exception, bool>>>().To<UpdateUserAccountPhotoHandler>();
            kernel.Bind<IRequestHandler<DeleteUserAccountPhotoCommand, Result<Exception, bool>>>().To<DeleteUserAccountPhotoHandler>();
            kernel.Bind<IRequestHandler<GetUserAccountPhotoQuery, Result<Exception, byte[]>>>().To<GetUserAccountPhotoHandler>();

        }

        private static void BindAuthorizationSetup(this IKernel kernel)
        {
            kernel.Bind<IRequestHandler<AuthenticateCommand, Result<Exception, AuthenticatedUserAccountModel>>>().To<AuthenticateHandler>();
        }

        private static void BindTicketSetup(this IKernel kernel)
        {
            kernel.Bind<IRequestHandler<AllTicketsQuery, Result<Exception, List<TicketModel>>>>().To<AllTicketsHandler>();
            kernel.Bind<IRequestHandler<AddTicketCommand, Result<Exception, int>>>().To<AddTicketHandler>();
            kernel.Bind<IRequestHandler<DeleteTicketCommand, Result<Exception, bool>>>().To<DeleteTicketHandler>();
            kernel.Bind<IRequestHandler<GetTicketByCodeQuery, Result<Exception, TicketModel>>>().To<GetTicketByCodeHandler>();
            kernel.Bind<IRequestHandler<GetTicketByIDQuery, Result<Exception, TicketModel>>>().To<GetTicketByIDHandler>();
        }
    }
}
