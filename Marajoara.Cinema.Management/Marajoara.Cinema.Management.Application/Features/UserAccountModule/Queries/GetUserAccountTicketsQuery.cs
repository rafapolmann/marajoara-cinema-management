using Marajoara.Cinema.Management.Application.Features.TicketModule.Models;
using Marajoara.Cinema.Management.Domain.Common.ResultModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.UserAccountModule.Queries
{
    public class GetUserAccountTicketsQuery: IRequest<Result<Exception, List<TicketModel>>> 
    {
        public int UserAccountID { get; set; }

        public GetUserAccountTicketsQuery(int userAccountID)
        {
            UserAccountID = userAccountID;
        }
    }
}
