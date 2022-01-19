using AutoMapper;

namespace BankWebApp.Infrastructure.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Models.Account, BankWebApp.ViewModels.AccountTransactionsView>()
                .ForMember(dest => dest.Transactions, opt => opt.Ignore());

        }
        
    }
}
