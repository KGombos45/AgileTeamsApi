using Application.Common.Interfaces;
using MediatR;

namespace Application.Ticket.Commands.Delete
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Unit>
    {
        private readonly ITicketService _ticketService;
        public DeleteCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            await _ticketService.DeleteTicket(request.TicketId);

            return Unit.Value;
        }
    }
}
