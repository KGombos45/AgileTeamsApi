using Application.Common.Interfaces;
using MediatR;

namespace Application.Ticket.Commands.Create
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand, Unit>
    {
        private readonly ITicketService _ticketService;
        public CreateCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<Unit> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            await _ticketService.CreateTicket(request.Ticket);

            return Unit.Value;
        }
    }
}
