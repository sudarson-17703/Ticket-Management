using DataCore;
using DataEntities;
using DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataServices
{
    public class TicketService
    {
        private readonly TicketContext _context;

        public TicketService(TicketContext context)
        {
            _context = context;
        }

        // GET all tickets
        public async Task<List<TicketModel>> GetAllTicketsAsync() =>
            await _context.Tickets
                .Select(t => new TicketModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Status = t.Status,
                    Priority = t.Priority,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync();

        // GET ticket by Id
        public async Task<TicketModel?> GetTicketByIdAsync(int id)
        {
            var t = await _context.Tickets.FindAsync(id);
            if (t == null) return null;

            return new TicketModel
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status,
                Priority = t.Priority,
                CreatedAt = t.CreatedAt
            };
        }

        // POST add new ticket
        public async Task<TicketModel> AddTicketAsync(TicketUpdateModel model)
        {
            var ticket = new Ticket
            {
                Title = model.Title,
                Status = model.Status,
                Priority = model.Priority,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return new TicketModel
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Status = ticket.Status,
                Priority = ticket.Priority,
                CreatedAt = ticket.CreatedAt
            };
        }

        // PUT update ticket
        public async Task<TicketModel?> UpdateTicketAsync(int id, TicketUpdateModel model)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return null;

            ticket.Title = model.Title;
            ticket.Status = model.Status;
            ticket.Priority = model.Priority;

            await _context.SaveChangesAsync();

            return new TicketModel
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Status = ticket.Status,
                Priority = ticket.Priority,
                CreatedAt = ticket.CreatedAt
            };
        }

        // DELETE ticket
        public async Task<bool> DeleteTicketAsync(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null) return false;

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
