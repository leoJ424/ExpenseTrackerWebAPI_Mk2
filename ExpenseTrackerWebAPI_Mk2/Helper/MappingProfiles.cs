using AutoMapper;
using ExpenseTrackerWebAPI_Mk2.Dto;
using ExpenseTrackerWebAPI_Mk2.Models;

namespace ExpenseTrackerWebAPI_Mk2.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Recipient, RecipientDto>();
            CreateMap<RecipientDto, Recipient>();

            CreateMap<CreditCard, CreditCardDto>();
            CreateMap<CreditCardDto, CreditCard>();
        }
    }
}
