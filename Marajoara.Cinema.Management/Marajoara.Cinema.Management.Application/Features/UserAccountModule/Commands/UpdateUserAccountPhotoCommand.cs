using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.IO;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Commands
{
    public class UpdateUserAccountPhotoCommand : IRequest<Result<Exception, bool>>
    {
        public int UserAccountID { get; set; }
        public Stream PosterStream { get; set; }

        public UpdateUserAccountPhotoCommand(int userAccountID, Stream stream)
        {
            UserAccountID = userAccountID;
            PosterStream = stream;
        }
    }
}
