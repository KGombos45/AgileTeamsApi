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
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ApiControllerBase
    {
        [HttpPost]
        [Route("Create")]
        //POST: api/Ticket/Create
        public async Task<ActionResult> Create(Ticket ticket)
        {
            var command = new CreateCommand
            {
                Ticket = ticket
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        //PUT : /api/Ticket/UpdateTicket
        public async Task<ActionResult> Update(Ticket ticket)
        {
            var command = new UpdateCommand
            {
                Ticket = ticket
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<ActionResult> Delete(string ticketId)
        {
            var command = new DeleteCommand
            {
                TicketId = ticketId
            };

            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        [Route("Statuses")]
        //GET : /api/Project/Statuses
        public async Task<ActionResult<List<TicketStatus>>> GetStatuses()
        {
            var response = await Mediator.Send(new GetStatusesQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("Types")]
        //GET : /api/Ticket/Types
        public async Task<ActionResult<List<TicketType>>> GetTypes()
        {
            var response = await Mediator.Send(new GetTypesQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("GetTicketStatusCount")]
        //GET : /api/Ticket/GetTicketStatusCount
        public async Task<ActionResult<List<Array>>> GetTicketStatusCount()
        {
            var response = await Mediator.Send(new GetTicketStatusCountQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("GetTicketTypeCount")]
        //GET : /api/Ticket/GetTicketTypeCount
        public async Task<ActionResult<List<Array>>> GetTicketTypeCount()
        {
            var response = await Mediator.Send(new GetTicketTypeCountQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("GetTicketOwnerCount")]
        //GET : /api/Ticket/GetTicketOwnerCount
        public async Task<ActionResult<List<Array>>> GetTicketOwnerCount()
        {
            var response = await Mediator.Send(new GetTicketOwnerCountQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("Tickets")]
        //GET : /api/Ticket/Tickets
        public async Task<ActionResult<List<Ticket>>> GetTickets()
        {
           var response = await Mediator.Send(new GetTicketsQuery());

            return Ok(response);
        }

        [HttpGet]
        [Route("Tickets/{userId}")]
        //GET : /api/Ticket/Tickets
        public async Task<ActionResult<List<Ticket>>> GetUserTickets(string userId)
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
