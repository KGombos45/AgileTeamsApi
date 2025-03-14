using Application.Common.Interfaces;
using MediatR;

namespace Application.Ticket.Commands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Unit>
    {
        private readonly ITicketService _ticketService;
        public UpdateCommandHandler(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            await _ticketService.UpdateTicket(request.Ticket);

            return Unit.Value;
        }
    }
}
