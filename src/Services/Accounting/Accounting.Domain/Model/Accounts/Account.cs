using Common.Domain.Model;

namespace Accounting.Domain.Model.Accounts;

public class Account
{
    public Guid Id { get; } = Guid.NewGuid();

    public static (Account, List<IDomainEvent>) Create()
    {
        var account = new Account();
        return (account, [new AccountCreated(account.Id)]);
    }
}
