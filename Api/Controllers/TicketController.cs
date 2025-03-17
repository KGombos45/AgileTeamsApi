using Api.Common;
using Application.Ticket.Commands.CreateTicket;
using Application.Ticket.Commands.DeleteTicket;
using Application.Ticket.Commands.UpdateTicket;
using Application.Ticket.Queries.GetTicketStatuses;
using Application.Ticket.Queries.GetTicketOwnerCount;
using Application.Ticket.Queries.GetTickets;
using Application.Ticket.Queries.GetTicketStatusCount;
using Application.Ticket.Queries.GetTicketTypeCount;
using Application.Ticket.Queries.GetTicketTypes;
using Application.Ticket.Queries.GetUserTickets;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TicketController : ApiControllerBase
    {
        [HttpPost("create", Name = nameof(CreateTicket))]
        public async Task<ActionResult> CreateTicket([FromBody] Ticket ticket)
        {
            var command = new CreateTicketCommand
            {
                Ticket = ticket
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("update", Name = nameof(UpdateTicket))]
        public async Task<ActionResult> UpdateTicket([FromBody] Ticket ticket)
        {
            var command = new UpdateTicketCommand
            {
                Ticket = ticket
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("{ticketId}/delete", Name = nameof(DeleteTicket))]
        public async Task<ActionResult> DeleteTicket([FromRoute] string ticketId)
        {
            var command = new DeleteTicketCommand
            {
                TicketId = ticketId
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet("statuses", Name = nameof(GetTicketStatuses))]
        public async Task<ActionResult<List<TicketStatus>>> GetTicketStatuses()
        {
            var response = await Mediator.Send(new GetTicketStatusesQuery());

            return Ok(response);
        }

        [HttpGet("types", Name = nameof(GetTicketTypes))]
        public async Task<ActionResult<List<TicketType>>> GetTicketTypes()
        {
            var response = await Mediator.Send(new GetTicketTypesQuery());

            return Ok(response);
        }

        [HttpGet("ticketStatusCount", Name = nameof(GetTicketStatusCount))]
        public async Task<ActionResult<List<Array>>> GetTicketStatusCount()
        {
            var response = await Mediator.Send(new GetTicketStatusCountQuery());

            return Ok(response);
        }

        [HttpGet("ticketTypeCount", Name = nameof(GetTicketTypeCount))]
        public async Task<ActionResult<List<Array>>> GetTicketTypeCount()
        {
            var response = await Mediator.Send(new GetTicketTypeCountQuery());

            return Ok(response);
        }

        [HttpGet("ticketOwnerCount", Name = nameof(GetTicketOwnerCount))]
        public async Task<ActionResult<List<Array>>> GetTicketOwnerCount()
        {
            var response = await Mediator.Send(new GetTicketOwnerCountQuery());

            return Ok(response);
        }

        [HttpGet("tickets", Name = nameof(GetTickets))]
        public async Task<ActionResult<List<Ticket>>> GetTickets()
        {
           var response = await Mediator.Send(new GetTicketsQuery());

            return Ok(response);
        }

        [HttpGet("{userId}/tickets", Name = nameof(GetUserTickets))]
        public async Task<ActionResult<List<Ticket>>> GetUserTickets([FromRoute] string userId)
        {
            var query = new GetUserTicketsQuery
            {
                UserId = userId
            };

            var response = await Mediator.Send(query);

            return Ok(response);
        }
    }
}
