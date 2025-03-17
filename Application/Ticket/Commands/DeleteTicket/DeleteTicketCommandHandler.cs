using Application.Common.Interfaces;
using MediatR;

namespace Application.Ticket.Commands.DeleteTicket
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, Unit>
    {
        private readonly ITicketService _ticketService;
        public DeleteTicketCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<Unit> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            await _ticketService.DeleteTicket(request.TicketId);

            return Unit.Value;
        }
    }
}
