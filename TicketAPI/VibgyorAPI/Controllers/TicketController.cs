using DataEntities;
using DataModels;
using DataServices;
using Microsoft.AspNetCore.Mvc;

namespace VibgyorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketsController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: api/tickets
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        // GET: api/tickets/{id}
        [HttpGet("by/{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            return ticket == null ? NotFound() : Ok(ticket);
        }

        // POST: api/tickets
        [HttpPost("add")]
        public async Task<IActionResult> AddTicket([FromBody] TicketUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _ticketService.AddTicketAsync(model);
            return CreatedAtAction(nameof(GetTicket), new { id = created.Id }, created);
        }

        // PUT: api/tickets/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] TicketUpdateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _ticketService.UpdateTicketAsync(id, model);
            return updated == null ? NotFound() : Ok(updated);
        }

        // DELETE: api/tickets/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var deleted = await _ticketService.DeleteTicketAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
