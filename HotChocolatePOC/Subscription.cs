using James.Shared.Model;

namespace HotChocolatePOC
{
    public partial class Subscription
    {
        [Subscribe]
        [Topic(nameof(Mutation.UpdateBank))]
        public Account OnAccountModified([EventMessage] Account account) => account;
    }
}
