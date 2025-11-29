using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Status { get; set; } = "Open";  // Open, In Progress, Closed
        public string Priority { get; set; } = "Medium"; // High, Medium, Low
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
