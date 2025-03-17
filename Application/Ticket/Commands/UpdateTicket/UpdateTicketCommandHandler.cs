using Application.Common.Interfaces;
using MediatR;

namespace Application.Ticket.Commands.UpdateTicket
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, Unit>
    {
        private readonly ITicketService _ticketService;
        public UpdateTicketCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<Unit> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            await _ticketService.UpdateTicket(request.Ticket);

            return Unit.Value;
        }
    }
}
