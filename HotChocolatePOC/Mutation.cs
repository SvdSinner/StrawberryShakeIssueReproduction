using HotChocolate.Subscriptions;
using James.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HotChocolatePOC
{
    [MutationType]
    public class BankMutationType : ObjectType<Mutation>
    {
    }

    public partial class Mutation
    {
        public async Task<Account> UpdateBank(UpdateBankInput input,
            [Service] ITopicEventSender eventSender)
        {
            var account = Query.GetFakeAccount();
            account.AccountNum=input.AccountNum;
            if (input.Bank != account.Bank)
            {
                account.Bank = input.Bank;
                await eventSender.SendAsync(nameof(Mutation.UpdateBank), account);
            }
            return account;
        }
    }

    public class UpdateBankInput
    {
        public string AccountNum { get; set; }
        public string Bank { get; set; }
    }
}
