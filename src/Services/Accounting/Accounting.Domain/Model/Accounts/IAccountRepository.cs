namespace Accounting.Domain.Model.Accounts;

public interface IAccountRepository
{
    Task<Account> FindByIdAsync(Guid accountId);
    Task SaveAsync(Account account);
}
