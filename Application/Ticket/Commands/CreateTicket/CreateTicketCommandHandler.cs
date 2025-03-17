using Application.Common.Interfaces;
using MediatR;

namespace Application.Ticket.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Unit>
    {
        private readonly ITicketService _ticketService;
        public CreateTicketCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<Unit> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            await _ticketService.CreateTicket(request.Ticket);

            return Unit.Value;
        }
    }
}
