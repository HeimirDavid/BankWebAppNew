using AutoMapper;

namespace BankWebApp.Infrastructure.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {

            CreateMap<Models.Transaction, BankWebApp.ViewModels.TransactionViewModel>();
                
        }
        
    }
}
