using AutoMapper;

namespace BankWebApp.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<BankWebApp.ViewModels.NewCustomerViewModel, Models.Customer>();

            CreateMap<BankWebApp.ViewModels.EditCustomerViewModel, Models.Customer>()
                .ReverseMap();
            
            CreateMap<BankWebApp.ViewModels.CustomerView, BankWebApp.ViewModels.EditCustomerViewModel> ()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
