using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class TicketModel
    {
        public int Id { get; set; }             // for GET responses
        public string Title { get; set; } = null!;
        public string Status { get; set; } = "Open";
        public string Priority { get; set; } = "Medium";
        public DateTime CreatedAt { get; set; }
    }
}
