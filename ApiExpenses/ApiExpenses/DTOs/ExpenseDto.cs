using ApiExpenses.Models;

namespace ApiExpenses.DTOs
{
    public class ExpenseDto
    {
        public string Id { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string CategoryId { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public CategoryDto Category { get; set; } = null!;

        public UserDto User { get; set; } = null!;
    }
}
