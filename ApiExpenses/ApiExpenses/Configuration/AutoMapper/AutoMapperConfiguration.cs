using ApiExpenses.DTOs;
using ApiExpenses.Models;
using AutoMapper;

namespace ApiExpenses.Configuration.AutoMapper
{
    public class AutoMapperConfiguration:Profile
    {
        public AutoMapperConfiguration() { 
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<Expense, ExpenseDtoUpdate>().ReverseMap();
            CreateMap<ExpenseDtoUpdate, ExpenseDto>().ReverseMap();
            CreateMap<Expense, ExpenseDtoCreate>().ReverseMap();
            CreateMap<UserExpenseDto, Expense>().ReverseMap();
        }
    }
}
