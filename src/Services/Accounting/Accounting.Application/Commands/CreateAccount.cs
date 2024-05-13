using Accounting.Domain.Model.Accounts;
using Common.Application.Commands;

namespace Accounting.Application.Commands;

public class CreateAccount
{
    public record Command() : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IAccountRepository _accountRepository;

        public Handler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            (var account, var events) = Account.Create();
            await _accountRepository.SaveAsync(account);
            domainEventPublisher.publish(account, events);
        }
    }
}
