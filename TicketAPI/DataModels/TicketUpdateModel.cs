using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class TicketUpdateModel
    {
        public string Title { get; set; } = null!;
        public string Status { get; set; } = "Open";
        public string Priority { get; set; } = "Medium";
    }
}
