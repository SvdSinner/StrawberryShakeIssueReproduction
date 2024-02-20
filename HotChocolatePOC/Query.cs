using HotChocolate.Types;
using James.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace HotChocolatePOC
{
    //[QueryType]
    public partial class Query
    {

        static Random _rnd = new Random();

        static public Account GetFakeAccount()
        {
            var id = Guid.NewGuid();
            var addressId = Guid.NewGuid();
            var phoneId = Guid.NewGuid();
            var agentId = Guid.NewGuid();
            var agencyId = Guid.NewGuid();
            var agencyNumber = _rnd.Next(1000, 9999).ToString();
            var accountNumber = _rnd.Next(10000, 99999).ToString();
            var attorneyId = Guid.NewGuid();
            var underwriterId = Guid.NewGuid();
            var acct = new Account
            {
                Id = id,
                Bank = "Fake Bank",
                IdNavigation = new LegalEntity
                {
                    Id = id,
                    FullName = "Fake TestCompany",
                    LegalEntityAddresses = new List<LegalEntityAddress>
                    {
                        new LegalEntityAddress
                        {
                            Type = "Main",
                            Address = new Address
                            {
                                Address1 = "123 Main St",
                                Address2 = "Suite 200",
                                City = "Ames",
                                StateCode = "IA",
                                StateCodeNavigation = new State
                                {
                                    Code = "IA",
                                    CountryCode = "US",
                                    CountryCodeNavigation =
                                        new CountryDm
                                        {
                                            Code = "US",
                                            Name = "United States"
                                        }
                                },
                                PostalCode = "50010"
                            }
                        }
                    },
                    LegalEntityPhones = new List<LegalEntityPhone>
                    {
                        new LegalEntityPhone
                        {
                            Type="Main",
                            PhoneNumber = new PhoneNumber
                            {
                                MainNumber = "5155551234",
                                Extension = "x1"
                            }
                        }
                    },
                    LegalEntityEmails = new List<LegalEntityEmail>
                    {
                        new LegalEntityEmail
                        {
                            Type = "Main",
                            EmailAddress = "Bob@bob.com"
                        }
                    }
                },
                AgentId = agentId,
                Agent = new Agent
                {
                    Id = agentId,
                    IdNavigation = new LegalEntity
                    {
                        Id = agentId,
                        FullName = "Allen the Agent"
                    }
                },
                AgencyNumber = agencyNumber,
                AgencyNumberNavigation = new Agency
                {
                    Id = agencyId,
                    IdNavigation = new LegalEntity
                    {
                        Id = agencyId,
                        FullName = "Agency of Awesome",
                        LegalEntityAddresses = new List<LegalEntityAddress>
                        {
                            new LegalEntityAddress
                            {
                                Type = "Main",
                                Address = new Address
                                {
                                    Address1 = "4000 Corporate Way",
                                    City = "Dallas",
                                    StateCode = "TX"
                                }
                            }
                        }
                    }
                },
                AccountNum = accountNumber,
                Division = "Commercial",
                HomeOfficeReviewed = new DateTime(2020, 2, 2, 2, 2, 2),
                HomeOfficeReviewByNavigation = new UserProfile
                {
                    FullName = "Chuck Schumer"
                },
                BranchReviewed = new DateTime(2021, 1, 1, 1, 1, 1),
                BranchReviewByNavigation = new UserProfile
                {
                    FullName = "Joe Branch"
                },
                AttorneyId = attorneyId,
                Attorney = new LawEntity
                {
                    Id = attorneyId,
                    IdNavigation = new LegalEntity
                    {
                        Id = attorneyId,
                        FullName = "Snidely Whiplash"
                    },
                    MartindaleHubbellRating = "12"
                },
                UnderwriterId = underwriterId,
                Underwriter = new Underwriter
                {
                    Id = underwriterId,
                    IdNavigation = new Employee
                    {
                        FullName = "Ursula Underwriter",
                        Initials = "UUU",
                        Title = "Underwriter",
                        Email = "UUU@berkeysurety.com"
                    },
                    ReportsTo = new Guid()
                }
            };
            return acct;
        }
        public Account? GetAccount(string? accountNumber)
        {
            if (accountNumber == null)
                return null;
            var result = GetFakeAccount();
            result.AccountNum=accountNumber;
            return result;
        }

        public string[] GetFilteredAccountNumbers(string? filter, bool allowSmallFilters = false)
        {
            if (!allowSmallFilters && (filter?.Length ?? 0) < 3)
                throw new GraphQLException(
                    "Filter must be more than 3 characters, or allowSmallFilters must be set to true.");
            if (string.IsNullOrWhiteSpace(filter))
                return FakeAccountNumberList;
            return FakeAccountNumberList.Where(fan=>fan.Contains(filter)).ToArray();
        }

        private static string[] FakeAccountNumberList =
        {
            "10149", "10408", "10531", "10734", "10932", "11465", "13214", "13569", "14404", "14699", "14803", "15075",
            "16374", "18481", "19109", "21417", "21504", "23273", "23578", "23752", "25716", "26334", "29722", "30632",
            "31211", "31846", "32449", "32880", "32886", "32911", "32970", "33775", "34248", "34624", "34808", "35645",
            "35732", "35913", "37357", "37922", "38053", "39171", "39289", "39373", "39455", "39537", "39830", "39996",
            "40234", "40677", "40740", "41003", "41023", "41484", "43227", "43470", "43905", "44009", "44239", "45123",
            "46469", "46796", "46978", "47366", "48152", "49270", "49774", "49906", "50213", "50802", "51471", "51597",
            "52713", "53638", "54024", "54209", "54768", "55851", "56084", "56863", "57121", "57414", "58265", "58914",
            "58977", "58986", "60230", "60738", "62833", "64080", "64758", "65099", "65748", "66336", "67513", "67939",
            "69315", "70194", "70269", "70670",
        };
    }
}
