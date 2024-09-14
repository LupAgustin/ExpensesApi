namespace ApiExpenses.DTOs
{
    public class ExpenseDtoUpdate
    {
        public string Id { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string CategoryId { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
