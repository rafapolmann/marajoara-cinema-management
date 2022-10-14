using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.SessionModule.Models
{
    public class SessionFlatModel
    {
        public int SessionID { get; set; }
        public DateTime SessionDate { get; set; }
        public DateTime EndSession { get; set; }
        public decimal Price { get; set; }       
    }
}
