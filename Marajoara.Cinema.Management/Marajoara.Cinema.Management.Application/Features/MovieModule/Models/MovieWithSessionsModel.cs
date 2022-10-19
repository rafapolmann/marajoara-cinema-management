using Marajoara.Cinema.Management.Application.Features.SessionModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marajoara.Cinema.Management.Application.Features.MovieModule.Models
{
    public class MovieWithSessionsModel
    {
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Minutes { get; set; }
        public bool Is3D { get; set; }
        public bool IsOriginalAudio { get; set; }
        
        public byte[] Poster { get; set; }

        public IEnumerable<SessionFlatModel> Sessions { get; set; }
    }
}
