using Api.Common;
using Application.Ticket.Commands.Create;
using Application.Ticket.Commands.Delete;
using Application.Ticket.Commands.Update;
using Application.Ticket.Queries.GetStatuses;
using Application.Ticket.Queries.GetTicketOwnerCount;
using Application.Ticket.Queries.GetTickets;
using Application.Ticket.Queries.GetTicketStatusCount;
using Application.Ticket.Queries.GetTicketTypeCount;
using Application.Ticket.Queries.GetTypes;
using Application.Ticket.Queries.GetUserTickets;
using Domain.Entities.AgileTeams;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TicketController : ApiControllerBase
    {
        [HttpPost("create", Name = nameof(Create))]
        public async Task<ActionResult> Create([FromBody] Ticket ticket)
        {
            var command = new CreateCommand
            {
                Ticket = ticket
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("update", Name = nameof(Update))]
        public async Task<ActionResult> Update([FromBody] Ticket ticket)
        {
            var command = new UpdateCommand
            {
                Ticket = ticket
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost("{ticketId}/delete", Name = nameof(Delete))]
        public async Task<ActionResult> Delete([FromRoute] string ticketId)
        {
            var command = new DeleteCommand
            {
                TicketId = ticketId
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet("statuses", Name = nameof(GetStatuses))]
        public async Task<ActionResult<List<TicketStatus>>> GetStatuses()
        {
            var response = await Mediator.Send(new GetStatusesQuery());

            return Ok(response);
        }

        [HttpGet("types", Name = nameof(GetTypes))]
        public async Task<ActionResult<List<TicketType>>> GetTypes()
        {
            var response = await Mediator.Send(new GetTypesQuery());

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

        [HttpGet("{userId}/tickets", Name = nameof(GetTickets))]
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
