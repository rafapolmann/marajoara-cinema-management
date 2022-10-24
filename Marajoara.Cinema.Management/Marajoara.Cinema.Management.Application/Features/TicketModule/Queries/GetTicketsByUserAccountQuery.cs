﻿using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;

namespace Marajoara.Cinema.Management.Application.Features.TicketModule.Queries
{
    public class GetTicketsByUserAccountQuery : IRequest<Result<Exception, List<TicketModel>>>
    {
        public int UserAccountID { get; set; }

        public GetTicketsByUserAccountQuery(int id)
        {
            UserAccountID = id;
        }
    }
}
